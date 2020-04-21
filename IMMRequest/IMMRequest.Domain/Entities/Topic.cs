using System.Collections.Generic;
using System;

namespace IMMRequest.Domain
{
    public class Topic
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        public Area Area { get; set; }
        public Guid AreaId  { get; set; }
        public virtual ICollection<TypeEntity> Types { get; set; }

        public Topic() 
        {
            this.Id = Guid.NewGuid();
            this.Types = new List<TypeEntity>();
        }
        public Topic(string Name, Area Area) 
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Area = Area;
        }
        public Topic(string Name, Area Area,List<TypeEntity> Types)
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Types = Types;
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
				equals = this.Name == topic.Name && this.AreaId == topic.AreaId;
			}
			return equals;
		}
    }
}
