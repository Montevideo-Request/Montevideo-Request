using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DTO
{
    public class TopicDTO : DTO<Topic, TopicDTO>
    {
        public Guid Id { get; set; }
        public Guid AreaId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TypeDTO> Types { get; set; }

        public TopicDTO() 
        {
            Types = new List<TypeDTO>();
        }

        public TopicDTO(Topic entity)
        {
            SetModel(entity);
        }

        public override Topic ToEntity() => new Topic()
        {
            Id = this.Id,
            AreaId = this.AreaId,
            Name = this.Name,
            Types = this.Types.ToList().ConvertAll(m => m.ToEntity())
        };

        protected override TopicDTO SetModel(Topic entity)
        {
            Id = entity.Id;
            AreaId = entity.AreaId;
            Name = entity.Name;
            Types = entity.Types.ToList().ConvertAll(m => new TypeDTO(m));
            return this;
        }
    }
}
