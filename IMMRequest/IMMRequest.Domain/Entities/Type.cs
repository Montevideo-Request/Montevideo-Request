using System.Collections.Generic;

namespace IMMRequest.Domain
{
    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Topic Topic { get; set; }

        public virtual ICollection<AdditionalField> AdditionalFields { get; set; }

        public Type() 
        {
            this.AdditionalFields = new List<AdditionalField>();
        }
    }
}
