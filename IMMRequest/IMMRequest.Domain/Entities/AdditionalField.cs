using System.Collections.Generic;

namespace IMMRequest.Domain
{
    public class AdditionalField
    {
        public string Name { get; set; }

        public string FildType { get; set; }

        public List<string> Range { get; set; }

        public AdditionalField()
        {
            Range = new List<string>();
        }
    }
}