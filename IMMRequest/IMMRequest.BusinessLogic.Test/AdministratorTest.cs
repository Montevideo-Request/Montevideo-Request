using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;
using Moq;
using IMMRequest.DataAccess.Interface;

namespace IMMRequest.BusinessLogic.Test
{
    [TestClass]
    public class AdministratorTest 
    {
        public AdministratorLogic administratorLogic;

        public AdministratorTest() {}

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

        /*
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateIdExists() 
        {
            Guid guid = Guid.NewGuid();
            Guid anotherGuid = Guid.NewGuid();

	        Administrator administratorExpected = new Administrator() 
            {
                Id = guid,
                Name = "Just Testing",
                Email = "first@test.com",
                Password = "notSecure"
	        };
            this.administratorLogic.Add(administratorExpected);
            this.administratorLogic.Save();

            this.administratorLogic.Add(administratorExpected);
            this.administratorLogic.Save();
            
            Assert.AreEqual(administratorExpected, administratorExpected);
        }

        [TestMethod]
        public void RemoveCorrectId() 
        {
            Guid firstGuid = Guid.NewGuid();
            Administrator firstAdministratorExpected = new Administrator() 
            {
                Id = firstGuid,
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

            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<Administrator>()));
            mock.Setup(m => m.Save());
            var administratorLogic = new AdministratorLogic(mock.Object);

            Guid firstAdminId = administratorLogic.Create(firstAdministratorExpected);
            Guid secondAdminId = administratorLogic.Create(secondAdministratorExpected);
            administratorLogic.Remove(firstAdministratorExpected);
            
            mock.VerifyAll();
            IEnumerable<Administrator> resultList = this.administratorLogic.GetAdministrators();
            Assert.AreEqual(1, resultList.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveInvalidId() 
        {
            Guid randomGuid = Guid.NewGuid();
	        Administrator administrator = new Administrator() 
            {
                Id = Guid.NewGuid(),
                Name = "First Just Testing",
                Email = "newtest@test.com",
                Password = "seemsSecure"
	        };
            this.administratorLogic.Add(administrator);
            this.administratorLogic.Save();

            this.administratorLogic.Remove(randomGuid);
            IEnumerable<Administrator> resultList = this.administratorLogic.GetAdministrators();
            Assert.AreEqual(1, resultList.Count());
        }
        [TestMethod]
        public void UpdateValidId() 
        {
            Guid guid = Guid.NewGuid();

	        Administrator administrator = new Administrator() 
            {
                Id = guid,
                Name = "Just Testing",
                Email = "first@test.com",
                Password = "notSecure"
	        };
            this.administratorLogic.Add(administrator);
            this.administratorLogic.Save();

            Administrator administratorUpdated = new Administrator() 
            {
                Id = guid,
                Name = "Just Updating",
                Email = "firstUpdated@test.com",
                Password = "seemsSecure"
	        };
            this.administratorLogic.Update(guid, administratorUpdated);
            Administrator result = this.administratorLogic.Get(guid);

            Assert.AreEqual(administratorUpdated.Email, result.Email);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateInvalidId() 
        {
            Guid guid = Guid.NewGuid();
	        Administrator administrator = new Administrator() 
            {
                Id = guid,
                Name = "Just Testing",
                Email = "first@test.com",
                Password = "notSecure"
	        };

            this.administratorLogic.Update(guid, administrator);
            IEnumerable<Administrator> resultList = this.administratorLogic.GetAdministrators();
            Assert.AreEqual(0, resultList.Count());
        }

        [TestMethod]
        public void GetIsOk() 
        {
            Guid guid = Guid.NewGuid();

	        Administrator administratorExpected = new Administrator() 
            {
                Id = guid,
                Name = "Just Testing",
                Email = "first@test.com",
                Password = "notSecure"
	        };
            this.administratorLogic.Add(administratorExpected);
            this.administratorLogic.Save();

            Administrator result = this.administratorLogic.Get(guid);
            
            Assert.AreEqual(administratorExpected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetIsNotOk() 
        {
            Guid guid = Guid.NewGuid();
            Guid anotherGuid = Guid.NewGuid();

	        Administrator administratorExpected = new Administrator() 
            {
                Id = guid,
                Name = "Just Testing",
                Email = "first@test.com",
                Password = "notSecure"
	        };
            this.administratorLogic.Add(administratorExpected);
            this.administratorLogic.Save();

            Administrator result = this.administratorLogic.Get(anotherGuid);
            
            Assert.AreEqual(administratorExpected, result);
        }

        [TestMethod]
        public void GetAdministratorsIsOk() 
        {
	        Administrator firstAdministratorExpected = new Administrator() 
            {
                Id = Guid.NewGuid(),
                Name = "First Just Testing",
                Email = "newtest@test.com",
                Password = "notSecure"
	        };
            this.administratorLogic.Add(firstAdministratorExpected);
            
	        Administrator secondAdministratorExpected = new Administrator() 
            {
                Id = Guid.NewGuid(),
                Name = "Second Just Testing",
                Email = "newtest@test.com",
                Password = "notSecure"
	        };
            this.administratorLogic.Add(secondAdministratorExpected);
            this.administratorLogic.Save();

            IEnumerable<Administrator> resultList = this.administratorLogic.GetAdministrators();
            
            Assert.AreEqual(2, resultList.Count());
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAdministratorsNoElements() 
        {
            IEnumerable<Administrator> resultList = this.administratorLogic.GetAdministrators();
            Assert.AreEqual(0, resultList.Count());
        }*/
    }
}
