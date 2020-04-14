using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess.Test
{
    [TestClass]
    public class TopicRepositoryTest : BaseRepositoryTest<Topic>
    {

        public override Topic CreateEntity()
        {
            Topic Topic = new Topic();
            return Topic;
        }

         public override Topic ModifyEntity(Topic Topic, string prop)
        {
            Topic ModifiedTopic = Topic;
            ModifiedTopic.Name = prop;
            return ModifiedTopic;
        }

        public override string GetEntityProp()
        {
            return "New Property to test";
        }

        public override Boolean CompareProps(Topic Topic, string prop)
        {
            return Topic.Name == prop;
        }

        public override Topic GetSavedEntity(BaseRepository<Topic> TopicRepo, Topic Topic)
        {
            Topic TopicToReturn = TopicRepo.Get(Topic.Id);
            return TopicToReturn;
        }

        public override BaseRepository<Topic> CreateRepository()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            TopicRepository topicRepo = new TopicRepository(IMMRequestContext);

            return topicRepo;
        }

        [TestMethod]
        public void TestTopicGetAllOK()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            TopicRepository topicRepo = new TopicRepository(IMMRequestContext);

            topicRepo.Add(new Topic
            {
                Id = Guid.NewGuid(),
                Name = "Alumbrado",
                Area = new Area()
            });

            topicRepo.Save();

            var topics = topicRepo.GetAll().ToList().Count();
            Assert.AreEqual(1, topics);
        }

         [TestMethod]
        public void TestTopicGetAllOK2()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            TopicRepository topicRepo = new TopicRepository(IMMRequestContext);

            topicRepo.Add(new Topic
            {
                Id = Guid.NewGuid(),
                Name = "Alumbrado",
                Area = new Area()
            });

            topicRepo.Add(new Topic
            {
                Id = Guid.NewGuid(),
                Name = "Ambulancias",
                Area = new Area()
            });

            topicRepo.Save();

            var topics = topicRepo.GetAll().ToList().Count();
            Assert.AreEqual(2, topics);
        }

        
        [TestMethod]
        public void TestTopicGetAll3()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            TopicRepository topicRepo = new TopicRepository(IMMRequestContext);
            Topic topic = new Topic()
            {
                Id = Guid.NewGuid(),
                Name = "Alumbrado",
                Area = new Area()
            };

            topicRepo.Add(topic);
            topicRepo.Save();

            var topics = topicRepo.GetAll().ToList();

            Assert.AreEqual(topics.First(), topic);
        }


        [TestMethod]
        public void TestTopicGet()
        {
            var id = Guid.NewGuid();

            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            TopicRepository topicRepo = new TopicRepository(IMMRequestContext);
            Topic topic = new Topic()
            {
                Id = id,
                Name = "Alumbrado",
                Area = new Area()
            };

            topicRepo.Add(topic);
            topicRepo.Save();

            Assert.AreEqual(topicRepo.Get(id), topic);
            
        }

        [TestMethod]
        public void TestTopicGet2()
        {
            
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            TopicRepository topicRepo = new TopicRepository(IMMRequestContext);
         
            var id = Guid.NewGuid();   
            
            Topic topic1 = new Topic()
            {
                Id = Guid.NewGuid(),
                Name = "Alumbrado",
                Area = new Area()
            };

            Topic topic2 = new Topic()
            {
                Id = id,
                Name = "Ambulancias",
                Area = new Area()
            };

            
            topicRepo.Add(topic1);
            topicRepo.Add(topic2);
            topicRepo.Save();

            Assert.AreEqual(topicRepo.Get(id), topic2);
        }
    }
}
