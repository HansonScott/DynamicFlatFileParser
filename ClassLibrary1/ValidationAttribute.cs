using System;

namespace AttributeUtilities
{
    public class ValidationAttribute : Attribute
    {
        public string RegEx;
        public bool AllowNull = false;

        public ValidationAttribute(String RegEx) : this(RegEx, false) { }
        public ValidationAttribute(String RegEx, bool AllowNull)
        {
            this.RegEx = RegEx;
            this.AllowNull = AllowNull;
        }
    }
}