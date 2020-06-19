using System.Collections.Generic;
using IMMRequest.Domain;
using System;

namespace IMMRequest.Parser.Interface
{
    public interface IParseable
    {
        string GetParserName();
        IEnumerable<Area> ConvertAreas(string file);
        IEnumerable<Topic> ConvertTopics(string file);
        IEnumerable<TypeEntity> ConvertTypes(string file);
    } 
}
