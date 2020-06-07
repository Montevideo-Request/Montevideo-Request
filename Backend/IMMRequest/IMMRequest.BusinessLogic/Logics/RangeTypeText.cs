using IMMRequest.Exceptions;
using System.Linq;
using IMMRequest.Domain;

namespace IMMRequest.BusinessLogic
{
    class RangeTypeText : IRangeTypeStrategy
    {
        public void ValidRangeFormat(AdditionalField additionalField)
        {
            return; //No Range Format Validation For Strings.
        }

        public void IsValidRangeValue(AdditionalField additionalField, AdditionalFieldValue additionalFieldValue)
        {
            
            if (additionalFieldValue.Values.Count > 1)
            {
                throw new ExceptionController(LogicExceptions.INVALID_MULTISELECTION);
            }

            if (additionalField.Ranges.Count > 0 )
            {
                FieldRange field = new FieldRange();
                field.Range = additionalFieldValue.Values.ToList().First().Value;

                if (!additionalField.Ranges.Contains(field))
                {
                    throw new ExceptionController(LogicExceptions.INVALID_ADDITIONAL_FIELD_RANGES);
                }   
            }
        }

        public void HasValidRangeValues(AdditionalField additionalField, AdditionalFieldValue additionalFieldValue)
        {
            if (additionalField.Ranges.Count > 0 && additionalFieldValue.Values.Count > 0)
            {
                foreach (SelectedValues selection in additionalFieldValue.Values)
                {
                    FieldRange field = new FieldRange();
                    field.Range = selection.Value;

                    if (!additionalField.Ranges.Contains(field))
                    {
                        throw new ExceptionController(LogicExceptions.INVALID_ADDITIONAL_FIELD_RANGES);
                    }
                }   
            }
            else
            {
                throw new ExceptionController(LogicExceptions.INVALID_SELECTION);
            }
        }
    }  
}

