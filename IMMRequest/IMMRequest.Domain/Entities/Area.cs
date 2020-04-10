using System;
using System.Collections.Generic;

namespace IMMRequest.Domain
{
    public class Area
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public virtual ICollection<Topic> Topics { get; set; }

        public Area() 
        {
            this.Topics = new List<Topic>();
        }

        public Area(string Name, string Email, ICollection<Topic> Topics) 
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
				equals = this.Name == area.Name && this.Topics == area.Topics;
			}
			return equals;
		}

    }
}
