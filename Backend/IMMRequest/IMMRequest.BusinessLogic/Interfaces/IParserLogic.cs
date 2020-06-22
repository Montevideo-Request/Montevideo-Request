using System.Collections.Generic;

namespace IMMRequest.BusinessLogic
{
    public interface IParserLogic
    {
        IEnumerable<string> GetAvailableParsers();
        IEnumerable<string> GetRequiredFields(string type);
        void Convert(Dictionary<string, string> model);
    }
}
