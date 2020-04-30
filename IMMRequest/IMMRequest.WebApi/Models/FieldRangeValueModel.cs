using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.WebApi.Models
{
    public class FieldRangeValueModel : Model<AdditionalFieldValue, FieldRangeValueModel>
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public Guid AdditionalFieldId { get; set;}
        public string Value { get; set; }

        public FieldRangeValueModel() { }

        public FieldRangeValueModel(AdditionalFieldValue entity)
        {
            SetModel(entity);
        }

        public override AdditionalFieldValue ToEntity() => new AdditionalFieldValue()
        {
            Id = this.Id,
            RequestId = this.RequestId,
            AdditionalFieldId = this.AdditionalFieldId,
            Value = this.Value
        };

        protected override FieldRangeValueModel SetModel(AdditionalFieldValue entity)
        {
            Id = entity.Id;
            RequestId = entity.RequestId;
            AdditionalFieldId = entity.AdditionalFieldId;
            Value = entity.Value;
            
            return this;
        }
    }
}
