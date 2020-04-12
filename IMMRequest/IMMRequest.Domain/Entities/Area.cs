using System;
using System.Collections.Generic;
using System;

namespace IMMRequest.Domain
{
    public class Area
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }

        public Area() 
        {
            this.Id = Guid.NewGuid();
            this.Topics = new List<Topic>();
        }

        public Area(string Name, List<Topic> Topics)
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Topics = Topics;
        }

        public bool IsValid() 
        {
            return true;
        }

        public override bool Equals(Object obj) 
        {
			Area area = obj as Area;
			bool equals = false;
			if (obj == null) {
				equals = false;
			}
			else {
				equals = this.Name == area.Name;
			}
			return equals;
		}
    }
}
