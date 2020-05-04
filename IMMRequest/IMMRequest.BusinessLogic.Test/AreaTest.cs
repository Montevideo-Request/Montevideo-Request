using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.Domain;
using System;
using IMMRequest.DataAccess.Interface;
using System.Collections.Generic;
using Moq;
using IMMRequest.Exceptions;

namespace IMMRequest.BusinessLogic.Test 
{
    [TestClass]
    public class AreaTest : BaseLogicTest<Area, Area>
    {
        public AreaTest() {}

        public override BaseLogic<Area, Area> CreateBaseLogic(IRepository<Area, Area> obj)
        {
            var controller = new AreaLogic(obj);
            return controller;
        }

        public override Area CreateEntity()
        {
	        Area area = new Area() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing"
	        };
            return area;
        }

        public override Guid GetId(Area entity)
        {
            return entity.Id;
        }

        public override Area ModifyEntity(Area entity)
        {
            entity.Name = "New Name";
            return entity;
        }


        [TestMethod]
        public void CreateCaseNotExist() 
        {
            Guid guid = Guid.NewGuid();
	        Area area = new Area() 
            {
                Id = guid,
                Name = "Just Testing"
	        };
            Area dummyArea = new Area();
            dummyArea.Id = guid;
            dummyArea.Name = "Just Testing";

            var mock = new Mock<IRepository<Area, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyArea)).Returns(false);
            mock.Setup(m => m.Add(It.IsAny<Area>()));
            mock.Setup(m => m.Save());

            var controller = new AreaLogic(mock.Object);
            Area result = controller.Create(area);

            mock.VerifyAll();
            Assert.AreEqual(result, area);
        }

        
        [TestMethod]
        public void CreateInvalidId() 
        {
            Guid guid = Guid.NewGuid();
            Area area = new Area();
            area.Name = "name";
            area.Id = guid;

            Area dummyArea = new Area();
            dummyArea.Id = guid;
            dummyArea.Name = "name";
            var mock = new Mock<IRepository<Area, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyArea)).Returns(false);
            mock.Setup(m => m.Add(area)).Throws(new ExceptionController());

            var controller = new AreaLogic(mock.Object);
            Assert.ThrowsException<ExceptionController>(() => controller.Create(area));
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetIsOk() 
        {
            Guid guid = Guid.NewGuid();
	        Area area = new Area() 
            {
                Id = guid,
                Name = "Just Testing"
	        };

            Area dummyArea = new Area();
            dummyArea.Id = guid;
            
            var mock = new Mock<IRepository<Area, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyArea)).Returns(true);
            mock.Setup(m => m.Get(guid)).Returns(area);
            var controller = new AreaLogic(mock.Object);
            
            Area result = controller.Get(guid);
            Assert.AreEqual(area, result);
        }

        [TestMethod]
        public void GetIsNotOk() 
        {
            Guid guid = Guid.NewGuid();
            Area dummyArea = new Area();
            dummyArea.Id = guid;

            var mock = new Mock<IRepository<Area, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyArea)).Returns(true);
            mock.Setup(m => m.Get(guid)).Throws(new ExceptionController());
            var controller = new AreaLogic(mock.Object);

            Assert.ThrowsException<ExceptionController>(() => controller.Get(guid));
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetAllIsOk() 
        {
            Area firstAreaExpected = new Area() 
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing"
	        };
                
            Area secondAreaExpected = new Area() 
            {
                Id = Guid.NewGuid(),
                Name = "Second Just Testing"
	        };

            IEnumerable<Area> areas = new List<Area>(){ 
                firstAreaExpected, 
                secondAreaExpected 
            };

            var mock = new Mock<IRepository<Area, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Returns(areas);
            var controller = new AreaLogic(mock.Object);
            
            IEnumerable<Area> resultList = controller.GetAll();
            Assert.AreEqual(areas, resultList);
        }
        
        [TestMethod]
        public void GetAllNoElements() 
        {
            var mock = new Mock<IRepository<Area, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Throws(new ArgumentException());
            var controller = new AreaLogic(mock.Object);

            Assert.ThrowsException<ArgumentException>(() => controller.GetAll());
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateCorrect() 
        {
            Guid guid = Guid.NewGuid();
            Area area = new Area();
            area.Id = guid;

            var mock = new Mock<IRepository<Area, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(area)).Returns(true);
            mock.Setup(m => m.Get(guid)).Returns(area);
            mock.Setup(m => m.Update(area));
            mock.Setup(m => m.Save());
            var controller = new AreaLogic(mock.Object);

            controller.Update(area);
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateInvalid() 
        {
            Guid guid = Guid.NewGuid();
            Area area = new Area();
            area.Id = guid;

            var mock = new Mock<IRepository<Area, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(area)).Returns(true);
            mock.Setup(m => m.Get(guid)).Throws(new ExceptionController());
            var controller = new AreaLogic(mock.Object);

            Assert.ThrowsException<ExceptionController>(() => controller.Update(area));
            mock.VerifyAll();
        }

        [TestMethod]
        public void RemoveValid() 
        {
            Guid guid = Guid.NewGuid();
            Area area = new Area();
            area.Id = guid;

            var mock = new Mock<IRepository<Area, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(area)).Returns(true);
            mock.Setup(m => m.Remove(area));
            mock.Setup(m => m.Save());
            var controller = new AreaLogic(mock.Object);

            controller.Remove(area);
            mock.VerifyAll();
        }

        [TestMethod]
        public void RemoveInvalid() 
        {
            Guid guid = Guid.NewGuid();
            Area area = new Area();
            area.Id = guid;

            var mock = new Mock<IRepository<Area, Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(area)).Returns(true);
            mock.Setup(m => m.Remove(area)).Throws(new ExceptionController());
            var controller = new AreaLogic(mock.Object);

            Assert.ThrowsException<ExceptionController>(() => controller.Remove(area));
            mock.VerifyAll();
        }
    }
}