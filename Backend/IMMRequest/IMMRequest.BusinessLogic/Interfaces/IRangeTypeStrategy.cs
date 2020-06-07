using System.Collections.Generic;
using IMMRequest.Domain;

namespace IMMRequest.BusinessLogic
{
    public interface IRangeTypeStrategy
    {
        void ValidRangeFormat(AdditionalField additionalField);
        void IsValidRangeValue(AdditionalField additionalField, AdditionalFieldValue value);
        void HasValidRangeValues(AdditionalField additionalField, AdditionalFieldValue value);
    }
}
