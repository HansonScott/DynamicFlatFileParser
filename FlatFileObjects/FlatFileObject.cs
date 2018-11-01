using System;
using System.Collections;

namespace FlatFileObjects
{
    public class FlatFileObject
    {
        public ValidationResult ObjectValidationResult = new ValidationResult();
        public ArrayList FieldValidationResults = new ArrayList();
    }
}
