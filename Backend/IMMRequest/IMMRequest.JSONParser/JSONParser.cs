using IMMRequest.Parser.Interface;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using IMMRequest.Domain;
using Newtonsoft.Json;
using System.IO;
using System;

namespace IMMRequest.JSONParser
{
    public class JSONParser : IParseable 
    {
        public string Type { get; set; }
        public string FilePath { get; set; }
        public JSONParser() { this.Type = "JSON"; }
        public string GetParserName() { return this.Type; }

        public IEnumerable<Area> ConvertAreas(Dictionary<string, string> parserModel)
        {
            List<Area> areas = new List<Area>();

            using (StreamReader fileReader = File.OpenText(parserModel["FilePath"]))
            using (JsonTextReader jsonReader = new JsonTextReader(fileReader))
            {
                JObject json = (JObject) JToken.ReadFrom(jsonReader);
                JArray areasArray = (JArray) json["Area"];

                foreach(JObject node in areasArray)
                {
                    Area area = new Area();
                    area.Id = Guid.Parse(node.GetValue("InternalId").ToString());
                    area.Name = node.GetValue("Name").ToString();
                    areas.Add(area);
                }
            }

            return areas;
        }
        public IEnumerable<Topic> ConvertTopics(Dictionary<string, string> parserModel)
        {
            List<Topic> topics = new List<Topic>();

            using (StreamReader fileReader = File.OpenText(parserModel["FilePath"]))
            using (JsonTextReader jsonReader = new JsonTextReader(fileReader))
            {
                JObject json = (JObject) JToken.ReadFrom(jsonReader);
                JArray topicsArray = (JArray) json["Topic"];

                foreach(JObject node in topicsArray)
                {
                    Topic topic = new Topic();
                    topic.Id = Guid.Parse(node.GetValue("InternalId").ToString());
                    topic.AreaId = Guid.Parse(node.GetValue("AreaId").ToString());
                    topic.Name = node.GetValue("Name").ToString();
                    topics.Add(topic);
                }
            }

            return topics;
        }

        public IEnumerable<TypeEntity> ConvertTypes(Dictionary<string, string> parserModel)
        {
            List<TypeEntity> types = new List<TypeEntity>();
            using (StreamReader fileReader = File.OpenText(parserModel["FilePath"]))
            using (JsonTextReader jsonReader = new JsonTextReader(fileReader))
            {
                JObject json = (JObject) JToken.ReadFrom(jsonReader);
                JArray topicsArray = (JArray) json["Type"];

                foreach(JObject node in topicsArray)
                {
                    TypeEntity type = new TypeEntity();
                    type.Id = Guid.Parse(node.GetValue("InternalId").ToString());
                    type.TopicId = Guid.Parse(node.GetValue("TopicId").ToString());
                    type.Name = node.GetValue("Name").ToString();
                    types.Add(type);
                }
            }

            return types;
        }
    }
}
