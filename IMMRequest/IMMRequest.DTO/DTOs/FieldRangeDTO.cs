using IMMRequest.Domain;
using System;

namespace IMMRequest.DTO
{
    public class FieldRangeDTO : DTO<FieldRange, FieldRangeDTO>
    {
        public Guid Id { get; set; }
        public Guid AdditionalFieldId { get; set; }
        public string Range { get; set; }
    

        public FieldRangeDTO() { }

        public FieldRangeDTO(FieldRange entity)
        {
            SetModel(entity);
        }

        public override FieldRange ToEntity() => new FieldRange()
        {
            Id = this.Id,
            AdditionalFieldId = this.AdditionalFieldId,
            Range = this.Range
        };

        protected override FieldRangeDTO SetModel(FieldRange entity)
        {
            Id = entity.Id;
            AdditionalFieldId = entity.AdditionalFieldId;
            Range = entity.Range;
            return this;
        }
    }
}
