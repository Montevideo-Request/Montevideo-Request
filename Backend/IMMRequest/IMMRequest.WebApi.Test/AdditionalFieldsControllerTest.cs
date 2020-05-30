using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.WebApi.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using IMMRequest.BusinessLogic;
using IMMRequest.DataAccess;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
using IMMRequest.DTO;
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
            var FieldResults = CreatedResult.Value as IEnumerable<AdditionalFieldDTO>;

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
                FieldType = "Texto"
            };

            Logic.Create(Field);

            var Result = Controller.Get(Field.Id);
            var CreatedResult = Result as OkObjectResult;
            var Model = CreatedResult.Value as AdditionalFieldDTO;

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
                FieldType = "Fecha"
            };

            var SecondField = new AdditionalField
            {
                Id = Guid.NewGuid(),
                Name = "Second Field",
                Type = type,
                TypeId = type.Id,
                FieldType = "Entero"
            };

            FieldRange dummyRangeStartDate = new FieldRange()
            {
                AdditionalFieldId = FirstField.Id,
                Range = "01/14/1993"
            };

            FieldRange dummyRangeEndDate = new FieldRange()
            {
                AdditionalFieldId = FirstField.Id,
                Range = "01/30/1993"
            };

            FieldRange dummyRangeStart = new FieldRange()
            {
                AdditionalFieldId = SecondField.Id,
                Range = "1"
            };

            FieldRange dummyRangeEnd = new FieldRange()
            {
                AdditionalFieldId = SecondField.Id,
                Range = "10"
            };

            var DummyIntRangeList = new List<FieldRange>(){dummyRangeStart, dummyRangeEnd };
            var DummyDateRangeList = new List<FieldRange>(){dummyRangeStartDate, dummyRangeEndDate };
            FirstField.Ranges = DummyDateRangeList;
            SecondField.Ranges = DummyIntRangeList;
            
            Logic.Create(FirstField);
            Logic.Create(SecondField);

            var Result = Controller.Get(SecondField.Id);
            var CreatedResult = Result as OkObjectResult;
            var Model = CreatedResult.Value as AdditionalFieldDTO;

            Assert.AreEqual(SecondField.Name, Model.Name);
        }


         [TestMethod]
        public void AdditionalFieldControllerPostTest()
        {
            var type = CreateContext();
            var Logic = CreateLogic();
            var Controller = new AdditionalFieldsController(Logic);
            var AdditionalField = new AdditionalField
            {
                Id = Guid.NewGuid(),
                Name = "First AdditionalField",
                Type = type,
                TypeId = type.Id,
                FieldType = "Entero"
            };

            FieldRange dummyRangeStart = new FieldRange()
            {
                AdditionalFieldId = AdditionalField.Id,
                Range = "1"
            };

            FieldRange dummyRangeEnd = new FieldRange()
            {
                AdditionalFieldId = AdditionalField.Id,
                Range = "10"
            };

            var DummyIntRangeList = new List<FieldRange>(){dummyRangeStart, dummyRangeEnd };

            AdditionalField.Ranges = DummyIntRangeList;
            

            var result = Controller.Post(AdditionalFieldDTO.ToModel(AdditionalField));
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as AdditionalFieldDTO;

            Assert.AreEqual(AdditionalField.Name, model.Name);
        }


        [TestMethod]
        public void AdditionalFieldControllerPostTestWithRanges()
        {
            var id = Guid.NewGuid();
            var type = CreateContext();
            var Logic = CreateLogic();
            var Controller = new AdditionalFieldsController(Logic);
            var AdditionalField = new AdditionalField
            {
                Id = id,
                Name = "First AdditionalField",
                Type = type,
                TypeId = type.Id,
                FieldType = "Texto"
            };

            FieldRange range = new FieldRange()
            {
                AdditionalFieldId = id,
                Range = "Range 1"
            };

            FieldRange range2 = new FieldRange()
            {
                AdditionalFieldId = id,
                Range = "Range 2"
            };

            FieldRange range3 = new FieldRange()
            {
                AdditionalFieldId = id,
                Range = "Range 3"
            };

            var RangeList = new List<FieldRange>(){ range, range2, range3 };

            AdditionalField.Ranges = RangeList;

            var result = Controller.Post(AdditionalFieldDTO.ToModel(AdditionalField));
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as AdditionalFieldDTO;

            Assert.AreEqual(AdditionalField.Ranges.Count, model.Ranges.Count);
        }


         [TestMethod]
        public void AdditionalFieldsControllerUpdateTest()
        {
            var type = CreateContext();
            var fieldId = Guid.NewGuid();
            var Logic = CreateLogic();
            var Controller = new AdditionalFieldsController(Logic);

            AdditionalField field = new AdditionalField()
            {
                Id = fieldId,
                Name = "First AdditionalField",
                Type = type,
                TypeId = type.Id,
                FieldType = "Texto"
            };

            Logic.Create(field);

            AdditionalFieldDTO UpdatedField = new AdditionalFieldDTO()
            {
                Id = fieldId,
                Name = "Updated Field"
            };

            var result = Controller.Put( fieldId, UpdatedField);
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as AdditionalFieldDTO;

            Assert.AreEqual("Updated Field", model.Name);
        }

        [TestMethod]
        public void TypesControllerDeleteTest()
        {
            var type = CreateContext();
            var fieldId = Guid.NewGuid();
            var Logic = CreateLogic();
            var Controller = new AdditionalFieldsController(Logic);

            AdditionalField field = new AdditionalField()
            {
                Id = fieldId,
                Name = "First AdditionalField",
                Type = type,
                TypeId = type.Id,
                FieldType = "Texto"
            };

            Logic.Create(field);
            Controller.Delete(fieldId);

            Assert.ThrowsException<ExceptionController>(() => Logic.Get(fieldId));
        }
    }
}
