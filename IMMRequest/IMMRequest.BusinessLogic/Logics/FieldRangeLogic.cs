using IMMRequest.BusinessLogic.Interface;
using IMMRequest.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace IMMRequest.BusinessLogic
{
    public class FieldRangeLogic
    {
        public const string TEXT = "Texto"; //default
        public const string DATE = "Fecha";
        public const string INTEGER = "Entero";
        public static void ValidateRanges(string FieldType, ICollection<FieldRange> ranges)
        {
            foreach(FieldRange range in ranges)
            {
                if (FieldType == DATE)
                {
                    string[] formats = { "MM/dd/yyyy" };
                    DateTime parsedDateTime;
                    bool isDate = DateTime.TryParseExact(range.Range, formats, new CultureInfo("en-US"),
                                                DateTimeStyles.None, out parsedDateTime);
                    if(!isDate)
                    {
                        throw new ExceptionController(ExceptionMessage.WRONG_DATE_FORMAT);
                    }
                }
                else if (FieldType == INTEGER)
                {
                    if(!Regex.IsMatch(range.Range, @"^\d+$"))
                    {
                        throw new ExceptionController(ExceptionMessage.WRONG_INTEGER_FORMAT);
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}
