using System;
using System.Collections.Generic;

namespace IMMRequest.Domain
{
    public class FieldRange
    {
        public Guid Id { get; set; }
        public string Range { get; set; }
        public AdditionalField AdditionalField { get; set; }
        public Guid AdditionalFieldId { get; set; }
        public FieldRange() { }

        public override bool Equals(Object obj)
        {
            FieldRange fieldRange = obj as FieldRange;
            bool equals = false;
            if (obj == null)
            {
                equals = false;
            }
            else
            {
                equals = this.Range == fieldRange.Range;
            }
            return equals;
        }
    }
}
