using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using IMMRequest.DataAccess.Interface;
using IMMRequest.Domain;
using Moq;
using System.Collections.Generic;
using IMMRequest.Exceptions;

namespace IMMRequest.BusinessLogic.Test
{
    [TestClass]
    public class TypeTest : BaseLogicTest<TypeEntity, Topic>
    {
        public TypeTest() {}

        public override BaseLogic<TypeEntity, Topic> CreateBaseLogic(IRepository<TypeEntity, Topic> obj)
        {
            var controller = new TypeLogic(obj);
            return controller;
        }

        public override TypeEntity CreateEntity()
        {
            TypeEntity type = new TypeEntity() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                TopicId = Guid.NewGuid()
	        };
            return type;
        }

        public override Guid GetId(TypeEntity entity)
        {
            return entity.Id;
        }

        public override TypeEntity ModifyEntity(TypeEntity entity)
        {
            entity.Name = "New Name";
            return entity;
        }

        [TestMethod]
        public void CreateCaseNotExist() 
        {
            Guid guid = Guid.NewGuid();
            Guid secondGuid = Guid.NewGuid();
	        TypeEntity type = new TypeEntity() 
            {
                Id = guid,
                Name = "Just Testing",
                TopicId = secondGuid
	        };

            Topic topic = new Topic();
            topic.Id = secondGuid;

            var mock = new Mock<IRepository<TypeEntity, Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.GetParent(secondGuid)).Returns(topic);
            mock.Setup(m => m.Add(It.IsAny<TypeEntity>()));
            mock.Setup(m => m.Save());

            var controller = new TypeLogic(mock.Object);
            TypeEntity result = controller.Create(type);

            mock.VerifyAll();
            Assert.AreEqual(result, type);
        }

        
        [TestMethod]
        public void CreateInvalidId() 
        {
            Guid guid = Guid.NewGuid();
            TypeEntity type = new TypeEntity();
            type.Name = "test";
            type.TopicId = guid;

            Topic topic = new Topic();
            topic.Id = guid;

            var mock = new Mock<IRepository<TypeEntity, Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.GetParent(guid)).Returns(topic);
            mock.Setup(m => m.Add(type)).Throws(new ExceptionController());

            var controller = new TypeLogic(mock.Object);
            Assert.ThrowsException<ExceptionController>(() => controller.Create(type));
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetIsOk() 
        {
            Guid guid = Guid.NewGuid();
	        TypeEntity type = new TypeEntity() 
            {
                Id = guid,
                Name = "Just Testing",
                TopicId = Guid.NewGuid()
	        };
            
            
            TypeEntity dummyType = new TypeEntity();
            dummyType.Id = guid;
            var mock = new Mock<IRepository<TypeEntity, Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyType)).Returns(true);
            mock.Setup(m => m.Get(guid)).Returns(type);
            var controller = new TypeLogic(mock.Object);
            
            TypeEntity result = controller.Get(guid);
            Assert.AreEqual(type, result);
        }

        [TestMethod]
        public void GetIsNotOk() 
        {
            Guid guid = Guid.NewGuid();
            TypeEntity dummyType = new TypeEntity();
            dummyType.Id = guid;
            var mock = new Mock<IRepository<TypeEntity, Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyType)).Returns(true);
            mock.Setup(m => m.Get(guid)).Throws(new ExceptionController());
            var controller = new TypeLogic(mock.Object);

            Assert.ThrowsException<ExceptionController>(() => controller.Get(guid));
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetAllIsOk() 
        {
	        TypeEntity firstTypeExpected = new TypeEntity() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                TopicId = Guid.NewGuid()
	        };
                
            TypeEntity secondTypeExpected = new TypeEntity() 
            {
                Id = Guid.NewGuid(),
                Name = "Second Just Testing",
                TopicId = Guid.NewGuid()
	        };

            IEnumerable<TypeEntity> types = new List<TypeEntity>(){ 
                firstTypeExpected, 
                secondTypeExpected 
            };

            var mock = new Mock<IRepository<TypeEntity, Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Returns(types);
            var controller = new TypeLogic(mock.Object);
            
            IEnumerable<TypeEntity> resultList = controller.GetAll();
            Assert.AreEqual(types, resultList);
        }
        
        [TestMethod]
        public void GetAllNoElements() 
        {
            var mock = new Mock<IRepository<TypeEntity, Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Throws(new ArgumentException());
            var controller = new TypeLogic(mock.Object);

            Assert.ThrowsException<ArgumentException>(() => controller.GetAll());
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateCorrect() 
        {
	        TypeEntity entity = CreateEntity();
            var mock = new Mock<IRepository<TypeEntity, Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(GetId(entity))).Returns(entity);
            mock.Setup(m => m.Update(ModifyEntity(entity)));
            mock.Setup(m => m.Save());
            var controller = CreateBaseLogic(mock.Object);

            controller.Update(entity);
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateInvalid() 
        {
            TypeEntity entity = CreateEntity();
            Guid entityGuid = GetId(entity);
            var mock = new Mock<IRepository<TypeEntity, Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(entityGuid)).Throws(new ArgumentException());
            var controller = CreateBaseLogic(mock.Object);

            Assert.ThrowsException<ExceptionController>(() => controller.Update(entity));
            mock.VerifyAll();
        } 
    }
}