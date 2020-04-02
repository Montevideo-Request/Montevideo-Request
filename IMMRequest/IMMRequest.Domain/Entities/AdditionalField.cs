using System.Collections.Generic;

namespace IMMRequest.Domain
{
    public class AdditionalField
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string FieldType { get; set; }
        public Type Type { get; set; }

        public virtual ICollection<FieldRange> Ranges { get; set; }

        public AdditionalField()
        {
            this.Ranges = new List<FieldRange>();
        }
    }
}
