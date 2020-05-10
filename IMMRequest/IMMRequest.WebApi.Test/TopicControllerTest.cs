using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.WebApi.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using IMMRequest.BusinessLogic;
using IMMRequest.WebApi.Models;
using IMMRequest.DataAccess;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.WebApi.Test
{
    [TestClass]
    public class TopicsControllerTest
    {
        private IMMRequestContext Context = ContextFactory.GetNewContext();
        public TopicLogic CreateLogic()
        {
            var Repository = new TopicRepository(Context);
            var Logic = new TopicLogic(Repository);

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

        [TestMethod]
        public void TopicsControllerGetAllTest()
        {
            var area = CreateAreaContext();
            var Logic = CreateLogic();
            var Controller = new TopicsController(Logic);
            var FirstTopic = new Topic
            {
                Id = Guid.NewGuid(),
                Name = "First Topic",
                Area = area,
                AreaId = area.Id
            };
            
            var SecondTopic = new Topic
            {
                Id = Guid.NewGuid(),
                Name = "Second Topic",
                Area = area,
                AreaId = area.Id
            };

            Logic.Create(FirstTopic);
            Logic.Create(SecondTopic);

            List<Topic> Topics = new List<Topic>() { FirstTopic, SecondTopic };

            var Result = Controller.Get();
            var CreatedResult = Result as OkObjectResult;
            var TopicResults = CreatedResult.Value as IEnumerable<TopicModel>;

            Assert.AreEqual(Topics.Count, TopicResults.ToList().Count);
        }

        [TestMethod]
        public void TopicsControllerGetTest()
        {
            var area = CreateAreaContext();
            var Logic = CreateLogic();
            var Controller = new TopicsController(Logic);
            var Topic = new Topic
            {
                Id = Guid.NewGuid(),
                Name = "First Topic",
                Area = area,
                AreaId = area.Id
            };

            Logic.Create(Topic);

            var Result = Controller.Get(Topic.Id);
            var CreatedResult = Result as OkObjectResult;
            var Model = CreatedResult.Value as TopicModel;
            
            Assert.AreEqual(Topic.Name, Model.Name);
        }


        [TestMethod]
        public void TopicControllerPostTest()
        {
            var area = CreateAreaContext();
            var Logic = CreateLogic();
            var Controller = new TopicsController(Logic);
            var Topic = new Topic
            {
                Id = Guid.NewGuid(),
                Name = "First Topic",
                Area = area,
                AreaId = area.Id
            };

            var result = Controller.Post(TopicModel.ToModel(Topic));
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as TopicModel;

            Assert.AreEqual(Topic.Name, model.Name);
        }

         [TestMethod]
        public void TopicControllerUpdateTest()
        {
            var area = CreateAreaContext();
            var topicId = Guid.NewGuid();
            var Logic = CreateLogic();
            var Controller = new TopicsController(Logic);

            Topic Topic = new Topic()
            {
                Id = topicId,
                AreaId = area.Id,
                Area = area,
                Name = "First Topic"
            };

            Logic.Create(Topic);

            TopicModel UpdatedTopic = new TopicModel()
            {
                Id = topicId,
                Name = "Updated Topic"
            };

            var result = Controller.Put( topicId, UpdatedTopic);
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as TopicModel;

            Assert.AreEqual("Updated Topic", model.Name);
        }


        [TestMethod]
        public void TopicsControllerDeleteTest()
        {
            var area = CreateAreaContext();
            var topicId = Guid.NewGuid();
            var Logic = CreateLogic();
            var Controller = new TopicsController(Logic);

            Topic Topic = new Topic()
            {
                Id = Guid.NewGuid(),
                AreaId = area.Id,
                Area = area,
                Name = "First Topic"
            };

            Logic.Create(Topic);
            Controller.Delete(Topic.Id);

            Assert.ThrowsException<ExceptionController>(() => Logic.Get(Topic.Id));
        }
    }
}
