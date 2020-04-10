using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic.Test 
{
    [TestClass]
    public class AdditionalFieldTest 
    {
        public AdditionalFieldLogic additionalFieldLogic;

        public AdditionalFieldTest() {}

        [TestInitialize()]
        public void Initialize()
        {
            this.additionalFieldLogic = new AdditionalFieldLogic();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.additionalFieldLogic = new AdditionalFieldLogic();
        }

        [TestMethod]
        public void CreateIsOk() 
        {
            Guid guid = Guid.NewGuid();
	        AdditionalField additionalField = new AdditionalField() 
            {
                Id = guid,
                Name = "Just Testing",
                FieldType = "Texto",
                Ranges = new ICollection<FieldRange>()
	        };
            AdditionalField result = this.additionalFieldLogic.Create(additionalField);
            Assert.AreEqual(additionalField, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateIdExists() 
        {
            Guid guid = Guid.NewGuid();
            Guid anotherGuid = Guid.NewGuid();

	        AdditionalField additionalFieldExpected = new AdditionalField() 
            {
                Id = guid,
                Name = "Just Testing",
                FieldType = "Texto",
                Ranges = new ICollection<FieldRange>()
	        };
            this.additionalFieldLogic.Add(additionalFieldExpected);
            this.additionalFieldLogic.Save();

            this.additionalFieldLogic.Add(additionalFieldExpected);
            this.additionalFieldLogic.Save();
            
            Assert.AreEqual(additionalFieldExpected, additionalFieldExpected);
        }

        [TestMethod]
        public void RemoveCorrectId() 
        {
            Guid firstGuid = Guid.NewGuid();
            AdditionalField firstAdditionalFieldExpected = new AdditionalField() 
            {
                Id = guid,
                Name = "Just First Testing",
                FieldType = "Texto",
                Ranges = new ICollection<FieldRange>()
	        };
            this.additionalFieldLogic.Add(firstAdditionalFieldExpected);
            
	        AdditionalField secondAdditionalFieldExpected = new AdditionalField() 
            {
                Id = guid,
                Name = "Just Second Testing",
                FieldType = "Texto",
                Ranges = new ICollection<FieldRange>()
	        };
            this.additionalFieldLogic.Add(secondAdditionalFieldExpected);
            this.additionalFieldLogic.Save();

            this.additionalFieldLogic.Remove(firstGuid);

            IEnumerable<AdditionalField> resultList = this.additionalFieldLogic.GetAdditionalFields();
            
            Assert.AreEqual(1, resultList.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveInvalidId() 
        {
            Guid randomGuid = Guid.NewGuid();
	        AdditionalField additionalField = new AdditionalField() 
            {
                Id = guid,
                Name = "Just Testing",
                FieldType = "Texto",
                Ranges = new ICollection<FieldRange>()
	        };
            this.additionalFieldLogic.Add(additionalField);
            this.additionalFieldLogic.Save();

            this.additionalFieldLogic.Remove(randomGuid);
            IEnumerable<AdditionalField> resultList = this.additionalFieldLogic.GetAdditionalFields();
            Assert.AreEqual(1, resultList.Count());
        }

        [TestMethod]
        public void GetIsOk() 
        {
            Guid guid = Guid.NewGuid();

	        AdditionalField additionalFieldExpected = new AdditionalField() 
            {
                Id = guid,
                Name = "Just Testing",
                FieldType = "Texto",
                Ranges = new ICollection<FieldRange>()
	        };
            this.additionalFieldLogic.Add(additionalFieldExpected);
            this.additionalFieldLogic.Save();

            AdditionalField result = this.additionalFieldLogic.Get(guid);
            
            Assert.AreEqual(additionalFieldExpected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetIsNotOk() 
        {
            Guid guid = Guid.NewGuid();
            Guid anotherGuid = Guid.NewGuid();

	        AdditionalField additionalFieldExpected = new AdditionalField() 
            {
                Id = guid,
                Name = "Just Testing",
                FieldType = "Texto",
                Ranges = new ICollection<FieldRange>()
	        };
            this.additionalFieldLogic.Add(additionalFieldExpected);
            this.additionalFieldLogic.Save();

            AdditionalField result = this.additionalFieldLogic.Get(anotherGuid);
            
            Assert.AreEqual(additionalFieldExpected, result);
        }

        [TestMethod]
        public void GetAdditionalFieldsIsOk() 
        {
	        AdditionalField firstAdditionalFieldExpected = new AdditionalField() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                FieldType = "Texto",
                Ranges = new ICollection<FieldRange>()
	        };
            this.additionalFieldLogic.Add(firstAdditionalFieldExpected);
            
	        AdditionalField secondAdditionalFieldExpected = new AdditionalField() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                FieldType = "Texto",
                Ranges = new ICollection<FieldRange>()
	        };
            this.additionalFieldLogic.Add(secondAdditionalFieldExpected);
            this.additionalFieldLogic.Save();

            IEnumerable<AdditionalField> resultList = this.additionalFieldLogic.GetAdditionalFields();
            
            Assert.AreEqual(2, resultList.Count());
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAdditionalFieldsNoElements() 
        {
            IEnumerable<AdditionalField> resultList = this.additionalFieldLogic.GetAdditionalFields();
            Assert.AreEqual(0, resultList.Count());
        }
    }
}