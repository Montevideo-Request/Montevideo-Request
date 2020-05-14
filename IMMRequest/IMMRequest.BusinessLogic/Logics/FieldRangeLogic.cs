using IMMRequest.Domain;

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
            else if (fieldType == TEXT)
            {
                SetValidationStrategy(new RangeTypeText());
            }
        }
        
        public void SetValidationStrategy(IRangeTypeStrategy validationStrategy)
        {
            this._validationStrategy = validationStrategy;
        }

        public void ValidRangeFormat(AdditionalField additionalField)
        {  
            this._validationStrategy.ValidRangeFormat(additionalField);  
        }

        public void IsValidRangeValue(AdditionalField additionalField, AdditionalFieldValue additionalFieldValue)
        {  
            this._validationStrategy.IsValidRangeValue(additionalField, additionalFieldValue);  
        }

    }
}
