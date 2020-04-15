using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic.Test 
{/*
    [TestClass]
    public class AreaTest 
    {
        public AreaLogic areaLogic;

        public AreaTest() {}

        [TestInitialize()]
        public void Initialize()
        {
            this.areaLogic = new AreaLogic();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.areaLogic = new AreaLogic();
        }

        [TestMethod]
        public void CreateIsOk() 
        {
            Guid guid = Guid.NewGuid();
	        Area area = new Area() 
            {
                Id = guid,
                Name = "Just Testing",
                Ranges = new ICollection<Topic>()
	        };
            Area result = this.areaLogic.Create(area);
            Assert.AreEqual(area, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateIdExists() 
        {
            Guid guid = Guid.NewGuid();
            Guid anotherGuid = Guid.NewGuid();

	        Area areaExpected = new Area() 
            {
                Id = guid,
                Name = "Just Testing",
                Ranges = new ICollection<Topic>()
	        };
            this.areaLogic.Add(areaExpected);
            this.areaLogic.Save();

            this.areaLogic.Add(areaExpected);
            this.areaLogic.Save();
            
            Assert.AreEqual(areaExpected, areaExpected);
        }

        [TestMethod]
        public void RemoveCorrectId() 
        {
            Guid firstGuid = Guid.NewGuid();
            Area firstAreaExpected = new Area() 
            {
                Id = guid,
                Name = "Just Testing",
                Ranges = new ICollection<Topic>()
	        };
            this.areaLogic.Add(firstAreaExpected);
            
	        Area secondAreaExpected = new Area() 
            {
                Id = guid,
                Name = "Just Second Testing",
                Ranges = new ICollection<Topic>()
	        };
            this.areaLogic.Add(secondAreaExpected);
            this.areaLogic.Save();

            this.areaLogic.Remove(firstGuid);

            IEnumerable<Area> resultList = this.areaLogic.GetAreas();
            
            Assert.AreEqual(1, resultList.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveInvalidId() 
        {
            Guid randomGuid = Guid.NewGuid();
	        Area area = new Area() 
            {
                Id = guid,
                Name = "Just Testing",
                Ranges = new ICollection<Topic>()
	        };
            this.areaLogic.Add(area);
            this.areaLogic.Save();

            this.areaLogic.Remove(randomGuid);
            IEnumerable<AdditionalField> resultList = this.areaLogic.GetAreas();
            Assert.AreEqual(1, resultList.Count());
        }

        [TestMethod]
        public void GetIsOk() 
        {
            Guid guid = Guid.NewGuid();

	        Area areaExpected = new Area() 
            {
                Id = guid,
                Name = "Just Testing",
                Ranges = new ICollection<Topic>()
	        };
            this.areaLogic.Add(areaExpected);
            this.areaLogic.Save();

            Area result = this.areaLogic.Get(guid);
            
            Assert.AreEqual(areaExpected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetIsNotOk() 
        {
            Guid guid = Guid.NewGuid();
            Guid anotherGuid = Guid.NewGuid();

	        Area areaExpected = new Area() 
            {
                Id = guid,
                Name = "Just Testing",
                Ranges = new ICollection<Topic>()
	        };
            this.areaLogic.Add(areaExpected);
            this.areaLogic.Save();

            Area result = this.areaLogic.Get(anotherGuid);
            
            Assert.AreEqual(areaExpected, result);
        }

        [TestMethod]
        public void GetAreasIsOk() 
        {
	        Area firstAreaExpected = new Area() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                Ranges = new ICollection<Topic>()
	        };
            this.areaLogic.Add(firstAreaExpected);
            
	        Area secondAreaExpected = new Area() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                Ranges = new ICollection<Topic>()
	        };
            this.areaLogic.Add(secondAreaExpected);
            this.areaLogic.Save();

            IEnumerable<Area> resultList = this.areaLogic.GetAreas();
            
            Assert.AreEqual(2, resultList.Count());
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAreasNoElements() 
        {
            IEnumerable<Area> resultList = this.areaLogic.GetAreas();
            Assert.AreEqual(0, resultList.Count());
        }
    }*/
}