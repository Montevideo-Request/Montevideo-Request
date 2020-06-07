using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DTO
{
    public class AdditionalFieldDTO : DTO<AdditionalField, AdditionalFieldDTO>
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public string Name { get; set; }
        public string FieldType { get; set; }
        public Boolean MultiSelect { get; set; }
        public virtual ICollection<FieldRangeDTO> Ranges { get; set; }

        public AdditionalFieldDTO() 
        {
            Ranges = new List<FieldRangeDTO>();
        }

        public AdditionalFieldDTO(AdditionalField entity)
        {
            SetModel(entity);
        }

        public override AdditionalField ToEntity() => new AdditionalField()
        {
            Id = this.Id,
            TypeId = this.TypeId,
            Name = this.Name,
            FieldType = this.FieldType,
            MultiSelect = this.MultiSelect ? this.MultiSelect : false,
            Ranges = this.Ranges.ToList().ConvertAll(m => m.ToEntity())
        };

        protected override AdditionalFieldDTO SetModel(AdditionalField entity)
        {
            Id = entity.Id;
            TypeId = entity.TypeId;
            Name = entity.Name;
            FieldType = entity.FieldType;
            MultiSelect = entity.MultiSelect;
            Ranges = entity.Ranges.ToList().ConvertAll(m => new FieldRangeDTO(m));
            return this;
        }
    }
}
