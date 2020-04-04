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
        public readonly AdministratorLogic administratorLogic;

        public AdministratorTest() 
        {
            this.administratorLogic = new AdministratorLogic();
        }


        // Al tener solo un context, si llamo al Count después de haber realizado otros tests, contará a todos los insertados. 
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
            this.administratorLogic.administratorRepository.Add(firstAdministratorExpected);
            
	        Administrator secondAdministratorExpected = new Administrator() 
            {
                Id = Guid.NewGuid(),
                Name = "Second Just Testing",
                Email = "newtest@test.com",
                Password = "notSecure"
	        };
            this.administratorLogic.administratorRepository.Add(secondAdministratorExpected);

            this.administratorLogic.administratorRepository.Save();

            IEnumerable<Administrator> resultList = this.administratorLogic.GetAdministrators();
            
            Assert.AreEqual(2, resultList.Count());
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
            this.administratorLogic.administratorRepository.Add(administratorExpected);
            this.administratorLogic.administratorRepository.Save();

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
            this.administratorLogic.administratorRepository.Add(administratorExpected);
            this.administratorLogic.administratorRepository.Save();

            Administrator result = this.administratorLogic.Get(anotherGuid);
            
            Assert.AreEqual(administratorExpected, result);
        }

        
    }
}
