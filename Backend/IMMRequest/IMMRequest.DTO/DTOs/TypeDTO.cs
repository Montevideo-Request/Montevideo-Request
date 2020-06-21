using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DTO
{
    public class TypeDTO : DTO<TypeEntity, TypeDTO>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid TopicId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AdditionalFieldDTO> AdditionalFields { get; set; }

        public TypeDTO() 
        {
            AdditionalFields = new List<AdditionalFieldDTO>();
        }

        public TypeDTO(TypeEntity entity)
        {
            SetModel(entity);
        }

        public override TypeEntity ToEntity() => new TypeEntity()
        {
            Id = this.Id,
            Date = this.Date,
            TopicId = this.TopicId,
            Name = this.Name,
            AdditionalFields = this.AdditionalFields.ToList().ConvertAll(m => m.ToEntity())
        };

        protected override TypeDTO SetModel(TypeEntity entity)
        {
            Id = entity.Id;
            Date = entity.Date;
            TopicId = entity.TopicId;
            Name = entity.Name;
            AdditionalFields = entity.AdditionalFields.ToList().ConvertAll(m => new AdditionalFieldDTO(m));
            return this;
        }
    }
}
