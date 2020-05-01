using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.WebApi.Models
{
    public class AdditionalFieldModel : Model<AdditionalField, AdditionalFieldModel>
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public string Name { get; set; }
        public string FieldType { get; set; }
        public virtual ICollection<FieldRangeModel> Ranges { get; set; }

        public AdditionalFieldModel() 
        {
            Ranges = new List<FieldRangeModel>();
        }

        public AdditionalFieldModel(AdditionalField entity)
        {
            SetModel(entity);
        }

        public override AdditionalField ToEntity() => new AdditionalField()
        {
            Id = this.Id,
            TypeId = this.TypeId,
            Name = this.Name,
            FieldType = this.FieldType,
            Ranges = this.Ranges.ToList().ConvertAll(m => m.ToEntity())
        };

        protected override AdditionalFieldModel SetModel(AdditionalField entity)
        {
            Id = entity.Id;
            TypeId = entity.TypeId;
            Name = entity.Name;
            FieldType = entity.FieldType;
            Ranges = entity.Ranges.ToList().ConvertAll(m => new FieldRangeModel(m));
            return this;
        }
    }
}
