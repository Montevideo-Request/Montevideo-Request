using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DTO
{
    public class FieldRangeValueDTO : DTO<AdditionalFieldValue, FieldRangeValueDTO>
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public Guid AdditionalFieldId { get; set;}
        public ICollection<SelectedValuesDTO> Values { get; set; }

        public FieldRangeValueDTO() { Values = new List<SelectedValuesDTO>(); }

        public FieldRangeValueDTO(AdditionalFieldValue entity)
        {
            SetModel(entity);
        }

        public override AdditionalFieldValue ToEntity() => new AdditionalFieldValue()
        {
            Id = this.Id,
            RequestId = this.RequestId,
            AdditionalFieldId = this.AdditionalFieldId,
            Values = this.Values.ToList().ConvertAll(m => m.ToEntity())
        };

        protected override FieldRangeValueDTO SetModel(AdditionalFieldValue entity)
        {
            Id = entity.Id;
            RequestId = entity.RequestId;
            AdditionalFieldId = entity.AdditionalFieldId;
            Values = entity.Values.ToList().ConvertAll( m => new SelectedValuesDTO(m) );
            
            return this;
        }
    }
}
