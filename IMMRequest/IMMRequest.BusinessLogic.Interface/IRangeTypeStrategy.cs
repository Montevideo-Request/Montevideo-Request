using System.Collections.Generic;
using IMMRequest.Domain;

namespace IMMRequest.BusinessLogic.Interface
{
    public interface IRangeTypeStrategy
    {
        void ValidRangeFormat(AdditionalField additionalField);
    }
}
