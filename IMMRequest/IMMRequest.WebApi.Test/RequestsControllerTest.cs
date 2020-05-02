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
        public RequestLogic CreateLogic()
        {
            IMMRequestContext Context = ContextFactory.GetNewContext();
            var Repository = new RequestRepository(Context);
            var Logic = new RequestLogic(Repository);

            return Logic;
        }

        [TestMethod]
        public void RequestsControllerGetAllTest()
        {

            Request FirstRequest = new Request()
            {
                Id = Guid.NewGuid(),
                RequestorsName = "Just Testing",
                RequestorsEmail = "first@test.com",
                RequestorsPhone = "489498948894",
                TypeId = Guid.NewGuid(),
                State = "State",
                Description = "description"
            };

            Request SecondRequest = new Request()
            {
                Id = Guid.NewGuid(),
                RequestorsName = "Fernando",
                RequestorsEmail = "fernando@test.com",
                RequestorsPhone = "134234234234",
                TypeId = Guid.NewGuid(),
                State = "State",
                Description = "description"
            };

            var Logic = CreateLogic();
            var Controller = new RequestsController(Logic);

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
            Request Request = new Request()
            {
                Id = Guid.NewGuid(),
                RequestorsName = "Just Testing",
                RequestorsEmail = "first@test.com",
                RequestorsPhone = "489498948894",
                TypeId = Guid.NewGuid(),
                State = "State",
                Description = "description"
            };

            var Logic = CreateLogic();
            var Controller = new RequestsController(Logic);

            Logic.Create(Request);

            var Result = Controller.Get(Request.Id);
            var CreatedResult = Result as OkObjectResult;
            var Model = CreatedResult.Value as RequestModel;

            Assert.AreEqual(Request.Id, Model.Id);
        }

        [TestMethod]
        public void RequestsControllerPostTest()
        {
            AdditionalFieldValue additionalFieldValue = new AdditionalFieldValue();
            Request Request = new Request()
            {
                Id = Guid.NewGuid(),
                RequestorsName = "Just Testing",
                RequestorsEmail = "first@test.com",
                RequestorsPhone = "489498948894",
                TypeId = Guid.NewGuid(),
                State = "State",
                Description = "description",
            };

            var Logic = CreateLogic();
            var Controller = new RequestsController(Logic);

            var result = Controller.Post(RequestModel.ToModel(Request));
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as RequestModel;

            Assert.AreEqual(Request.Id, model.Id);
        }
    }
}
