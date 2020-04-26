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
    public class TopicsControllerTest
    {

        public TopicLogic CreateLogic()
        {
            IMMRequestContext Context = ContextFactory.GetNewContext();
            var Repository = new TopicRepository(Context);
            var Logic = new TopicLogic(Repository);

            return Logic;
        }

        [TestMethod]
        public void TopicsControllerGetAllTest()
        {

            var FirstTopic = new Topic
            {
                Id = Guid.NewGuid(),
                Name = "First Topic",
            };
            
            var SecondTopic = new Topic
            {
                Id = Guid.NewGuid(),
                Name = "Second Topic",
            };

            var Logic = CreateLogic();
            var Controller = new TopicsController(Logic);

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

            var Topic = new Topic
            {
                Id = Guid.NewGuid(),
                Name = "First Topic",
            };

            var Logic = CreateLogic();
            var Controller = new TopicsController(Logic);

            Logic.Create(Topic);

            var Result = Controller.Get(Topic.Id);
            var CreatedResult = Result as OkObjectResult;
            var Model = CreatedResult.Value as TopicModel;
            
            Assert.AreEqual(Topic.Name, Model.Name);
        }


        [TestMethod]
        public void TopicControllerPostTest()
        {
            
            var Topic = new Topic
            {
                Id = Guid.NewGuid(),
                Name = "First Topic"
            };

            var Logic = CreateLogic();
            var Controller = new TopicsController(Logic);

            var result = Controller.Post(TopicModel.ToModel(Topic));
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as TopicModel;

            Assert.AreEqual(Topic.Name, model.Name);
        }
    }
}
