using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
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
    }
}
