using IMMRequest.Domain;
using System;

namespace IMMRequest.DTO
{
    public class SelectedValuesDTO : DTO<SelectedValues, SelectedValuesDTO>
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public Guid AdditionalFieldValueId { get; set; }
    
        public SelectedValuesDTO() { }

        public SelectedValuesDTO(SelectedValues entity)
        {
            SetModel(entity);
        }

        public override SelectedValues ToEntity() => new SelectedValues()
        {
            Id = this.Id,
            AdditionalFieldValueId = this.AdditionalFieldValueId,
            Value = this.Value
        };

        protected override SelectedValuesDTO SetModel(SelectedValues entity)
        {
            Id = entity.Id;
            AdditionalFieldValueId = entity.AdditionalFieldValueId;
            Value = entity.Value;
            
            return this;
        }
    }
}
