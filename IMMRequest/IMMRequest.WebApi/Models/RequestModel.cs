using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.WebApi.Models
{
    public class RequestModel : Model<Request, RequestModel>
    {
        public Guid Id { get; set; }
        public string State { get; set; }
        public string RequestorsName { get; set; }
        public string RequestorsEmail { get; set; }
        public string RequestorsPhone { get; set; }
        public string Description { get; set; }
        public Guid TypeId { get; set; }

        public RequestModel() { }

        public RequestModel(Request entity)
        {
            SetModel(entity);
        }

        public override Request ToEntity() => new Request()
        {
            Id = this.Id,
            State = this.State,
            TypeId = this.TypeId,
            Description = this.Description,
            RequestorsName = this.RequestorsName,
            RequestorsEmail = this.RequestorsEmail,
            RequestorsPhone = this.RequestorsPhone
        };

        protected override RequestModel SetModel(Request entity)
        {
            Id = entity.Id;
            State = entity.State;
            TypeId = entity.TypeId;
            Description = entity.Description;
            RequestorsName = entity.RequestorsName;
            RequestorsEmail = entity.RequestorsEmail;
            RequestorsPhone = entity.RequestorsPhone;
            return this;
        }
    }
}
