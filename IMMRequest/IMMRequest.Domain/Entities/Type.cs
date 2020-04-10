using System.Collections.Generic;
using System;

namespace IMMRequest.Domain
{
    public class Type
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Topic Topic { get; set; }

        public virtual ICollection<AdditionalField> AdditionalFields { get; set; }

        public Type() 
        {
            this.AdditionalFields = new List<AdditionalField>();
        }

        public Type(string Name, Topic Topic, ICollection<AdditionalField> AdditionalFields) 
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
			Type type = obj as Type;
			bool equals = false;
			if (obj == null) {
				equals = false;
			}
			else {
				equals = this.Name == type.Name 
                && this.Topic == type.Topic
                && this.AdditionalFields == type.AdditionalFields;
			}
			return equals;
		}
    }
}
