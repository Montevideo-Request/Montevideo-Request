using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.Domain;
using System;
using IMMRequest.DataAccess.Interface;
using Moq;
using System.Collections.Generic;

namespace IMMRequest.BusinessLogic.Test
{
    [TestClass]
    public class TopicTest : BaseLogicTest<Topic>
    {
        public TopicTest() {}

        public override BaseLogic<Topic> CreateBaseLogic(IRepository<Topic> obj)
        {
            throw new NotImplementedException();
        }

        public override Topic CreateEntity()
        {
            throw new NotImplementedException();
        }

        public override Guid GetId(Topic entity)
        {
            throw new NotImplementedException();
        }

        public override Topic GetSavedEntity(BaseLogic<Topic> BaseLogic, Topic Entity)
        {
            throw new NotImplementedException();
        }

        public override Topic ModifyEntity(Topic Entity)
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CreateCaseNotExist() 
        {
            Guid guid = Guid.NewGuid();
	        Topic topic = new Topic() 
            {
                Id = guid,
                Name = "Just Testing",
                AreaId = Guid.NewGuid()
	        };

            var mock = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<Topic>()));
            mock.Setup(m => m.Save());

            var controller = new TopicLogic(mock.Object);
            Guid result = controller.Create(topic);

            mock.VerifyAll();
            Assert.AreEqual(result, guid);
        }

        
        [TestMethod]
        public void CreateInvalidId() 
        {
            Topic topic = new Topic();
            var mock = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(topic)).Throws(new ArgumentException());

            var controller = new TopicLogic(mock.Object);
            Assert.ThrowsException<ArgumentException>(() => controller.Create(topic));
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
            
            var mock = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(guid)).Returns(topic);
            var controller = new TopicLogic(mock.Object);
            
            Topic result = controller.Get(guid);
            Assert.AreEqual(topic, result);
        }

        [TestMethod]
        public void GetIsNotOk() 
        {
            Guid guid = Guid.NewGuid();
            var mock = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(guid)).Throws(new ArgumentException());
            var controller = new TopicLogic(mock.Object);

            Assert.ThrowsException<ArgumentException>(() => controller.Get(guid));
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

            var mock = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Returns(topics);
            var controller = new TopicLogic(mock.Object);
            
            IEnumerable<Topic> resultList = controller.GetAll();
            Assert.AreEqual(topics, resultList);
        }
        
        [TestMethod]
        public void GetAllNoElements() 
        {
            var mock = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Throws(new ArgumentException());
            var controller = new TopicLogic(mock.Object);

            Assert.ThrowsException<ArgumentException>(() => controller.GetAll());
            mock.VerifyAll();
        }
    }
}