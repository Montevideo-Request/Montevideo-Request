using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using IMMRequest.DataAccess.Interface;
using IMMRequest.Domain;
using Moq;
using System.Collections.Generic;

namespace IMMRequest.BusinessLogic.Test
{
    [TestClass]
    public class TypeTest : BaseLogicTest<Type>
    {
        public TypeTest() {}

        public override BaseLogic<Type> CreateBaseLogic(IRepository<Type> obj)
        {
            throw new NotImplementedException();
        }

        public override Type CreateEntity()
        {
            throw new NotImplementedException();
        }

        public override Guid GetId(Type entity)
        {
            throw new NotImplementedException();
        }

        public override Type GetSavedEntity(BaseLogic<Type> BaseLogic, Type Entity)
        {
            throw new NotImplementedException();
        }

        public override Type ModifyEntity(Type Entity)
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CreateCaseNotExist() 
        {
            Guid guid = Guid.NewGuid();
	        TypeEntity type = new TypeEntity() 
            {
                Id = guid,
                Name = "Just Testing",
                TopicId = Guid.NewGuid()
	        };

            var mock = new Mock<IRepository<TypeEntity>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<TypeEntity>()));
            mock.Setup(m => m.Save());

            var controller = new TypeLogic(mock.Object);
            Guid result = controller.Create(type);

            mock.VerifyAll();
            Assert.AreEqual(result, guid);
        }

        
        [TestMethod]
        public void CreateInvalidId() 
        {
            TypeEntity type = new TypeEntity();
            var mock = new Mock<IRepository<TypeEntity>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(type)).Throws(new ArgumentException());

            var controller = new TypeLogic(mock.Object);
            Assert.ThrowsException<ArgumentException>(() => controller.Create(type));
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
            
            var mock = new Mock<IRepository<TypeEntity>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(guid)).Returns(type);
            var controller = new TypeLogic(mock.Object);
            
            TypeEntity result = controller.Get(guid);
            Assert.AreEqual(type, result);
        }

        [TestMethod]
        public void GetIsNotOk() 
        {
            Guid guid = Guid.NewGuid();
            var mock = new Mock<IRepository<TypeEntity>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(guid)).Throws(new ArgumentException());
            var controller = new TypeLogic(mock.Object);

            Assert.ThrowsException<ArgumentException>(() => controller.Get(guid));
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

            var mock = new Mock<IRepository<TypeEntity>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Returns(types);
            var controller = new TypeLogic(mock.Object);
            
            IEnumerable<TypeEntity> resultList = controller.GetAll();
            Assert.AreEqual(types, resultList);
        }
        
        [TestMethod]
        public void GetAllNoElements() 
        {
            var mock = new Mock<IRepository<TypeEntity>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Throws(new ArgumentException());
            var controller = new TypeLogic(mock.Object);

            Assert.ThrowsException<ArgumentException>(() => controller.GetAll());
            mock.VerifyAll();
        }
    }
}