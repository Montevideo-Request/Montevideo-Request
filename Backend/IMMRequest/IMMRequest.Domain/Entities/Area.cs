using System.Collections.Generic;
using System.Xml.Serialization;
using System;

namespace IMMRequest.Domain
{
    public class Area
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Boolean IsDeleted { get; set; }
        [XmlIgnoreAttribute]
        public virtual ICollection<Topic> Topics { get; set; }
        public Area() 
        {
            this.Id = Guid.NewGuid();
            this.Topics = new List<Topic>();
            this.IsDeleted = false;
        }
        public Area(string Name, List<Topic> Topics)
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Topics = Topics;
            this.IsDeleted = false;
        }
    }
}
