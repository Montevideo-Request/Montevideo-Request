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
        public Boolean MultiSelect { get; set; }
        public Boolean IsDeleted { get; set; }
        public virtual ICollection<FieldRange> Ranges { get; set; }

        public AdditionalField()
        {
            this.Id = Guid.NewGuid();
            this.Ranges = new List<FieldRange>();
            this.MultiSelect = false;
            this.IsDeleted = false;
        }

        public AdditionalField(string Name, string FieldType, Type Type, ICollection<FieldRange> Ranges, Boolean MultiSelect)
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.FieldType = FieldType;
            this.Ranges = Ranges;
            this.MultiSelect = MultiSelect;
            this.IsDeleted = false;
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
