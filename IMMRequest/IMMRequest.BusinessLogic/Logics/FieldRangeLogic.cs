using IMMRequest.Domain;
using System.Collections.Generic;
using IMMRequest.BusinessLogic.Interface;

namespace IMMRequest.BusinessLogic
{
    public class FieldRangeLogic
    {   
        public const string TEXT = "Texto";
        public const string DATE = "Fecha";
        public const string INTEGER = "Entero";

        private IRangeTypeStrategy _validationStrategy;

        public FieldRangeLogic(string fieldType)
        {
            SetStrategy(fieldType);
        }
        
        public void SetValidationStrategy(IRangeTypeStrategy validationStrategy)
        {
            this._validationStrategy = validationStrategy;
        }

        public void ValidateRanges(string FieldType, ICollection<FieldRange> ranges)
        {  
            this._validationStrategy.ValidateRanges(FieldType, ranges);  
        }  

        public void SetStrategy(string fieldType)
        {
            if (fieldType == DATE)
            {
                SetValidationStrategy(new RangeTypeDate());
            }
            else if (fieldType == INTEGER)
            {
                SetValidationStrategy(new RangeTypeInteger());
            } 
            else
            {
                SetValidationStrategy(new RangeTypeText());
            }
        }
    }
}
