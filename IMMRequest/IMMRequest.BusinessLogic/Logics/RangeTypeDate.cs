using System;
using System.Collections.Generic;
using System.Globalization;
using IMMRequest.Domain;
using IMMRequest.Exceptions;

namespace IMMRequest.BusinessLogic.Interface
{
    class RangeTypeDate : IRangeTypeStrategy  
    {
        public const string DATE = "Fecha";
        public void ValidRangeFormat(AdditionalField additionalField)
        {
            foreach(FieldRange range in additionalField.Ranges)
            {
                string[] formats = { "MM/dd/yyyy" };
                DateTime parsedDateTime;
                bool isDate = DateTime.TryParseExact(range.Range, formats, new CultureInfo("en-US"),
                                            DateTimeStyles.None, out parsedDateTime);
                if(!isDate)
                {
                    throw new ExceptionController(LogicExceptions.WRONG_DATE_FORMAT);
                }
            }
        }
    }  
}

