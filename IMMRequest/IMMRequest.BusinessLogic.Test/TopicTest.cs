using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic.Test
{/*
    [TestClass]
    public class TopicTest 
    {
        public TopicLogic topicLogic;

        public TopicTest() {}

        [TestInitialize()]
        public void Initialize()
        {
            this.topicLogic = new TopicLogic();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.topicLogic = new TopicLogic();
        }

        [TestMethod]
        public void CreateIsOk() 
        {
            Guid guid = Guid.NewGuid();
	        Topic topic = new Topic() 
            {
                Id = guid,
                Name = "Just Testing",
                Area = new Area()
	        };
            Topic result = this.topicLogic.Create(area);
            Assert.AreEqual(topic, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateIdExists() 
        {
            Guid guid = Guid.NewGuid();
            Guid anotherGuid = Guid.NewGuid();

	        Topic topicExpected = new Topic() 
            {
                Id = guid,
                Name = "Just Testing",
                Area = new Area()
	        };
            this.topicLogic.Add(topicExpected);
            this.topicLogic.Save();

            this.topicLogic.Add(topicExpected);
            this.topicLogic.Save();
            
            Assert.AreEqual(topicExpected, topicExpected);
        }

        [TestMethod]
        public void RemoveCorrectId() 
        {
            Guid firstGuid = Guid.NewGuid();
            Topic firstTopicExpected = new Topic() 
            {
                Id = guid,
                Name = "Just Testing",
                Area = new Area()
	        };
            this.topicLogic.Add(firstTopicExpected);
            
	        Topic secondTopicExpected = new Topic() 
            {
                Id = guid,
                Name = "Just Second Testing",
                Area = new Area()
	        };
            this.topicLogic.Add(secondTopicExpected);
            this.topicLogic.Save();

            this.topicLogic.Remove(firstGuid);

            IEnumerable<Topic> resultList = this.topicLogic.GetTopics();
            
            Assert.AreEqual(1, resultList.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveInvalidId() 
        {
            Guid randomGuid = Guid.NewGuid();
	        Topic topic = new Area() 
            {
                Id = guid,
                Name = "Just Testing",
                Area = new Area()
	        };
            this.topicLogic.Add(topic);
            this.topicLogic.Save();

            this.topicLogic.Remove(randomGuid);
            IEnumerable<Topic> resultList = this.topicLogic.GetTopics();
            Assert.AreEqual(1, resultList.Count());
        }

        [TestMethod]
        public void GetIsOk() 
        {
            Guid guid = Guid.NewGuid();

	        Topic topicExpected = new Topic() 
            {
                Id = guid,
                Name = "Just Testing",
                Area = new Area()
	        };
            this.topicLogic.Add(topicExpected);
            this.topicLogic.Save();

            Topic result = this.topicLogic.Get(guid);
            
            Assert.AreEqual(topicExpected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetIsNotOk() 
        {
            Guid guid = Guid.NewGuid();
            Guid anotherGuid = Guid.NewGuid();

	        Topic topicExpected = new Topic() 
            {
                Id = guid,
                Name = "Just Testing",
                Area = new Area()
	        };
            this.topicLogic.Add(topicExpected);
            this.topicLogic.Save();

            Topic result = this.topicLogic.Get(anotherGuid);
            
            Assert.AreEqual(topicExpected, result);
        }

        [TestMethod]
        public void GetTopicsIsOk() 
        {
	        Topic firsTopicExpected = new Topic() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                Area = new Area()
	        };
            this.topicLogic.Add(firstTopicExpected);
            
	        Topic secondTopicExpected = new Topic() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                Area = new Area()
	        };
            this.topicLogic.Add(secondTopicExpected);
            this.topicLogic.Save();

            IEnumerable<Topic> resultList = this.topicLogic.GetTopics();
            
            Assert.AreEqual(2, resultList.Count());
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetTopicsNoElements() 
        {
            IEnumerable<Topic> resultList = this.topicLogic.GetTopics();
            Assert.AreEqual(0, resultList.Count());
        }
    }*/
}