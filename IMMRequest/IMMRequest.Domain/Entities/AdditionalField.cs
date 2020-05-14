using System.Collections.Generic;
using System;

namespace IMMRequest.Domain
{
    public class AdditionalField
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FieldType { get; set; }
        public TypeEntity Type { get; set; }
        public Guid TypeId { get; set; }
        public virtual ICollection<FieldRange> Ranges { get; set; }

        public AdditionalField()
        {
            this.Id = Guid.NewGuid();
            this.Ranges = new List<FieldRange>();
        }

        public AdditionalField(string Name, string FieldType, Type Type, ICollection<FieldRange> Ranges)
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.FieldType = FieldType;
            this.Ranges = Ranges;
        }

        public override bool Equals(Object obj)
        {
            AdditionalField additionalField = obj as AdditionalField;
            bool equals = false;
            if (obj == null)
            {
                equals = false;
            }
            else
            {
                equals = this.Name == additionalField.Name;
            }
            return equals;
        }

    }
}
