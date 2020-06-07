using System.Text.RegularExpressions;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
using System.Collections.Generic;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic
{
    class RangeTypeInteger : IRangeTypeStrategy  
    {
        public void ValidRangeFormat(AdditionalField additionalField)
        {
            foreach(FieldRange range in additionalField.Ranges)
            {
                if(!Regex.IsMatch(range.Range, @"^\d+$"))
                {
                    throw new ExceptionController(LogicExceptions.WRONG_INTEGER_FORMAT + "for the following additional field: " + additionalField.Id);
                }
            }

            if (additionalField.Ranges.Count > 0)
            {
                /* Valid Date Range */
                if (additionalField.Ranges.Count != 2)
                {
                    throw new ExceptionController(LogicExceptions.INVALID_NUMBER_RANGE);
                }

                var FromToNumbers = ConvertRangesToInts(additionalField);

                /* Valid Number Range */
                if (FromToNumbers[0] > FromToNumbers[1])
                {
                    throw new ExceptionController(LogicExceptions.INVALID_NUMBER_RANGE);
                }
            }
        }

        public void IsValidRangeValue(AdditionalField additionalField, AdditionalFieldValue additionalFieldValue)
        {
            if (additionalFieldValue.Values.Count > 1)
            {
                throw new ExceptionController(LogicExceptions.INVALID_MULTISELECTION);
            }

            /* Validate Value Within Ranges */
            var incomingNumber = ConvertStringToInt(additionalFieldValue.Values.ToList().First().Value);
            
            if (additionalField.Ranges.Count > 0 ) 
            {
                var FromToNumbers = ConvertRangesToInts(additionalField);
                
                if (!(incomingNumber >= FromToNumbers[0] && incomingNumber <= FromToNumbers[1]))
                {
                    throw new ExceptionController(LogicExceptions.NUMBER_OUT_OF_RANGE + " for the following additional field: " + additionalField.Id);
                }   
            }
        }

        public void HasValidRangeValues(AdditionalField additionalField, AdditionalFieldValue additionalFieldValue)
        {
            if (additionalField.Ranges.Count > 0 && additionalFieldValue.Values.Count > 0)
            {
                foreach (SelectedValues selection in additionalFieldValue.Values)
                {
                    /* Validate Value Within Ranges */
                    var incomingNumber = ConvertStringToInt(selection.Value);
                    var FromToNumbers = ConvertRangesToInts(additionalField);
                    
                    if (!(incomingNumber >= FromToNumbers[0] && incomingNumber <= FromToNumbers[1]))
                    {
                        throw new ExceptionController(LogicExceptions.NUMBER_OUT_OF_RANGE + " for the following additional field: " + additionalField.Id);
                    }   
                }
            }
            else
            {
                throw new ExceptionController(LogicExceptions.INVALID_SELECTION);
            }
        }

        public List<int> ConvertRangesToInts(AdditionalField additionalField)
        {
            var FromToNumbers = new List<int>();
            try
            {
                foreach(FieldRange range in additionalField.Ranges)
                {
                    int convertedInt = int.Parse(range.Range);
                    FromToNumbers.Add(convertedInt);
                }    
            }

            catch (System.Exception)
            {
                throw new ExceptionController(LogicExceptions.WRONG_INTEGER_FORMAT + " for the following additional field: " + additionalField.Id);
            }

            return FromToNumbers;
        }

        public int ConvertStringToInt(string intField)
        {
            try
            {
                if(!Regex.IsMatch(intField, @"^\d+$"))
                {
                    throw new ExceptionController(LogicExceptions.WRONG_INTEGER_FORMAT);
                }
                
                int convertedInt = int.Parse(intField);
                return convertedInt;
            }
            catch (System.Exception)
            {
                throw new ExceptionController(LogicExceptions.WRONG_INTEGER_FORMAT);
            }
        }
    }  
}

