using System.Collections.Generic;

namespace IMMRequest.BusinessLogic
{
    public interface IParserLogic
    {
        IEnumerable<string> GetAvailableParsers();
        void Convert(string type, string body);
    }
}
