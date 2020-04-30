using System.Collections.Generic;
using IMMRequest.Domain;

namespace IMMRequest.BusinessLogic.Interface
{
    public interface IRangeTypeStrategy
    {
        void ValidateRanges(string FieldType, ICollection<FieldRange> ranges);
    }
}
