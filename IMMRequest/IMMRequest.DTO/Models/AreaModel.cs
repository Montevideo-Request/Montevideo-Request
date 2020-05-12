using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DTO
{
    public class AreaModel : Model<Area, AreaModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TopicModel> Topics { get; set; }

        public AreaModel() 
        {
            this.Topics = new List<TopicModel>();
        }

        public AreaModel(Area entity)
        {
            SetModel(entity);
        }

        public override Area ToEntity() => new Area()
        {
            Id = this.Id,
            Name = this.Name,
            Topics = this.Topics.ToList().ConvertAll(m => m.ToEntity())
        };

        protected override AreaModel SetModel(Area entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Topics = entity.Topics.ToList().ConvertAll(m => new TopicModel(m));
            return this;
        }
    }
}
