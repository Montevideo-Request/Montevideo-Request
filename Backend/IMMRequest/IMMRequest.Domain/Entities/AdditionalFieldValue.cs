using System.Collections.Generic;
using System;

namespace IMMRequest.Domain
{
    public class AdditionalFieldValue
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public Request Request { get; set; }
        public AdditionalField AdditionalField { get; set;}
        public Guid AdditionalFieldId { get; set;}
        public ICollection<SelectedValues> Values { get; set; }
        public AdditionalFieldValue () 
        { 
            this.Id = Guid.NewGuid(); 
            this.Values = new List<SelectedValues>();
        }
    }
}
