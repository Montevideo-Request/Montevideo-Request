using System.Collections.Generic;
using IMMRequest.Exceptions;
using System.Globalization;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic
{
    class RangeTypeDate : IRangeTypeStrategy  
    {
        public const string DATE = "Fecha";
        public void ValidRangeFormat(AdditionalField additionalField)
        {
            if (additionalField.Ranges.Count > 0)
            {
                 var FromToDates = ConvertRangesToDates(additionalField);

                /* Valid Date Range */
                if (additionalField.Ranges.Count != 2)
                {
                    throw new ExceptionController(LogicExceptions.INVALID_DATE_RANGE);
                }

                /* Valid Date Range */
                if (FromToDates[0] > FromToDates[1])
                {
                    throw new ExceptionController(LogicExceptions.INVALID_DATE_RANGE);
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
            DateTime incomingDate = ConvertStringToDate(additionalFieldValue.Values.ToList().First().Value);
            var FromToDates = ConvertRangesToDates(additionalField);
            
            if (!(incomingDate >= FromToDates[0] && incomingDate <= FromToDates[1]))
            {
                throw new ExceptionController(LogicExceptions.DATE_OUT_OF_RANGE);
            }
        }

        public void HasValidRangeValues(AdditionalField additionalField, AdditionalFieldValue additionalFieldValue)
        {
            if (additionalField.Ranges.Count > 0 && additionalFieldValue.Values.Count > 0)
            {
                foreach (SelectedValues selection in additionalFieldValue.Values)
                {
                    /* Validate Value Within Ranges */
                    DateTime incomingDate = ConvertStringToDate(selection.Value);
                    var FromToDates = ConvertRangesToDates(additionalField);
                    
                    if (!(incomingDate >= FromToDates[0] && incomingDate <= FromToDates[1]))
                    {
                        throw new ExceptionController(LogicExceptions.DATE_OUT_OF_RANGE);
                    }   
                }
            }
            else
            {
                throw new ExceptionController(LogicExceptions.INVALID_SELECTION);
            }
        }

        public List<DateTime> ConvertRangesToDates(AdditionalField additionalField)
        {
            var FromToDates = new List<DateTime>();

            foreach(FieldRange range in additionalField.Ranges)
            {
                string[] formats = { "MM/dd/yyyy" };
                DateTime parsedDateTime;

                bool isDate = DateTime.TryParseExact(range.Range,
                                                    "MM/dd/yyyy",
                                                    CultureInfo.InvariantCulture,
                                                    DateTimeStyles.None,
                                                    out parsedDateTime);

                
                if(!isDate)
                {
                    throw new ExceptionController(LogicExceptions.WRONG_DATE_FORMAT);
                }

                FromToDates.Add(parsedDateTime);
            }

            return FromToDates;
        }

        public DateTime ConvertStringToDate(string dateField)
        {
            DateTime incomingDate;
            try
            {
                incomingDate = Convert.ToDateTime(dateField);   
            }
            catch (System.Exception)
            {   
                throw new ExceptionController(LogicExceptions.WRONG_DATE_FORMAT);
            }

            return incomingDate;
        }
    }  
}

