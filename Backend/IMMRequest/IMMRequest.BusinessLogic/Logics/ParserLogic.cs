using IMMRequest.Parser.Interface;
using System.Collections.Generic;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
using System.Reflection;
using System.Linq;
using System.IO;
using System;

namespace IMMRequest.BusinessLogic
{
    public class ParserLogic: IParserLogic
    {
        private readonly IAreaLogic<Area> AreaLogic;
        private readonly ILogic<Topic> TopicLogic;
        private readonly ILogic<TypeEntity> TypeLogic;

        public ParserLogic(IAreaLogic<Area> AreaLogic, ILogic<Topic> TopicLogic, ILogic<TypeEntity> TypeLogic) 
        { this.AreaLogic = AreaLogic; this.TopicLogic = TopicLogic; this.TypeLogic = TypeLogic; }

        public IEnumerable<string> GetAvailableParsers()
        {
            IEnumerable<IParseable> parsers = GetParsers();
            List<string> availableTypes = new List<string>();
            foreach (IParseable parser in parsers)
            {
                availableTypes.Add(parser.GetParserName());   
            }
            
            return availableTypes;
        }

        public void Convert(string type, string file)
        {
            var Parser = GetParser(type);
            IEnumerable<Area> areas = Parser.ConvertAreas(file);
            IEnumerable<Topic> topics = Parser.ConvertTopics(file);
            IEnumerable<TypeEntity> types = Parser.ConvertTypes(file);

            ImportAreas(areas);
            ImportTopics(topics);
            ImportTypes(types);
        }

        private static IEnumerable<IParseable> GetParsers()
        {
            List<IParseable> parsers = new List<IParseable>();
            string[] folder = Directory.GetFiles(@"../IMMRequest.WebApi/Parsers", "*.dll");
            foreach (string library in folder)
           {
                var libFile = new FileInfo(library);
                Assembly libAssembly = Assembly.LoadFile(libFile.FullName);
                IEnumerable<Type> implementations = GetTypesInAssembly<IParseable>(libAssembly);
                IParseable parser = (IParseable)Activator.CreateInstance(implementations.First());
                parsers.Add(parser);
           }

           return parsers;
        }

        private static IParseable GetParser(string type)
        {
            IParseable Parser = null;
            IEnumerable<IParseable> parsers = GetParsers();

            foreach( IParseable parser in parsers )
            {
                if (parser.GetParserName() == type)
                {
                    Parser = parser;
                }
            }

            if (Parser == null)
            {
                throw new ExceptionController(LogicExceptions.INVALID_PARSER_TYPE);
            }

            return Parser;
        }
        private static IEnumerable<Type> GetTypesInAssembly<Interface>(Assembly assembly)
        {
            List<Type> types = new List<Type>();
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(Interface).IsAssignableFrom(type))
                    types.Add(type);
            }
            return types;
        }

        private void ImportAreas(IEnumerable<Area> areas)
        {
            foreach(Area area in areas)
            {
                this.AreaLogic.Create(area);
            }
        }

        private void ImportTopics(IEnumerable<Topic> topics)
        {
            foreach(Topic topic in topics)
            {
                this.TopicLogic.Create(topic);
            }
        }

        private void ImportTypes(IEnumerable<TypeEntity> types)
        {
            foreach(TypeEntity type in types)
            {
                this.TypeLogic.Create(type);
            }
        }
    }
}
