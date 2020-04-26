using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.WebApi.Models
{
    public class FieldRangeModel : Model<FieldRange, FieldRangeModel>
    {
        public Guid Id { get; set; }
        public Guid AdditionalFieldId { get; set; }
        public string Range { get; set; }
    

        public FieldRangeModel() { }

        public FieldRangeModel(FieldRange entity)
        {
            SetModel(entity);
        }

        public override FieldRange ToEntity() => new FieldRange()
        {
            Id = this.Id,
            AdditionalFieldId = this.AdditionalFieldId,
            Range = this.Range
        };

        protected override FieldRangeModel SetModel(FieldRange entity)
        {
            Id = entity.Id;
            AdditionalFieldId = entity.AdditionalFieldId;
            Range = entity.Range;
            return this;
        }
    }
}
