using System.Collections.Generic;
using System;

namespace IMMRequest.Domain
{
    public class Request
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string State { get; set; }
        public string RequestorsName { get; set; }
        public string RequestorsEmail { get; set; }
        public string RequestorsPhone { get; set; }
        public string Description { get; set; }
        public TypeEntity Type { get; set; }
        public Guid TypeId { get; set; }
        public ICollection<AdditionalFieldValue> AdditionalFieldValues { get; set; }

        public Request()
        {
            this.Id = Guid.NewGuid();
            this.AdditionalFieldValues = new List<AdditionalFieldValue>();
        }

        public Request(string RequestorsName, string RequestorsEmail, string RequestorsPhone, 
        TypeEntity Type, string Description, List<AdditionalFieldValue> AdditionalFieldValues) 
        {
            this.Id = Guid.NewGuid();
            this.RequestorsName = RequestorsName;
            this.RequestorsEmail = RequestorsEmail;
            this.RequestorsPhone = RequestorsPhone;
            this.Type = Type;
            this.Description = Description;
            this.AdditionalFieldValues = AdditionalFieldValues;
        }
    }
}
