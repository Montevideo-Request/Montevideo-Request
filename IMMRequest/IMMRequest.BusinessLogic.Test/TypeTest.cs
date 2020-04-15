using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic.Test
{/*
    [TestClass]
    public class TypeTest 
    {
        public TypeLogic typeLogic;

        public TypeTest() {}

        [TestInitialize()]
        public void Initialize()
        {
            this.typeLogic = new TypeLogic();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.typeLogic = new TypeLogic();
        }

        [TestMethod]
        public void CreateIsOk() 
        {
            Guid guid = Guid.NewGuid();
	        Type type = new Type() 
            {
                Id = guid,
                Name = "Just Testing",
                Topic = new Topic(),
                AdditionalFields = new ICollection<AdditionalField>()
	        };
            Type result = this.typeLogic.Create(type);
            Assert.AreEqual(type, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateIdExists() 
        {
            Guid guid = Guid.NewGuid();
            Guid anotherGuid = Guid.NewGuid();

	        Type typeExpected = new Type() 
            {
                Id = guid,
                Name = "Just Testing",
                Topic = new Topic(),
                AdditionalFields = new ICollection<AdditionalField>()
	        };
            this.typeLogic.Add(typeExpected);
            this.typeLogic.Save();

            this.typeLogic.Add(typeExpected);
            this.typeLogic.Save();
            
            Assert.AreEqual(typeExpected, typeExpected);
        }

        [TestMethod]
        public void RemoveCorrectId() 
        {
            Guid firstGuid = Guid.NewGuid();
            Type firstTypeExpected = new Type() 
            {
                Id = guid,
                Name = "Just Testing",
                Topic = new Topic(),
                AdditionalFields = new ICollection<AdditionalField>()
	        };
            this.typeLogic.Add(firstTypeExpected);
            
	        Type secondTypeExpected = new Type() 
            {
                Id = guid,
                Name = "Just Second Testing",
                Topic = new Topic(),
                AdditionalFields = new ICollection<AdditionalField>()
	        };
            this.typeLogic.Add(secondTypeExpected);
            this.typeLogic.Save();

            this.typeLogic.Remove(firstGuid);

            IEnumerable<Type> resultList = this.typeLogic.GetTypes();
            
            Assert.AreEqual(1, resultList.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveInvalidId() 
        {
            Guid randomGuid = Guid.NewGuid();
	        Type type = new Type() 
            {
                Id = guid,
                Name = "Just Testing",
                Topic = new Topic(),
                AdditionalFields = new ICollection<AdditionalField>()
	        };
            this.typeLogic.Add(type);
            this.typeLogic.Save();

            this.typeLogic.Remove(randomGuid);
            IEnumerable<Type> resultList = this.typeLogic.GetTypes();
            Assert.AreEqual(1, resultList.Count());
        }

        [TestMethod]
        public void GetIsOk() 
        {
            Guid guid = Guid.NewGuid();

	        Type areaExpected = new Type() 
            {
                Id = guid,
                Name = "Just Testing",
                Topic = new Topic(),
                AdditionalFields = new ICollection<AdditionalField>()
	        };
            this.typeLogic.Add(typeExpected);
            this.typeLogic.Save();

            Area result = this.typeLogic.Get(guid);
            
            Assert.AreEqual(typeExpected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetIsNotOk() 
        {
            Guid guid = Guid.NewGuid();
            Guid anotherGuid = Guid.NewGuid();

	        Type typeExpected = new Type() 
            {
                Id = guid,
                Name = "Just Testing",
                Topic = new Topic(),
                AdditionalFields = new ICollection<AdditionalField>()
	        };
            this.typeLogic.Add(typeExpected);
            this.typeLogic.Save();

            Type result = this.typeLogic.Get(anotherGuid);
            
            Assert.AreEqual(typeExpected, result);
        }

        [TestMethod]
        public void GetTypesIsOk() 
        {
	        Type firstTypeExpected = new Type() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                Topic = new Topic(),
                AdditionalFields = new ICollection<AdditionalField>()
	        };
            this.typeLogic.Add(firstTypeExpected);
            
	        Type secondTypeExpected = new Type() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                Topic = new Topic(),
                AdditionalFields = new ICollection<AdditionalField>()
	        };
            this.typeLogic.Add(secondTypeExpected);
            this.typeLogic.Save();

            IEnumerable<Type> resultList = this.areaLogic.GetTypes();
            
            Assert.AreEqual(2, resultList.Count());
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetTypesNoElements() 
        {
            IEnumerable<Type> resultList = this.typeLogic.GetTypes();
            Assert.AreEqual(0, resultList.Count());
        }
    }*/
}