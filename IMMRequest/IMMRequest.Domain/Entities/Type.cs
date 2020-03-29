using System.Collections.Generic;

namespace IMMRequest.Domain
{
    public class Type
    {
        public string Name { get; set; }

        public Topic Topic { get; set; }

        public List<AdditionalField> AdditionalFields { get; set; }

        public Type() 
        {
            AdditionalFields = new List<AdditionalField>();
        }
    }
}