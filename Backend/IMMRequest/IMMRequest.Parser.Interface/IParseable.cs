using System.Collections.Generic;
using IMMRequest.Domain;
using System;

namespace IMMRequest.Parser.Interface
{
    public interface IParseable
    {
        string GetParserName();
        IEnumerable<Area> ConvertAreas(Dictionary<string, string> model);
        IEnumerable<Topic> ConvertTopics(Dictionary<string, string> model);
        IEnumerable<TypeEntity> ConvertTypes(Dictionary<string, string> model);
    } 
}
