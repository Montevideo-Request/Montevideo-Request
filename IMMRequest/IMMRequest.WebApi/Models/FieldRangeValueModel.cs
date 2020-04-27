using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.WebApi.Models
{
    public class FieldRangeValueModel : Model<FieldRangeValue, FieldRangeValueModel>
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public Guid AdditionalFieldId { get; set;}
        public string Value { get; set; }

        public FieldRangeValueModel() { }

        public FieldRangeValueModel(FieldRangeValue entity)
        {
            SetModel(entity);
        }

        public override FieldRangeValue ToEntity() => new FieldRangeValue()
        {
            Id = this.Id,
            RequestId = this.RequestId,
            AdditionalFieldId = this.AdditionalFieldId,
            Value = this.Value
        };

        protected override FieldRangeValueModel SetModel(FieldRangeValue entity)
        {
            Id = entity.Id;
            RequestId = entity.RequestId;
            AdditionalFieldId = entity.AdditionalFieldId;
            Value = entity.Value;
            
            return this;
        }
    }
}
