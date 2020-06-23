using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DTO
{
    public class RequestDTO : DTO<Request, RequestDTO>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string State { get; set; }
        public string RequestorsName { get; set; }
        public string RequestorsEmail { get; set; }
        public string RequestorsPhone { get; set; }
        public string Description { get; set; }
        public Guid TypeId { get; set; }
        public TypeDTO Type { get; set; }
        public ICollection<FieldRangeValueDTO> AdditionalFieldValues { get; set; }

        public RequestDTO() 
        {
            AdditionalFieldValues = new List<FieldRangeValueDTO>();
        }

        public RequestDTO(Request entity)
        {
            SetModel(entity);
        }

        public override Request ToEntity() => new Request()
        {
            Id = this.Id,
            Date = this.Date,
            State = this.State,
            TypeId = this.TypeId,
            Description = this.Description,
            RequestorsName = this.RequestorsName,
            RequestorsEmail = this.RequestorsEmail,
            RequestorsPhone = this.RequestorsPhone,
            AdditionalFieldValues = this.AdditionalFieldValues.ToList().ConvertAll(m => m.ToEntity())
        };

        protected override RequestDTO SetModel(Request entity)
        {
            Id = entity.Id;
            Date = entity.Date;
            State = entity.State;
            TypeId = entity.TypeId;
            Description = entity.Description;
            Type = new TypeDTO(entity.Type);
            RequestorsName = entity.RequestorsName;
            RequestorsEmail = entity.RequestorsEmail;
            RequestorsPhone = entity.RequestorsPhone;
            AdditionalFieldValues = entity.AdditionalFieldValues.ToList().ConvertAll(m => new FieldRangeValueDTO(m));
            return this;
        }
    }
}
