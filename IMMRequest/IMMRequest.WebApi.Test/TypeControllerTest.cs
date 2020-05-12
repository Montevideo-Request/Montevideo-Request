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
    public class TypesControllerTest
    {
        private IMMRequestContext Context = ContextFactory.GetNewContext();

        public TypeLogic CreateLogic()
        {
            var Repository = new TypeRepository(Context);
            var Logic = new TypeLogic(Repository);

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

        private Topic CreateContext()
        {
            var area = CreateAreaContext();
            var topic = CreateTopicContext(area);

            return topic;
        }

        [TestMethod]
        public void TypesControllerGetAllTest()
        {
            var topic = CreateContext();
            var Logic = CreateLogic();
            var Controller = new TypesController(Logic);
            var FirstType = new TypeEntity()
            {
                Id = Guid.NewGuid(),
                Name = "First Type",
                Topic = topic,
                TopicId = topic.Id
            };
            
            var SecondType = new TypeEntity
            {
                Id = Guid.NewGuid(),
                Name = "Second Type",
                Topic = topic,
                TopicId = topic.Id
            };

            Logic.Create(FirstType);
            Logic.Create(SecondType);

            List<TypeEntity> Types = new List<TypeEntity>() { FirstType, SecondType };

            var Result = Controller.Get();
            var CreatedResult = Result as OkObjectResult;
            var TypeResults = CreatedResult.Value as IEnumerable<TypeModel>;

            Assert.AreEqual(Types.Count, TypeResults.ToList().Count);
        }

        [TestMethod]
        public void TypesControllerGetTest()
        {

            var topic = CreateContext();
            var Logic = CreateLogic();
            var Controller = new TypesController(Logic);
            var Type = new TypeEntity
            {
                Id = Guid.NewGuid(),
                Name = "First Type",
                Topic = topic,
                TopicId = topic.Id
            };

            Logic.Create(Type);

            var Result = Controller.Get(Type.Id);
            var CreatedResult = Result as OkObjectResult;
            var Model = CreatedResult.Value as TypeModel;
            
            Assert.AreEqual(Type.Name, Model.Name);
        }

        [TestMethod]
        public void TypeControllerPostTest()
        {
            var topic = CreateContext();
            var Logic = CreateLogic();
            var Controller = new TypesController(Logic);
            var Type = new TypeEntity
            {
                Id = Guid.NewGuid(),
                Name = "First Type",
                Topic = topic,
                TopicId = topic.Id
            };

            var result = Controller.Post(TypeModel.ToModel(Type));
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as TypeModel;

            Assert.AreEqual(Type.Name, model.Name);
        }

         [TestMethod]
        public void TypesControllerUpdateTest()
        {
            var topic = CreateContext();
            var typeId = Guid.NewGuid();
            var Logic = CreateLogic();
            var Controller = new TypesController(Logic);

            TypeEntity type = new TypeEntity()
            {
                Id = typeId,
                Name = "First Type",
                Topic = topic,
                TopicId = topic.Id
            };

            Logic.Create(type);

            TypeModel UpdatedType = new TypeModel()
            {
                Id = typeId,
                Name = "Updated Type"
            };

            var result = Controller.Put( typeId, UpdatedType);
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as TypeModel;

            Assert.AreEqual("Updated Type", model.Name);
        }

        [TestMethod]
        public void TypesControllerDeleteTest()
        {
            var topic = CreateContext();
            var typeId = Guid.NewGuid();
            var Logic = CreateLogic();
            var Controller = new TypesController(Logic);

            TypeEntity type = new TypeEntity()
            {
                Id = typeId,
                Name = "First Type",
                Topic = topic,
                TopicId = topic.Id
            };

            Logic.Create(type);
            Controller.Delete(type.Id);

            Assert.ThrowsException<ExceptionController>(() => Logic.Get(type.Id));
        }
    }
}
