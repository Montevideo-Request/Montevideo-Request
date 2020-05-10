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
    public class RequestsControllerTest
    {

        private IMMRequestContext Context = ContextFactory.GetNewContext();

        public RequestLogic CreateLogic()
        {
            var Repository = new RequestRepository(Context);
            var Logic = new RequestLogic(Repository);

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

        private ICollection<AdditionalFieldValue> CreateFields(AdditionalField field, Guid RequestId, string Value)
        {
            var fieldValue = new AdditionalFieldValue
            {
                Id = Guid.NewGuid(),
                AdditionalFieldId = field.Id,
                Value = Value,
                RequestId = RequestId
            };

            List<AdditionalFieldValue> fields = new List<AdditionalFieldValue>(){ fieldValue };

            return fields;
        }

        private AdditionalField CreateFieldLogic(TypeEntity type)
        {
            var additionalFieldRepo = new AdditionalFieldRepository(Context);
            var additionalFieldLogic = new AdditionalFieldLogic(additionalFieldRepo);   

            AdditionalField field = new AdditionalField()
            {
                Id = Guid.NewGuid(),
                Name = "Celular de Contacto",
                Type = type,
                FieldType = "Texto",
                TypeId = type.Id
            };

            additionalFieldLogic.Create(field);

            return field;
        }

        private AdditionalField CreateFieldLogicWithRanges(TypeEntity type)
        {
            var additionalFieldRepo = new AdditionalFieldRepository(Context);
            var additionalFieldLogic = new AdditionalFieldLogic(additionalFieldRepo);  
            var AdditionalFieldId = Guid.NewGuid(); 

            FieldRange range = new FieldRange()
            {
                AdditionalFieldId = AdditionalFieldId,
                Range = "01/14/1993"
            };

            FieldRange range2 = new FieldRange()
            {
                AdditionalFieldId = AdditionalFieldId,
                Range = "01/15/1993"
            };

            FieldRange range3 = new FieldRange()
            {
                AdditionalFieldId = AdditionalFieldId,
                Range = "01/16/1993"
            };

            var RangeList = new List<FieldRange>(){ range, range2, range3 };

            AdditionalField field = new AdditionalField()
            {
                Id = AdditionalFieldId,
                Name = "Celular de Contacto",
                Type = type,
                FieldType = "Fecha",
                TypeId = type.Id,
                Ranges = RangeList
            };

            additionalFieldLogic.Create(field);

            return field;
        }

        private TypeEntity CreateContext()
        {
            var area = CreateAreaContext();
            var topic = CreateTopicContext(area);
            var type = CreateTypeContext(topic);

            return type;
        }

        [TestMethod]
        public void RequestsControllerGetAllTest()
        {
            var type = CreateContext();
            var Logic = CreateLogic();
            var Controller = new RequestsController(Logic);

            Request FirstRequest = new Request()
            {
                Id = Guid.NewGuid(),
                RequestorsName = "Just Testing",
                RequestorsEmail = "first@test.com",
                RequestorsPhone = "489498948894",
                TypeId = type.Id,
                State = "State",
                Description = "description"
            };

            Request SecondRequest = new Request()
            {
                Id = Guid.NewGuid(),
                RequestorsName = "Fernando",
                RequestorsEmail = "fernando@test.com",
                RequestorsPhone = "134234234234",
                TypeId = type.Id,
                State = "State",
                Description = "description"
            };

            Logic.Create(FirstRequest);
            Logic.Create(SecondRequest);

            List<Request> Requests = new List<Request>() { FirstRequest, SecondRequest };

            var Result = Controller.Get();
            var CreatedResult = Result as OkObjectResult;
            var RequestResults = CreatedResult.Value as IEnumerable<RequestModel>;

            Assert.AreEqual(Requests.Count, RequestResults.ToList().Count);
        }

        [TestMethod]
        public void RequestsControllerGetTest()
        {
            var type = CreateContext();
            var Logic = CreateLogic();
            var Controller = new RequestsController(Logic);

            Request Request = new Request()
            {
                Id = Guid.NewGuid(),
                RequestorsName = "Just Testing",
                RequestorsEmail = "first@test.com",
                RequestorsPhone = "489498948894",
                TypeId = type.Id,
                State = "State",
                Description = "description"
            };

            Logic.Create(Request);

            var Result = Controller.Get(Request.Id);
            var CreatedResult = Result as OkObjectResult;
            var Model = CreatedResult.Value as RequestModel;

            Assert.AreEqual(Request.Id, Model.Id);
        }

        [TestMethod]
        public void RequestsControllerPostTest()
        {
            var type = CreateContext();
            var additionalField = CreateFieldLogic(type);
            var RequestId = Guid.NewGuid();
            var AdditionalFieldValueList = CreateFields(additionalField, RequestId, "095937800");
            var Logic = CreateLogic();
            var Controller = new RequestsController(Logic);

            Request Request = new Request()
            {
                Id = RequestId,
                RequestorsName = "Just Testing",
                RequestorsEmail = "first@test.com",
                RequestorsPhone = "489498948894",
                TypeId = type.Id,
                State = "State",
                Description = "description",
                AdditionalFieldValues = AdditionalFieldValueList
            };

            var result = Controller.Post(RequestModel.ToModel(Request));
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as RequestModel;

            Assert.AreEqual(Request.Id, model.Id);
        }

        [TestMethod]
        public void RequestsControllerPostTestWithRanges()
        {
            var type = CreateContext();
            var additionalField = CreateFieldLogicWithRanges(type);
            var RequestId = Guid.NewGuid();
            var AdditionalFieldValueList = CreateFields(additionalField, RequestId, "01/15/1993");
            var Logic = CreateLogic();
            var Controller = new RequestsController(Logic);

            Request Request = new Request()
            {
                Id = RequestId,
                RequestorsName = "Just Testing",
                RequestorsEmail = "first@test.com",
                RequestorsPhone = "489498948894",
                TypeId = type.Id,
                State = "State",
                Description = "description",
                AdditionalFieldValues = AdditionalFieldValueList
            };

            var result = Controller.Post(RequestModel.ToModel(Request));
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as RequestModel;

            Assert.AreEqual(Request.Id, model.Id);
        }


        [TestMethod]
        public void RequestsControllerUpdateTest()
        {
            var type = CreateContext();
            var additionalField = CreateFieldLogicWithRanges(type);
            var RequestId = Guid.NewGuid();
            var Logic = CreateLogic();
            var Controller = new RequestsController(Logic);

            Request Request = new Request()
            {
                Id = RequestId,
                RequestorsName = "Just Testing",
                RequestorsEmail = "first@test.com",
                RequestorsPhone = "489498948894",
                TypeId = type.Id,
                Description = "description"
            };

            Logic.Create(Request);

            RequestModel UpdatedRequest = new RequestModel()
            {
                Id = RequestId,
                State = "En Revision",
                TypeId = type.Id
            };

            var result = Controller.Put( RequestId, UpdatedRequest);
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as RequestModel;

            Assert.AreEqual("En Revision", model.State);
        }
    }
}
