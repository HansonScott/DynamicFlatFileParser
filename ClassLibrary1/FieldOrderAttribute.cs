using System;

namespace AttributeUtilities
{
    public class FieldOrderAttribute: Attribute
    {
        public int FieldOrder;

        public FieldOrderAttribute(int Order)
        {
            this.FieldOrder = Order;
        }
    }
}
