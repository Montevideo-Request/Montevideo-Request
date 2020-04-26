using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.WebApi.Models
{
    public class TypeModel : Model<TypeEntity, TypeModel>
    {
        public Guid Id { get; set; }
        public Guid TopicId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AdditionalFieldModel> AdditionalFields { get; set; }

        public TypeModel() 
        {
            AdditionalFields = new List<AdditionalFieldModel>();
        }

        public TypeModel(TypeEntity entity)
        {
            SetModel(entity);
        }

        public override TypeEntity ToEntity() => new TypeEntity()
        {
            Id = this.Id,
            TopicId = this.TopicId,
            Name = this.Name,
            AdditionalFields = this.AdditionalFields.ToList().ConvertAll(m => m.ToEntity())
        };

        protected override TypeModel SetModel(TypeEntity entity)
        {
            Id = entity.Id;
            TopicId = entity.TopicId;
            Name = entity.Name;
            AdditionalFields = entity.AdditionalFields.ToList().ConvertAll(m => new AdditionalFieldModel(m));
            return this;
        }
    }
}
