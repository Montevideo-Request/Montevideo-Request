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
            var selectedValue = new SelectedValues();
            selectedValue.Value = Value;

            var values = new List<SelectedValues>(){ selectedValue };

            var fieldValue = new AdditionalFieldValue
            {
                Id = Guid.NewGuid(),
                AdditionalFieldId = field.Id,
                Values = values,
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
                Range = "01/19/1993"
            };

            var RangeList = new List<FieldRange>(){ range, range2 };

            AdditionalField field = new AdditionalField()
            {
                Id = AdditionalFieldId,
                Name = "Rango de fechas",
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
                TypeId = type.Id
            };

            Request SecondRequest = new Request()
            {
                Id = Guid.NewGuid(),
                RequestorsName = "Fernando",
                RequestorsEmail = "fernando@test.com",
                RequestorsPhone = "134234234234",
                TypeId = type.Id
            };

            Logic.Create(FirstRequest);
            Logic.Create(SecondRequest);

            List<Request> Requests = new List<Request>() { FirstRequest, SecondRequest };

            var Result = Controller.Get();
            var CreatedResult = Result as OkObjectResult;
            var RequestResults = CreatedResult.Value as IEnumerable<RequestDTO>;

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
                TypeId = type.Id
            };

            Logic.Create(Request);

            var Result = Controller.Get(Request.Id);
            var CreatedResult = Result as OkObjectResult;
            var Model = CreatedResult.Value as RequestDTO;

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
                AdditionalFieldValues = AdditionalFieldValueList
            };

            var result = Controller.Post(RequestDTO.ToModel(Request));
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as RequestDTO;

            Assert.AreEqual(Request.AdditionalFieldValues.Count, model.AdditionalFieldValues.Count);
        }

         [TestMethod]
        public void RequestsControllerPostTestNameOk()
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
                AdditionalFieldValues = AdditionalFieldValueList
            };

            var result = Controller.Post(RequestDTO.ToModel(Request));
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as RequestDTO;

            Assert.AreEqual(Request.RequestorsName, model.RequestorsName);
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
                AdditionalFieldValues = AdditionalFieldValueList
            };

            var result = Controller.Post(RequestDTO.ToModel(Request));
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as RequestDTO;

            Assert.AreEqual(Request.AdditionalFieldValues.Count, model.AdditionalFieldValues.Count);
        }


        [TestMethod]
        public void RequestsControllerUpdateTest()
        {
            var type = CreateContext();
            var additionalField = CreateFieldLogicWithRanges(type);
            var RequestId = Guid.NewGuid();
            var Logic = CreateLogic();
            var Controller = new RequestsController(Logic);

            AdditionalFieldValue fieldRangeValue = new AdditionalFieldValue()
            {
                Id = Guid.NewGuid(),
                AdditionalFieldId = additionalField.Id,
                RequestId = RequestId
            };
            
            var selectedValue = new SelectedValues();
            selectedValue.Value = "01/15/1993";

            var values = new List<SelectedValues>(){ selectedValue };
            fieldRangeValue.Values = values;

            var valuesList = new List<AdditionalFieldValue>(){ fieldRangeValue };

            Request Request = new Request()
            {
                Id = RequestId,
                RequestorsName = "Just Testing",
                RequestorsEmail = "first@test.com",
                RequestorsPhone = "489498948894",
                TypeId = type.Id,
                AdditionalFieldValues = valuesList
            };

            Logic.Create(Request);

            Request.State = "En Revision";

            var result = Controller.Put( RequestId, RequestDTO.ToModel(Request));
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as RequestDTO;

            Assert.AreEqual("En Revision", model.State);
        }
    }
}
