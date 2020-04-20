using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System;
using Moq;
using IMMRequest.DataAccess.Interface;

namespace IMMRequest.BusinessLogic.Test
{
    [TestClass]
    public class AdministratorTest : BaseLogicTest<Administrator>
    {
        public AdministratorLogic administratorLogic;

        public AdministratorTest() {}

        public override Administrator CreateEntity()
        {
            throw new NotImplementedException();
        }

        public override BaseLogic<Administrator> CreateBaseLogic(IRepository<Administrator> obj)
        {
            throw new NotImplementedException();
        }

        public override Guid GetId(Administrator entity)
        {
            throw new NotImplementedException();
        }

        public override Administrator GetSavedEntity(BaseLogic<Administrator> BaseLogic, Administrator Entity)
        {
            throw new NotImplementedException();
        }

        public override Administrator ModifyEntity(Administrator Entity)
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CreateCaseNotExist() 
        {
            Guid guid = Guid.NewGuid();
	        Administrator administrator = new Administrator() 
            {
                Id = guid,
                Name = "Just Testing",
                Email = "first@test.com",
                Password = "notSecure"
	        };

            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<Administrator>()));
            mock.Setup(m => m.Save());

            var administratorLogic = new AdministratorLogic(mock.Object);
            Guid result = administratorLogic.Create(administrator);

            mock.VerifyAll();
            Assert.AreEqual(result, guid);
        }

        
        [TestMethod]
        public void CreateInvalidId() 
        {
            Administrator administrator = new Administrator();
            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(administrator)).Throws(new ArgumentException());

            var controller = new AdministratorLogic(mock.Object);
            Assert.ThrowsException<ArgumentException>(() => controller.Create(administrator));
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
            
            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(administratorGuid)).Returns(administrator);
            var controller = new AdministratorLogic(mock.Object);
            
            Administrator result = controller.Get(administratorGuid);
            Assert.AreEqual(administrator, result);
        }

        [TestMethod]
        public void GetIsNotOk() 
        {
            Guid administratorGuid = Guid.NewGuid();
            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(administratorGuid)).Throws(new ArgumentException());
            var controller = new AdministratorLogic(mock.Object);

            Assert.ThrowsException<ArgumentException>(() => controller.Get(administratorGuid));
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

            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Returns(administradores);
            var controller = new AdministratorLogic(mock.Object);
            
            IEnumerable<Administrator> resultList = controller.GetAll();
            Assert.AreEqual(administradores, resultList);
        }
        
        [TestMethod]
        public void GetAllNoElements() 
        {
            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Throws(new ArgumentException());
            var controller = new AdministratorLogic(mock.Object);

            Assert.ThrowsException<ArgumentException>(() => controller.GetAll());
            mock.VerifyAll();
        }
    }
}
