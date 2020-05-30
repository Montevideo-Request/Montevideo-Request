using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
using System;
using Moq;

namespace IMMRequest.BusinessLogic.Test
{
    [TestClass]
    public class AdministratorTest
    {
        public AdministratorLogic administratorLogic;

        public AdministratorTest() {}

        public Administrator CreateEntity()
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

        public Guid GetId(Administrator entity)
        {
            return entity.Id;
        }

        public Administrator ModifyEntity(Administrator entity)
        {
            entity.Password = "changedPassword";
            return entity;
        }

        [TestMethod]
        public void CreateCaseNotExist() 
        {
	        Administrator administrator = new Administrator() 
            {
                Name = "Just Testing",
                Email = "mail@mail.com",
                Password = "notSecure"
	        };
            
            Administrator dummyAdministrator = new Administrator();
            dummyAdministrator.Email = "mail@mail.com";
            dummyAdministrator.Name = "Just Testing";
            dummyAdministrator.Password = "notSecure";

            var mock = new Mock<IAdministratorRepository<Administrator>>(MockBehavior.Strict);
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

            Administrator dummyAdministrator = new Administrator();
            dummyAdministrator.Name = "name";
            dummyAdministrator.Email = "mail@mail.com";
            dummyAdministrator.Password = "password";

            var mock = new Mock<IAdministratorRepository<Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyAdministrator)).Returns(true);

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

            var mock = new Mock<IAdministratorRepository<Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyAdministrator.Id)).Returns(true);
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

            var mock = new Mock<IAdministratorRepository<Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyAdministrator.Id)).Returns(false);
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

            var mock = new Mock<IAdministratorRepository<Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Returns(administradores);
            var controller = new AdministratorLogic(mock.Object);
            
            IEnumerable<Administrator> resultList = controller.GetAll();
            Assert.AreEqual(administradores, resultList);
        }
        
        [TestMethod]
        public void GetAllNoElements() 
        {
            var mock = new Mock<IAdministratorRepository<Administrator>>(MockBehavior.Strict);
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
            administrator.Email = "admin@admin.com";
            administrator.Name = "Joaquin";
            administrator.Password = "qwe123";
            
            var mock = new Mock<IAdministratorRepository<Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(administrator.Id)).Returns(true);
            mock.Setup(m => m.Exist(administrator)).Returns(false);
            mock.Setup(m => m.Get(guid)).Returns(administrator);
            mock.Setup(m => m.Update(administrator));
            mock.Setup(m => m.Save());
            var controller = new AdministratorLogic(mock.Object);

            controller.Update(administrator);
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateInvalid() 
        {
            Guid guid = Guid.NewGuid();
            Administrator administrator = new Administrator();
            administrator.Id = guid;
            
            var mock = new Mock<IAdministratorRepository<Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(administrator.Id)).Returns(false);
            var controller = new AdministratorLogic(mock.Object);

            Assert.ThrowsException<ExceptionController>(() => controller.Update(administrator));
            mock.VerifyAll();
        } 

        [TestMethod]
        public void RemoveValid() 
        {
            Guid guid = Guid.NewGuid();
            Administrator administrator = new Administrator()
            {
                Id = guid,
                Email = "test@test.com",
                Password = "qwe123",
                Name = "Joaquin"
            };

            var mock = new Mock<IAdministratorRepository<Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(administrator.Id)).Returns(true);
            mock.Setup(m => m.Get(guid)).Returns(administrator);
            mock.Setup(m => m.Remove(administrator));
            mock.Setup(m => m.Save());
            var controller = new AdministratorLogic(mock.Object);

            controller.Remove(administrator.Id);
            mock.VerifyAll();
        }
    }
}
