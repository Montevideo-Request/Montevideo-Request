using IMMRequest.Parser.Interface;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Xml;
using System;

namespace IMMRequest.XMLParser
{
    public class XMLParser : IParseable
    {
        public string Type { get; set; }
        public string FilePath { get; set;}
        public XMLParser() { this.Type = "XML"; }
        public string GetParserName() { return this.Type; }
        public IEnumerable<Area> ConvertAreas(Dictionary<string, string> parserModel)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(parserModel["FilePath"]);
            
            XmlElement root = xml.DocumentElement;
            List<Area> areas = new List<Area>();

            /* Areas Import */
            XmlNodeList nodes = root.SelectNodes("Area");
            foreach (XmlNode node in nodes)
            {
                Area importedArea = new Area();
                importedArea.Name = node["Name"].InnerText;
                importedArea.Id = Guid.Parse(node["InternalId"].InnerText);
                areas.Add(importedArea);
            }
            return areas;
        }

        public IEnumerable<Topic> ConvertTopics(Dictionary<string, string> parserModel)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(parserModel["FilePath"]);
            
            XmlElement root = xml.DocumentElement;
            List<Topic> topics = new List<Topic>();

            /* Topics Import */
            XmlNodeList nodes = root.SelectNodes("Topic");
            foreach (XmlNode node in nodes)
            {
                Topic importedTopic = new Topic();
                importedTopic.Name = node["Name"].InnerText;
                importedTopic.Id = Guid.Parse(node["InternalId"].InnerText);
                importedTopic.AreaId = Guid.Parse(node["AreaId"].InnerText);
                topics.Add(importedTopic);
            }
            return topics;
        }

        public IEnumerable<TypeEntity> ConvertTypes(Dictionary<string, string> parserModel)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(parserModel["FilePath"]);
            
            XmlElement root = xml.DocumentElement;
            List<TypeEntity> types = new List<TypeEntity>();

            /* Types Import */
            XmlNodeList nodes = root.SelectNodes("Type");
            foreach (XmlNode node in nodes)
            {
                TypeEntity importedType = new TypeEntity();
                importedType.Name = node["Name"].InnerText;
                importedType.Id = Guid.Parse(node["InternalId"].InnerText);
                importedType.TopicId = Guid.Parse(node["TopicId"].InnerText);;

                types.Add(importedType);
            }
            return types;
        }
    }
}
