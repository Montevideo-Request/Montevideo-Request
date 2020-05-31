using IMMRequest.Exceptions;
using IMMRequest.Domain;

namespace IMMRequest.BusinessLogic
{
    class RangeTypeBoolean : IRangeTypeStrategy
    {
        public void ValidRangeFormat(AdditionalField additionalField)
        {
            return; //No Range Format Validation For Strings.
        }

        public void IsValidRangeValue(AdditionalField additionalField, AdditionalFieldValue additionalFieldValue)
        {
            if (additionalField.Ranges.Count > 0 )
            {
                FieldRange dummyFieldRange = new FieldRange();
                dummyFieldRange.Range = additionalFieldValue.Value;

                if (!additionalField.Ranges.Contains(dummyFieldRange))
                {
                    throw new ExceptionController(LogicExceptions.INVALID_ADDITIONAL_FIELD_RANGES);
                }   
            }
        }
    }  
}

