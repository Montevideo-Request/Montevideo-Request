using System.Text.RegularExpressions;
using IMMRequest.Exceptions;
using IMMRequest.Domain;

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
                    throw new ExceptionController(LogicExceptions.WRONG_INTEGER_FORMAT);
                }
            }
        }
    }  
}

