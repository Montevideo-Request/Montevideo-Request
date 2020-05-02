using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.WebApi.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using IMMRequest.BusinessLogic;
using IMMRequest.WebApi.Models;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.WebApi.Test
{
    [TestClass]
    public class AdditionalFieldsControllerTest
    {
        private IMMRequestContext Context = ContextFactory.GetNewContext();

        private AdditionalFieldLogic CreateLogic()
        {
            var Repository = new AdditionalFieldRepository(Context);
            var Logic = new AdditionalFieldLogic(Repository);

            return Logic;
        }

        private Area CreateAreaContext()
        {
            var areaRepo = new AreaRepository(Context);
            var areaLogic = new AreaLogic(areaRepo);

            Area area = new Area()
            {
                Name = "Test area",
            };

            areaLogic.Create(area);

            return area;
        }

        private Topic CreateTopicContext(Area area)
        {
            var topicRepo = new TopicRepository(Context);
            var topicLogic = new TopicLogic(topicRepo);   

            Topic topic = new Topic()
            {
                Name = "Test topic",
                Area = area,
                AreaId = area.Id
            };

            topicLogic.Create(topic);

            return topic;
        }

        private TypeEntity CreateTypeContext(Topic topic)
        {
            var typeRepo = new TypeRepository(Context);
            var typeLogic = new TypeLogic(typeRepo);

            TypeEntity type = new TypeEntity()
            {
                Name = "Test Type",
                Topic = topic,
                TopicId = topic.Id
            };

            typeLogic.Create(type);

            return type;
        }

        private TypeEntity CreateContext()
        {
            var area = CreateAreaContext();
            var topic = CreateTopicContext(area);
            var type = CreateTypeContext(topic);

            return type;
        }

        [TestMethod]
        public void AdditionalFieldsControllerGetAllTest()
        {
            var type = CreateContext();
            var Logic = CreateLogic();
            var Controller = new AdditionalFieldsController(Logic);
            var FirstAdditionalField = new AdditionalField
            {
                Id = Guid.NewGuid(),
                Name = "First Field",
                Type = type,
                FieldType = "Texto",
                TypeId = type.Id
            };

            var SecondAdditionalField = new AdditionalField
            {
                Id = Guid.NewGuid(),
                Name = "Second Field",
                Type = type,
                FieldType = "Texto",
                TypeId = type.Id
            };

            Logic.Create(FirstAdditionalField);
            Logic.Create(SecondAdditionalField);

            List<AdditionalField> AdditionalFields = new List<AdditionalField>() { FirstAdditionalField, SecondAdditionalField };

            var Result = Controller.Get();
            var CreatedResult = Result as OkObjectResult;
            var FieldResults = CreatedResult.Value as IEnumerable<AdditionalFieldModel>;

            Assert.AreEqual(AdditionalFields.Count, FieldResults.ToList().Count);
        }

        [TestMethod]
        public void AdditionalFieldsControllerGetTest()
        {
            var type = CreateContext();
            var Logic = CreateLogic();
            var Controller = new AdditionalFieldsController(Logic);
            var Field = new AdditionalField
            {
                Id = Guid.NewGuid(),
                Name = "First Field",
                Type = type,
                TypeId = type.Id,
                FieldType = "Fecha"
            };

            Logic.Create(Field);

            var Result = Controller.Get(Field.Id);
            var CreatedResult = Result as OkObjectResult;
            var Model = CreatedResult.Value as AdditionalFieldModel;

            Assert.AreEqual(Field.Name, Model.Name);
        }

         [TestMethod]
        public void AdditionalFieldsControllerGetTest2()
        {
            var type = CreateContext();
            var Logic = CreateLogic();
            var Controller = new AdditionalFieldsController(Logic);
            var FirstField = new AdditionalField
            {
                Id = Guid.NewGuid(),
                Name = "First Field",
                Type = type,
                TypeId = type.Id,
                FieldType = "Fecha",
            };

            var SecondField = new AdditionalField
            {
                Id = Guid.NewGuid(),
                Name = "Second Field",
                Type = type,
                TypeId = type.Id,
                FieldType = "Entero",
            };
            
            Logic.Create(FirstField);
            Logic.Create(SecondField);

            var Result = Controller.Get(SecondField.Id);
            var CreatedResult = Result as OkObjectResult;
            var Model = CreatedResult.Value as AdditionalFieldModel;

            Assert.AreEqual(SecondField.Name, Model.Name);
        }
    }
}
