using System.Collections.Generic;
using System.Text.RegularExpressions;
using IMMRequest.Domain;
using IMMRequest.Exceptions;

namespace IMMRequest.BusinessLogic.Interface
{
    class RangeTypeInteger : IRangeTypeStrategy  
    {
        public void ValidRangeFormat(AdditionalField additionalField)
        {
            foreach(FieldRange range in additionalField.Ranges)
            {
                if(!Regex.IsMatch(range.Range, @"^\d+$"))
                {
                    throw new ExceptionController(LogicExceptions.WRONG_INTEGER_FORMAT);
                }
            }
        }
    }  
}

