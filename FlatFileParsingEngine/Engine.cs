using AttributeUtilities;
using FlatFileObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FlatFileParsingEngine
{
    public class Engine
    {
        #region custom logging event
        public delegate void LogEventHandler(object sender, LogEventArgs e);
        public event LogEventHandler OnLogEvent;
        public virtual void LogEvent(LogEventArgs e)
        {
            OnLogEvent?.Invoke(this, e);
        }
        #endregion

        #region Object Caching
        // for resource optimization, store this once, so we don't have to repeat the reflection step
        Dictionary<Type, PropertyInfo[]> CacheProps = new Dictionary<Type, PropertyInfo[]>();
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a collection of custom format objects from the flat file content, inject all necessary parts
        /// </summary>
        /// <param name="FilePath">the source content to parse</param>
        /// <param name="DLLFolder">the location of the DLLs to load dynamically</param>
        /// <param name="Delimiter">the delimiter for use in parsing</param>
        /// <returns></returns>
        public FlatFileObject[] LoadFileToObjects(string FilePath, string DLLFolder,char Delimiter)
        {
            FlatFileObject[] results = null;

            // try/catch the processing method
            // use the 'Out' function for user feedback
            try
            {
                Out("Reading lines from the file '" + FilePath + "'...", TraceLevel.Info);
                string[] Lines = File.ReadAllLines(FilePath);
                Out("File line count: " + Lines.Length, TraceLevel.Verbose);

                Out("Getting format from first line of file...", TraceLevel.Verbose);
                string formatVersion = GetFormatFromFirstLine(Lines[0], Delimiter);
                Out("Format found: " + formatVersion, TraceLevel.Verbose);

                // format versions are in the pattern of: [a-z]+[0-9]+, so strip off the digits
                string baseFormat = Regex.Replace(formatVersion, @"[\d-]", string.Empty);

                // go find the dll that matches the format
                string dllFile = Path.Combine(DLLFolder, baseFormat + ".dll");

                Out("Loading dll from: '" + dllFile + "'...", TraceLevel.Verbose);
                Assembly a = Assembly.LoadFile(dllFile);

                // check to see that the dll has any classes in it that extend the FlatFileObject class
                Out("Looking through assembly for known types...", TraceLevel.Verbose);
                foreach (TypeInfo ti in a.DefinedTypes)
                {
                    // we only care if the type is the one we want to create
                    if (ti.Name == formatVersion)
                    {
                        Out("Format found, populating objects...", TraceLevel.Verbose);

                        // because we found the right type, prepare the collection
                        results = new FlatFileObject[Lines.Length];

                        // get the default constructor for this formatVersion class type (we only need this one)
                        ConstructorInfo ci = ti.GetConstructor(Type.EmptyTypes);

                        // go through the content lines
                        Out("Count to be populated: " + Lines.Length, TraceLevel.Info);
                        for (int i = 0; i < Lines.Length; i++)
                        {
                            // skip blank lines
                            if (String.IsNullOrEmpty(Lines[i])) { continue; }

                            object o = ci.Invoke(null); // this invokes the child class constructor, specifically

                            // now fill the empty object from the flat file
                            PopulateObjectWithFields(o as FlatFileObject, Lines[i], Delimiter);

                            // add this new child object to our current collection
                            results[i] = o as FlatFileObject; // but we store it as the parent class
                        }

                        // no need to continue the loop since the types are all the same in a file
                        break; // but don't return within the middle of a function, always fall to the end.
                    }
                } // end foreach type
            }
            catch (Exception ex)
            {
                Out(ex);
                return results; // return what we have, regardless of the exception
            }

            return results;
        }

        /// <summary>
        /// Translate the memory objects into a flat data table, for ease of display
        /// NOTE: assumes the collection to be all the same child type (we only analyze the first object)
        /// </summary>
        /// <returns>DataTable where a row represents an object, and each cell is a field/property of that object.</returns>
        public static DataTable ConvertObjectsToTable(FlatFileObject[] ffo)
        {
            DataTable results = new DataTable();
            if (ffo != null &&
                ffo.Length > 0 &&
                ffo[0] != null)
            {
                // get all the properties of this class (will get the child properties)
                PropertyInfo[] props = ffo[0].GetType().GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    // add a column to our output structure
                    results.Columns.Add(new DataColumn(prop.Name));
                }

                // now populate the data to the table
                foreach (FlatFileObject o in ffo)
                {
                    // new row pulls the sturcuture, from above column work
                    DataRow row = results.NewRow();
                    foreach (PropertyInfo prop in props)
                    {
                        // since the columns were named by the properties, use that again for placing data
                        row[prop.Name] = prop.GetValue(o).ToString();
                    }

                    results.Rows.Add(row);
                }
            }

            return results;
        }

        /// <summary>
        /// Uses the classes own validation attributes to determine the contents of each field/property as valid
        /// </summary>
        /// <param name="FFOs">The collection of objects to be validated</param>
        public void ValidateObjects(FlatFileObject[] FFOs)
        {
            // catch blank collection
            if (FFOs == null || FFOs.Length < 1) { return; }

            // establish the properties of this type, need only do this once
            PropertyInfo[] props = GetProps(FFOs[0].GetType()); // will get the child props

            for (int i = 0; i < FFOs.Length; i++)
            {
                ValidateObject(FFOs[i], props);
            }
        }
        #endregion

        #region Private Business Functions
        private string GetFormatFromFirstLine(string FlatFileLine, char Delimiter)
        {
            Out("Reading fields from first line...", TraceLevel.Verbose);
            string[] fields = FlatFileLine.Split(Delimiter);
            Out("Field count: " + fields.Length, TraceLevel.Verbose);

            Out("Reading format from first field...", TraceLevel.Verbose);
            string format = fields[0];
            Out("Format read: " + format, TraceLevel.Verbose);

            return format;
        }
        private void PopulateObjectWithFields(FlatFileObject ffo, string line, char Delimiter)
        {
            string[] Fields = line.Split(Delimiter);
            PropertyInfo[] props = GetProps(ffo.GetType());
            foreach (PropertyInfo prop in props)
            {
                // get the custom attribute that we're intersted in
                Attribute a = prop.GetCustomAttribute(typeof(FieldOrderAttribute));
                if (a != null) // some props might be extra and not have our custom attribute, skip them.
                {
                    int position = (a as FieldOrderAttribute).FieldOrder;
                    string val = Fields[position];
                    if (String.IsNullOrEmpty(val)) { continue; } // if no value, skip this property (the default is acceptable)

                    if (prop.PropertyType.FullName.Contains("System.DateTime"))
                    {
                        // parse value as a date time, or skip as nullable
                        // must parse into the object before setting the property, or it will crash
                        DateTime dtVal;
                        if (DateTime.TryParse(val, out dtVal))
                        {
                            prop.SetValue(ffo, dtVal);
                        }
                    }
                    else if (prop.PropertyType == typeof(Int32))
                    {
                        // parse value as int, or skip as nullable
                        int iVal;
                        if (Int32.TryParse(val, out iVal))
                        {
                            prop.SetValue(ffo, iVal); // can only set the value to the correct type, or crash
                        }
                    }
                    else // assume string for most of them
                    {
                        prop.SetValue(ffo, Fields[position]); // null will fill as null, so no parsing necessary
                    }
                }
            }
        }
        private PropertyInfo[] GetProps(Type type)
        {
            if (!CacheProps.ContainsKey(type) ||
                CacheProps[type] == null)
            {
                PropertyInfo[] props = type.GetProperties();
                CacheProps[type] = props;
            }

            return CacheProps[type];
        }
        private void ValidateObject(FlatFileObject ffo, PropertyInfo[] props)
        {
            foreach (PropertyInfo p in props)
            {
                Attribute a = p.GetCustomAttribute<ValidationAttribute>();
                if (a != null)
                {
                    // setup the validation result for this field/property
                    ValidationResult res = new ValidationResult();
                    res.ObjectIdentifier = p.Name;
                    res.Valid = false; // assume false in case we crash mid-validation

                    if (p.PropertyType == typeof(String))
                    {
                        string v = p.GetValue(ffo) as string;
                        string pattern = (a as ValidationAttribute).RegEx;
                        res.ValidationCriteria = pattern;

                        try // only put a try/catch around the actual content validation, 
                            // everything else would be an engine problem.
                        {
                            res.Valid = Regex.IsMatch(v, pattern);
                        }
                        catch (Exception ex)
                        {
                            // store this exception to the field's validation results
                            res.ValidationException = ex;
                        }
                        if (!res.Valid)
                        {
                            // theory: keep the actual content out of the logs, in case this ends up being sensitive data
                            res.ValidationMessage = "Field failed Regex validation match";

                            // future: dig into the failed match and log patterns...
                        }// if it is valid, no need to set any other values.

                        // if one field fails it all fails
                        ffo.ObjectValidationResult.Valid &= res.Valid;

                        // add this result to the collection
                        ffo.FieldValidationResults.Add(res);
                    }
                }
            }
        }
        #endregion

        #region Logging
        private void Out(Exception ex)
        {
            LogEvent(new LogEventArgs(ex));
        }
        private void Out(String msg, TraceLevel lvl)
        {
            LogEvent(new LogEventArgs(msg, lvl));
        }
        #endregion
    }
}
