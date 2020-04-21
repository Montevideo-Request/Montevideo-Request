using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.WebApi.Models
{
    public class AdditionalFieldModel : Model<AdditionalField, AdditionalFieldModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
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
            Name = this.Name,
            Ranges = this.Ranges.ToList().ConvertAll(m => m.ToEntity())
        };

        protected override AdditionalFieldModel SetModel(AdditionalField entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Ranges = entity.Ranges.ToList().ConvertAll(m => new FieldRangeModel(m));
            return this;
        }
    }
}
