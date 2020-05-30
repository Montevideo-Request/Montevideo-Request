using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Exceptions;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System;
using Moq;

namespace IMMRequest.BusinessLogic.Test
{
    [TestClass]
    public class TopicTest : BaseLogicTest<Topic, Area>
    {
        public TopicTest() {}

        public override BaseLogic<Topic, Area> CreateBaseLogic(IRepository<Topic, Area> obj)
        {
            var controller = new TopicLogic(obj);
            return controller;
        }

        public override Topic CreateEntity()
        {
            Topic topic = new Topic() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                AreaId = Guid.NewGuid()
	        };
            return topic;
        }

        public override Guid GetId(Topic entity)
        {
            return entity.Id;
        }

        public override Topic ModifyEntity(Topic entity)
        {
            entity.Name = "New Name";
            return entity;
        }

        [TestMethod]
        public void CreateCaseNotExist() 
        {
            Guid guid = Guid.NewGuid();
            Guid secondGuid = Guid.NewGuid();
	        Topic topic = new Topic() 
            {
                Id = guid,
                Name = "Just Testing",
                AreaId = secondGuid
	        };
            Area area = new Area();
            area.Id = secondGuid;

            var mock = new Mock<IRepository<Topic, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.GetParent(secondGuid)).Returns(area);
            mock.Setup(m => m.Add(It.IsAny<Topic>()));
            mock.Setup(m => m.Save());

            var controller = new TopicLogic(mock.Object);
            Topic result = controller.Create(topic);

            mock.VerifyAll();
            Assert.AreEqual(result, topic);
        }

        
        [TestMethod]
        public void CreateInvalidId() 
        {
            Guid guid = Guid.NewGuid();
            Topic topic = new Topic();
            topic.Name = "test";
            topic.AreaId = guid;
            Area area = new Area();
            area.Id = guid;

            var mock = new Mock<IRepository<Topic, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.GetParent(guid)).Returns(area);
            mock.Setup(m => m.Add(topic)).Throws(new ExceptionController());

            var controller = new TopicLogic(mock.Object);
            Assert.ThrowsException<ExceptionController>(() => controller.Create(topic));
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetIsOk() 
        {
            Guid guid = Guid.NewGuid();
	        Topic topic = new Topic() 
            {
                Id = guid,
                Name = "Just Testing",
                AreaId = Guid.NewGuid()
	        };
            Topic dummyTopic = new Topic();
            dummyTopic.Id = guid;
            
            var mock = new Mock<IRepository<Topic, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyTopic)).Returns(true);
            mock.Setup(m => m.Get(guid)).Returns(topic);
            var controller = new TopicLogic(mock.Object);
            
            Topic result = controller.Get(guid);
            Assert.AreEqual(topic, result);
        }

        [TestMethod]
        public void GetIsNotOk() 
        {
            Guid guid = Guid.NewGuid();
            Topic dummyTopic = new Topic();
            dummyTopic.Id = guid;

            var mock = new Mock<IRepository<Topic, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyTopic)).Returns(true);
            mock.Setup(m => m.Get(guid)).Throws(new ExceptionController());
            var controller = new TopicLogic(mock.Object);

            Assert.ThrowsException<ExceptionController>(() => controller.Get(guid));
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetAllIsOk() 
        {
	        Topic firstTopicExpected = new Topic() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                AreaId = Guid.NewGuid()
	        };
                
            Topic secondTopicExpected = new Topic() 
            {
                Id = Guid.NewGuid(),
                Name = "Second Just Testing",
                AreaId = Guid.NewGuid()
	        };

            IEnumerable<Topic> topics = new List<Topic>(){ 
                firstTopicExpected, 
                secondTopicExpected 
            };

            var mock = new Mock<IRepository<Topic, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Returns(topics);
            var controller = new TopicLogic(mock.Object);
            
            IEnumerable<Topic> resultList = controller.GetAll();
            Assert.AreEqual(topics, resultList);
        }
        
        [TestMethod]
        public void GetAllNoElements() 
        {
            var mock = new Mock<IRepository<Topic, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Throws(new ArgumentException());
            var controller = new TopicLogic(mock.Object);

            Assert.ThrowsException<ArgumentException>(() => controller.GetAll());
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateCorrect() 
        {
            Guid guid = Guid.NewGuid();
	        Topic topic = new Topic() 
            {
                Id = guid,
                Name = "Transporte"
	        };

            Topic dummyTopic = new Topic();
            dummyTopic.Id = guid;

            var mock = new Mock<IRepository<Topic, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyTopic)).Returns(true);
            mock.Setup(m => m.Get(guid)).Returns(topic);
            mock.Setup(m => m.NameExists(topic)).Returns(false);
            mock.Setup(m => m.Update(topic));
            mock.Setup(m => m.Save());
            var controller = new TopicLogic(mock.Object);

            controller.Update(topic);
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateInvalid() 
        {
            Guid guid = Guid.NewGuid();
            Topic topic = new Topic();
            topic.Id = guid;

            var mock = new Mock<IRepository<Topic, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(topic)).Returns(true);
            mock.Setup(m => m.Get(guid)).Throws(new ExceptionController());
            var controller = new TopicLogic(mock.Object);

            Assert.ThrowsException<ExceptionController>(() => controller.Update(topic));
            mock.VerifyAll();
        }

        [TestMethod]
        public void RemoveValid() 
        {
            Guid guid = Guid.NewGuid();
            Topic topic = new Topic();
            topic.Id = guid;

            var mock = new Mock<IRepository<Topic, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(topic)).Returns(true);
            mock.Setup(m => m.Get(guid)).Returns(topic);
            mock.Setup(m => m.Remove(topic));
            mock.Setup(m => m.Save());
            var controller = new TopicLogic(mock.Object);

            controller.Remove(topic.Id);
            mock.VerifyAll();
        }

        [TestMethod]
        public void RemoveInvalid() 
        {
            Guid guid = Guid.NewGuid();
            Topic topic = new Topic();
            topic.Id = guid;

            var mock = new Mock<IRepository<Topic, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(topic)).Returns(false);
            var controller = new TopicLogic(mock.Object);

            Assert.ThrowsException<ExceptionController>(() => controller.Remove(topic.Id));
            mock.VerifyAll();
        }
    }
}
