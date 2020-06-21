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
        public Boolean IsDeleted { get; set; }
        public virtual ICollection<TypeEntity> Types { get; set; }

        public Topic() 
        {
            this.Id = Guid.NewGuid();
            this.Types = new List<TypeEntity>();
            this.IsDeleted = false;
        }
        public Topic(string Name, Area Area) 
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Area = Area;
            this.IsDeleted = false;
        }
        public Topic(string Name, Area Area,List<TypeEntity> Types)
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Types = Types;
            this.IsDeleted = false;
        }
    }
}
