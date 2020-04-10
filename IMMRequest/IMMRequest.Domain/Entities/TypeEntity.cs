using System.Collections.Generic;
using System;

namespace IMMRequest.Domain
{
    public class TypeEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Topic Topic { get; set; }
        public Guid TopicId { get; set; }
        public virtual ICollection<AdditionalField> AdditionalFields { get; set; }

        public TypeEntity() 
        {
            this.AdditionalFields = new List<AdditionalField>();
        }
    }
}
