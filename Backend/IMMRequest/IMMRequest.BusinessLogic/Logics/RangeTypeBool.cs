using IMMRequest.Exceptions;
using IMMRequest.Domain;
using System;

namespace IMMRequest.BusinessLogic
{
    class RangeTypeBoolean : IRangeTypeStrategy
    {
        public void ValidRangeFormat(AdditionalField additionalField)
        {
            if(additionalField.Ranges.Count > 0)
            {
                throw new ExceptionController(LogicExceptions.INVALID_BOOLEAN_RANGE);
            }
        }

        public void IsValidRangeValue(AdditionalField additionalField, AdditionalFieldValue additionalFieldValue)
        {
            Boolean boolValue;
            if(!Boolean.TryParse(additionalFieldValue.Value, out boolValue))
            {
                throw new ExceptionController(LogicExceptions.INVALID_BOOLEAN);
            }
        }
    }  
}

