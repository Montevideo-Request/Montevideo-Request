using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System;
using Moq;
using IMMRequest.DataAccess.Interface;

namespace IMMRequest.BusinessLogic.Test 
{
    [TestClass]
    public class AdditionalFieldTest : BaseLogicTest<AdditionalField>
    {
        public AdditionalFieldLogic additionalFieldLogic;

        public AdditionalFieldTest() {}

        public override BaseLogic<AdditionalField> CreateBaseLogic(IRepository<AdditionalField> obj)
        {
            var controller = new AdditionalFieldLogic(obj);
            return controller;
        }

        public override AdditionalField CreateEntity()
        {
            AdditionalField additionalField = new AdditionalField() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                FieldType = "Field Type",
                Type = new TypeEntity(),
                TypeId = Guid.NewGuid(),
                Request = new Request(),
                RequestId = Guid.NewGuid()
	        };
            
            return additionalField;
        }

        public override Guid GetId(AdditionalField entity)
        {
            return entity.Id;
        }

        public override AdditionalField ModifyEntity(AdditionalField entity)
        {
            entity.Name = "New name";
            return entity;
        }

        [TestMethod]
        public void CreateCaseNotExist() 
        {
            Guid guid = Guid.NewGuid();
	        AdditionalField additionalField = new AdditionalField() 
            {
                Id = guid,
                Name = "Just Testing",
                FieldType = "Field Type",
                Type = new TypeEntity(),
                TypeId = Guid.NewGuid(),
                Request = new Request(),
                RequestId = Guid.NewGuid()
	        };

            var mock = new Mock<IRepository<AdditionalField>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<AdditionalField>()));
            mock.Setup(m => m.Save());

            var controller = new AdditionalFieldLogic(mock.Object);
            AdditionalField result = controller.Create(additionalField);

            mock.VerifyAll();
            Assert.AreEqual(result, additionalField);
        }

        
        [TestMethod]
        public void CreateInvalidId() 
        {
            AdditionalField additionalField = new AdditionalField();
            var mock = new Mock<IRepository<AdditionalField>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(additionalField)).Throws(new ArgumentException());

            var controller = new AdditionalFieldLogic(mock.Object);
            Assert.ThrowsException<ArgumentException>(() => controller.Create(additionalField));
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetIsOk() 
        {
            Guid guid = Guid.NewGuid();
	        AdditionalField additionalField = new AdditionalField() 
            {
                Id = guid,
                Name = "Just Testing",
                FieldType = "Field Type",
                Type = new TypeEntity(),
                TypeId = Guid.NewGuid(),
                Request = new Request(),
                RequestId = Guid.NewGuid()
	        };
            
            var mock = new Mock<IRepository<AdditionalField>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(guid)).Returns(additionalField);
            var controller = new AdditionalFieldLogic(mock.Object);
            
            AdditionalField result = controller.Get(guid);
            Assert.AreEqual(additionalField, result);
        }

        [TestMethod]
        public void GetIsNotOk() 
        {
            Guid guid = Guid.NewGuid();
            var mock = new Mock<IRepository<AdditionalField>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(guid)).Throws(new ArgumentException());
            var controller = new AdditionalFieldLogic(mock.Object);

            Assert.ThrowsException<ArgumentException>(() => controller.Get(guid));
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetAllIsOk() 
        {
            AdditionalField firstAdditionalFieldExpected = new AdditionalField() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                FieldType = "Field Type",
                Type = new TypeEntity(),
                TypeId = Guid.NewGuid(),
                Request = new Request(),
                RequestId = Guid.NewGuid()
	        };
                
            AdditionalField secondAdditionalFieldExpected = new AdditionalField() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                FieldType = "Field Type",
                Type = new TypeEntity(),
                TypeId = Guid.NewGuid(),
                Request = new Request(),
                RequestId = Guid.NewGuid()
	        };

            IEnumerable<AdditionalField> additionalFields = new List<AdditionalField>(){ 
                firstAdditionalFieldExpected, 
                secondAdditionalFieldExpected
            };

            var mock = new Mock<IRepository<AdditionalField>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Returns(additionalFields);
            var controller = new AdditionalFieldLogic(mock.Object);
            
            IEnumerable<AdditionalField> resultList = controller.GetAll();
            Assert.AreEqual(additionalFields, resultList);
        }
        
        [TestMethod]
        public void GetAllNoElements() 
        {
            var mock = new Mock<IRepository<AdditionalField>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Throws(new ArgumentException());
            var controller = new AdditionalFieldLogic(mock.Object);

            Assert.ThrowsException<ArgumentException>(() => controller.GetAll());
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateCorrect() 
        {
	        AdditionalField entity = CreateEntity();
            var mock = new Mock<IRepository<AdditionalField>>(MockBehavior.Strict);
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
            AdditionalField entity = CreateEntity();
            Guid entityGuid = GetId(entity);
            var mock = new Mock<IRepository<AdditionalField>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(entityGuid)).Throws(new ArgumentException());
            var controller = CreateBaseLogic(mock.Object);

            Assert.ThrowsException<ArgumentException>(() => controller.Update(entity));
            mock.VerifyAll();
        } 
    }
}