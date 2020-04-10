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
            this.Ranges = new List<FieldRange>();
        }
    }
}
