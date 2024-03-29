using System;
using System.Collections.Generic;

namespace IMMRequest.Domain
{
    public class FieldRange
    {
        public Guid Id { get; set; }
        public string Range { get; set; }
        public AdditionalField AdditionalField { get; set; }
        public Guid AdditionalFieldId { get; set; }
        public FieldRange() { this.Id = Guid.NewGuid(); }
    }
}
