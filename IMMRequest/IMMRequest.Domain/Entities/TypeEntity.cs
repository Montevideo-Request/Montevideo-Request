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
            this.Id = Guid.NewGuid();
            this.AdditionalFields = new List<AdditionalField>();
        }

        public TypeEntity(string Name, Topic Topic, ICollection<AdditionalField> AdditionalFields) 
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Topic = Topic;
            this.AdditionalFields = AdditionalFields;
        }

        public bool IsValid() 
        {
            return true;
        }

        public override bool Equals(Object obj) 
        {
			TypeEntity type = obj as TypeEntity;
			bool equals = false;
			if (obj == null) {
				equals = false;
			}
			else {
				equals = this.Name == type.Name && this.Topic == type.Topic;
			}
			return equals;
		}
    }
}
