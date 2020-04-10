using System;
using System.Collections.Generic;

namespace IMMRequest.Domain
{
    public class Topic
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        public Area Area { get; set; }
        
        public virtual ICollection<Type> Types { get; set; }

        public Topic() 
        {
            this.Types = new List<Type>();
        }

        public Topic(string Name, Area Area) 
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Area = Area;
        }

        public bool IsValid() 
        {
            return true;
        }

        public override bool Equals(Object obj) 
        {
			Topic topic = obj as Topic;
			bool equals = false;
			if (obj == null) {
				equals = false;
			}
			else {
				equals = this.Name == topic.Name && this.Area == topic.Area;
			}
			return equals;
		}
    }
}
