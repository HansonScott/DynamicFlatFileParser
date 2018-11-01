using AttributeUtilities;
using System;

namespace User
{
    public class User02: FlatFileObjects.FlatFileObject
    {
        [FieldOrderAttribute(1)]
        [ValidationAttribute("^[a-zA-Z']{1,50}$")]
        public string FirstName { get; set; }

        [FieldOrderAttribute(2)]
        [ValidationAttribute("^[a-zA-Z']{1,50}$")]
        public string MiddleName { get; set; }

        [FieldOrderAttribute(3)]
        [ValidationAttribute("^[a-zA-Z']{1,50}$")]
        public string LastName { get; set; }

        [FieldOrderAttribute(4)]
        [ValidationAttribute("^[a-zA-Z']{1,50}$", true)]
        public DateTime? DOB { get; set; }
    }
}
