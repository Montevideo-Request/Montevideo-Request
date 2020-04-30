using System.Collections.Generic;
using System.Text.RegularExpressions;
using IMMRequest.Domain;
using IMMRequest.Exceptions;

namespace IMMRequest.BusinessLogic.Interface
{
    class RangeTypeInteger : IRangeTypeStrategy  
    {
        public void ValidateRanges(string FieldType, ICollection<FieldRange> ranges)
        {
            foreach(FieldRange range in ranges)
            {
                if(!Regex.IsMatch(range.Range, @"^\d+$"))
                {
                    throw new ExceptionController(LogicExceptions.WRONG_INTEGER_FORMAT);
                }
            }
        }
    }  
}

