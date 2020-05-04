using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System;
using Moq;
using IMMRequest.DataAccess.Interface;
using IMMRequest.Exceptions;

namespace IMMRequest.BusinessLogic.Test
{
    [TestClass]
    public class AdministratorTest : BaseLogicTest<Administrator, Administrator>
    {
        public AdministratorLogic administratorLogic;

        public AdministratorTest() {}

        public override Administrator CreateEntity()
        {
            Administrator administrator = new Administrator() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                Email = "first@test.com",
                Password = "notSecure"
	        };
            return administrator;
        }

        public override BaseLogic<Administrator, Administrator> CreateBaseLogic(IRepository<Administrator, Administrator> obj)
        {
            var administratorLogic = new AdministratorLogic(obj);
            return administratorLogic;
        }

        public override Guid GetId(Administrator entity)
        {
            return entity.Id;
        }

        public override Administrator ModifyEntity(Administrator entity)
        {
            entity.Password = "changedPassword";
            return entity;
        }

        [TestMethod]
        public void CreateCaseNotExist() 
        {
            Guid guid = Guid.NewGuid();
	        Administrator administrator = new Administrator() 
            {
                Id = guid,
                Name = "Just Testing",
                Email = "mail@mail.com",
                Password = "notSecure"
	        };
            
            Guid administratorGuid = Guid.NewGuid();
            Administrator dummyAdministrator = new Administrator();
            dummyAdministrator.Email = "mail@mail.com";

            var mock = new Mock<IRepository<Administrator, Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyAdministrator)).Returns(false);
            mock.Setup(m => m.Add(It.IsAny<Administrator>()));
            mock.Setup(m => m.Save());

            var administratorLogic = new AdministratorLogic(mock.Object);
            Administrator result = administratorLogic.Create(administrator);

            mock.VerifyAll();
            Assert.AreEqual(result, administrator);
        }

        
        [TestMethod]
        public void CreateInvalidId() 
        {
            Administrator administrator = new Administrator();
            administrator.Name = "name";
            administrator.Email = "mail@mail.com";
            administrator.Password = "password";

            Guid administratorGuid = Guid.NewGuid();
            Administrator dummyAdministrator = new Administrator();
            dummyAdministrator.Email = "mail@mail.com";

            var mock = new Mock<IRepository<Administrator, Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyAdministrator)).Returns(false);
            mock.Setup(m => m.Add(administrator)).Throws(new ExceptionController());

            var controller = new AdministratorLogic(mock.Object);
            Assert.ThrowsException<ExceptionController>(() => controller.Create(administrator));
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetIsOk() 
        {
            Guid administratorGuid = Guid.NewGuid();
	        Administrator administrator = new Administrator() 
            {
                Id = administratorGuid,
                Name = "Just Testing",
                Email = "first@test.com",
                Password = "notSecure"
	        };
            
            Administrator dummyAdministrator = new Administrator();
            dummyAdministrator.Id = administratorGuid;
            var mock = new Mock<IRepository<Administrator, Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyAdministrator)).Returns(true);
            mock.Setup(m => m.Get(administratorGuid)).Returns(administrator);
            var controller = new AdministratorLogic(mock.Object);
            
            Administrator result = controller.Get(administratorGuid);
            Assert.AreEqual(administrator, result);
        }

        [TestMethod]
        public void GetIsNotOk() 
        {
            Guid administratorGuid = Guid.NewGuid();
            Administrator dummyAdministrator = new Administrator();
            dummyAdministrator.Id = administratorGuid;

            var mock = new Mock<IRepository<Administrator, Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyAdministrator)).Returns(true);
            mock.Setup(m => m.Get(administratorGuid)).Throws(new ExceptionController());
            var controller = new AdministratorLogic(mock.Object);

            Assert.ThrowsException<ExceptionController>(() => controller.Get(administratorGuid));
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetAllIsOk() 
        {
            Administrator firstAdministratorExpected = new Administrator() 
            {
                Id = Guid.NewGuid(),
                Name = "First Just Testing",
                Email = "newtest@test.com",
                Password = "notSecure"
            };
                
            Administrator secondAdministratorExpected = new Administrator() 
            {
                Id = Guid.NewGuid(),
                Name = "Second Just Testing",
                Email = "newtest@test.com",
                Password = "notSecure"
            };

            IEnumerable<Administrator> administradores = new List<Administrator>(){ 
                firstAdministratorExpected, 
                secondAdministratorExpected 
            };

            var mock = new Mock<IRepository<Administrator, Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Returns(administradores);
            var controller = new AdministratorLogic(mock.Object);
            
            IEnumerable<Administrator> resultList = controller.GetAll();
            Assert.AreEqual(administradores, resultList);
        }
        
        [TestMethod]
        public void GetAllNoElements() 
        {
            var mock = new Mock<IRepository<Administrator, Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Throws(new ArgumentException());
            var controller = new AdministratorLogic(mock.Object);

            Assert.ThrowsException<ArgumentException>(() => controller.GetAll());
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateCorrect() 
        {
            Guid guid = Guid.NewGuid();
	        Administrator administrator = new Administrator();
            administrator.Id = guid;
            var mock = new Mock<IRepository<Administrator, Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(administrator)).Returns(true);
            mock.Setup(m => m.Get(guid)).Returns(administrator);
            mock.Setup(m => m.Update(administrator));
            mock.Setup(m => m.Save());
            var controller = CreateBaseLogic(mock.Object);

            controller.Update(administrator);
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateInvalid() 
        {
            Guid guid = Guid.NewGuid();
            Administrator administrator = new Administrator();
            administrator.Id = guid;
            var mock = new Mock<IRepository<Administrator, Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(administrator)).Returns(true);
            mock.Setup(m => m.Get(guid)).Throws(new ExceptionController());
            var controller = new AdministratorLogic(mock.Object);

            Assert.ThrowsException<ExceptionController>(() => controller.Update(administrator));
            mock.VerifyAll();
        } 

        [TestMethod]
        public void RemoveValid() 
        {
            Guid guid = Guid.NewGuid();
            Administrator administrator = new Administrator();
            administrator.Id = guid;

            var mock = new Mock<IRepository<Administrator, Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(administrator)).Returns(true);
            mock.Setup(m => m.Remove(administrator));
            mock.Setup(m => m.Save());
            var controller = new AdministratorLogic(mock.Object);

            controller.Remove(administrator);
            mock.VerifyAll();
        }

        [TestMethod]
        public void RemoveInvalid() 
        {
            Guid guid = Guid.NewGuid();
            Topic topic = new Topic();
            topic.Id = guid;

            var mock = new Mock<IRepository<Topic, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(topic)).Returns(true);
            mock.Setup(m => m.Remove(topic)).Throws(new ExceptionController());
            var controller = new TopicLogic(mock.Object);

            Assert.ThrowsException<ExceptionController>(() => controller.Remove(topic));
            mock.VerifyAll();
        }
    }
}
