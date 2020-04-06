using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic.Tests 
{
    [TestClass]
    public class AdministratorTest 
    {
        public AdministratorLogic administratorLogic;

        public AdministratorTest() {}

        [TestInitialize()]
        public void Initialize()
        {
            this.administratorLogic = new AdministratorLogic();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.administratorLogic = new AdministratorLogic();
        }

        [TestMethod]
        public void CreateIsOk() 
        {
            Guid guid = Guid.NewGuid();
	        Administrator administrator = new Administrator() 
            {
                Id = guid,
                Name = "Just Testing",
                Email = "first@test.com",
                Password = "notSecure"
	        };
            Administrator result = this.administratorLogic.Create(administrator);
            Assert.AreEqual(administrator, result);
        }

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

            this.administratorLogic.Remove(firstGuid);

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
        }
    }
}
