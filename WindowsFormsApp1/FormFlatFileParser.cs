using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FlatFileObjects;
using AttributeUtilities;
using System.Data;

namespace WindowsFormsApp1
{
    public partial class FormFlatFileParser : Form
    {
        #region MAIN
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormFlatFileParser());
        }
        #endregion

        #region Class Members and Properties
        // cash the memory objects for display and interaction
        FlatFileObject[] CurrentFFOs = null;
        DataGridViewCellStyle errorStyle;
        #endregion

        #region Constructor and Setup
        public FormFlatFileParser()
        {
            InitializeComponent();

            // one-time setup of the error cell style for the grid
            errorStyle = new DataGridViewCellStyle();
            errorStyle.ForeColor = System.Drawing.Color.Red;
        }
        #endregion

        #region Event Handlers
        private void btnDLLFolder_Click(object sender, EventArgs e)
        {
            // always start with a new dialog, just in case old folders or settings linger
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            // preload the previous folder, if it exists and is valid
            if (!String.IsNullOrEmpty(tbDLLFolder.Text))
            {
                if (System.IO.Directory.Exists(tbDLLFolder.Text))
                {
                    fbd.SelectedPath = tbDLLFolder.Text;
                }
            }

            // set the new folder
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                // only update it if the value is different (save resources)
                if (tbDLLFolder.Text != fbd.SelectedPath)
                {
                    tbDLLFolder.Text = fbd.SelectedPath;
                }
            }
        }
        private void btnFile_Click(object sender, EventArgs e)
        {
            // always start with a new dialog, just in case old files or settings linger
            OpenFileDialog ofd = new OpenFileDialog();

            // preload the folder of the file, if the file exists
            if (!String.IsNullOrEmpty(tbFile.Text))
            {
                if (System.IO.File.Exists(tbFile.Text))
                {
                    ofd.InitialDirectory = System.IO.Path.GetDirectoryName(tbFile.Text);
                }
            }

            // set the new folder
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // only update it if the value is different (save resources)
                if (tbFile.Text != ofd.FileName)
                {
                    tbFile.Text = ofd.FileName;

                    // and clear the grid of any old data, with no longer matches the selected file.
                    Out("Clearing old data from grid...");
                    dgvResults.DataSource = null;
                    Out("Done.");
                }
            }
        }
        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadFile();
            Out("File loaded, displaying...");
            DisplayFile();
            Out("Done.");
            Cursor.Current = Cursors.Default;
        }
        private void btnValidate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ValidateObjects();
            Out("File loaded, displaying...");
            DisplayValidation();
            Out("Done.");
            Cursor.Current = Cursors.Default;
        }
        private void dgvResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ValidationResult res = CurrentFFOs[e.RowIndex].FieldValidationResults[e.ColumnIndex] as ValidationResult;

            // only pop a message if the cell is not valid
            if (!res.Valid)
            {
                // show the exception, if we got one.
                if (res.ValidationException != null)
                {
                    Out(res.ValidationException);
                }
                else
                {
                    Out(res.ValidationMessage);
                }
            }
        }
        #endregion

        #region Business Methods
        /// <summary>
        /// Loads the text from a given file into the cached flat file object colleciton
        /// </summary>
        private void LoadFile()
        {
            // instantiate our engine, using instances to allow for expansion into multi-threads, etc.
            FlatFileParsingEngine.Engine engine = new FlatFileParsingEngine.Engine();
            // hook to our custom logging event, so we can log any internal messages
            engine.OnLogEvent += Engine_OnLogEvent;

            char delim = '|';
            if(tbDelimiter.Text.Length == 1)
            {
                delim = tbDelimiter.Text[0];
            }
            // do the work, injecting dependencies, for Unit Testing compatibility
            CurrentFFOs = engine.LoadFileToObjects(tbFile.Text, tbDLLFolder.Text, delim);
        }
        private void Engine_OnLogEvent(object sender, FlatFileParsingEngine.LogEventArgs e)
        {
            if(e.LogException != null)
            {
                Out("ERROR: " + e.LogException);
            }
            else
            {
                Out(e.LogMessage);
            }
        }
        /// <summary>
        /// Display the result of the flat file memory objects onto the UI
        /// </summary>
        private void DisplayFile()
        {
            // clear first, before doing the work, in case it is slow...
            dgvResults.DataSource = null;

            // because we used dynamic objects and child properties instead of a known parent class,
            // we must convert the anonymous collection to a simple table that the grid can understand
            Out("Converting collection to simple table for display...");
            DataTable results = FlatFileParsingEngine.Engine.ConvertObjectsToTable(CurrentFFOs);

            // now set the data to the grid for display
            Out("Binding data to grid for rendering...");
            dgvResults.DataSource = results;
            dgvResults.Refresh();
        }
        /// <summary>
        /// Use the objects own validation criteria to evaluate the contents of the flat file memory objects
        /// </summary>
        private void ValidateObjects()
        {
            FlatFileParsingEngine.Engine engine = new FlatFileParsingEngine.Engine();
            engine.ValidateObjects(CurrentFFOs); // will add info in place
        }
        /// <summary>
        /// Display the results of the field validation on the grid, by coloring the cells red
        /// </summary>
        private void DisplayValidation()
        {
            // color the cells based on their validation results
            for(int i = 0; i < CurrentFFOs.Length; i++)
            {
                if(!CurrentFFOs[i].ObjectValidationResult.Valid)
                {
                    // then find the field that is bad and color it red.
                    for(int j = 0; j < CurrentFFOs[i].FieldValidationResults.Count; j++)
                    {
                        if(!(CurrentFFOs[i].FieldValidationResults[j] as ValidationResult).Valid)
                        {
                            dgvResults.Rows[i].Cells[j].Style = errorStyle;
                        }
                    }
                }
            }
        }
        #endregion

        #region Utility Functions
        private void Out(Exception ex)
        {
            Out("Exception: " + ex.Message);
        }
        private void Out(String msg)
        {
            if (tbOutput.Text.Length > 0)
            {
                tbOutput.AppendText(Environment.NewLine);
            }

            tbOutput.AppendText(DateTime.Now.ToString("hh:mm:ss:fff "));
            tbOutput.AppendText(msg);

            // and force the screen to reprint the new message before moving on with processing
            Application.DoEvents();
        }
        #endregion
    }
}
