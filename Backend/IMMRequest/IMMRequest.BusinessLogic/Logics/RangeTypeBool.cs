using IMMRequest.Exceptions;
using IMMRequest.Domain;
using System.Linq;
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
            if (additionalFieldValue.Values.Count > 1)
            {
                throw new ExceptionController(LogicExceptions.INVALID_MULTISELECTION);
            }
            
            Boolean boolValue;
            if(!Boolean.TryParse(additionalFieldValue.Values.ToList().First().Value, out boolValue))
            {
                throw new ExceptionController(LogicExceptions.INVALID_BOOLEAN);
            }
        }

        public void HasValidRangeValues(AdditionalField additionalField, AdditionalFieldValue additionalFieldValue)
        {
            IsValidRangeValue(additionalField, additionalFieldValue);
        }
    }  
}

