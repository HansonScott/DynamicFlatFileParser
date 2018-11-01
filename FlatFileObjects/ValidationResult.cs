using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileObjects
{
    public class ValidationResult
    {
        public bool Valid = true;
        public string ObjectIdentifier; // generic for the propertyName, or object ID, etc.
        public string ValidationCriteria;
        public string ValidationMessage;
        public Exception ValidationException;
    }
}
