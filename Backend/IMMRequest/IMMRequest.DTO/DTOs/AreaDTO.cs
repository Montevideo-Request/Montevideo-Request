using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DTO
{
    public class AreaDTO : DTO<Area, AreaDTO>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TopicDTO> Topics { get; set; }

        public AreaDTO() 
        {
            this.Topics = new List<TopicDTO>();
        }

        public AreaDTO(Area entity)
        {
            SetModel(entity);
        }

        public override Area ToEntity() => new Area()
        {
            Id = this.Id,
            Name = this.Name,
            Topics = this.Topics.ToList().ConvertAll(m => m.ToEntity())
        };

        protected override AreaDTO SetModel(Area entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Topics = entity.Topics.ToList().ConvertAll(m => new TopicDTO(m));
            return this;
        }
    }
}
