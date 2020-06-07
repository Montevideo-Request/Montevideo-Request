using System;
using System.Collections.Generic;

namespace IMMRequest.Domain
{
    public class SelectedValues
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public AdditionalFieldValue AdditionalFieldValue { get; set; }
        public Guid AdditionalFieldValueId { get; set; }
        public SelectedValues() { this.Id = Guid.NewGuid(); }

    }
}
