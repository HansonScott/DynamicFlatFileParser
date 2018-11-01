using AttributeUtilities;
using System;

namespace User
{
    public class User01: FlatFileObjects.FlatFileObject
    {
        [FieldOrderAttribute(1)]
        [ValidationAttribute("^[a-zA-Z']{1,50}$")]
        public string FirstName { get; set; }

        [FieldOrderAttribute(2)]
        [ValidationAttribute("^[a-zA-Z']{1,50}$")]
        public string LastName { get; set; }

        [FieldOrderAttribute(3)]
        [ValidationAttribute("^[a-zA-Z']{1,50}$", true)]
        public DateTime? DOB { get; set; }
    }
}
