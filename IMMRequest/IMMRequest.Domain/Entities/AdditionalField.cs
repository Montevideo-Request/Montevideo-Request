using System.Collections.Generic;

namespace IMMRequest.Domain
{
    public class AdditionalField
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string FildType { get; set; }

        public List<AdditionalFieldRange> Range { get; set; }

        public AdditionalField()
        {
            Range = new List<AdditionalFieldRange>();
        }
    }
}
