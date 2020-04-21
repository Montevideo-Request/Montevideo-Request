using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.Domain;
using System;
using IMMRequest.DataAccess.Interface;
using Moq;
using System.Collections.Generic;

namespace IMMRequest.BusinessLogic.Test
{
    [TestClass]
    public class RequestTest : BaseLogicTest<Request>
    {
        public RequestTest() {}

        public override BaseLogic<Request> CreateBaseLogic(IRepository<Request> obj)
        {
            throw new NotImplementedException();
        }

        public override Request CreateEntity()
        {
            throw new NotImplementedException();
        }

        public override Guid GetId(Request entity)
        {
            throw new NotImplementedException();
        }

        public override Request GetSavedEntity(BaseLogic<Request> BaseLogic, Request Entity)
        {
            throw new NotImplementedException();
        }

        public override Request ModifyEntity(Request Entity)
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CreateCaseNotExist() 
        {
            Guid guid = Guid.NewGuid();
	        Request request = new Request() 
            {
                Id = guid,
                RequestorsName = "Just Testing",
                RequestorsEmail = "first@test.com",
                RequestorsPhone = "489498948894",
                TypeId = Guid.NewGuid(),
                State = "State",
                Description = "description"
	        };

            var mock = new Mock<IRepository<Request>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(It.IsAny<Request>()));
            mock.Setup(m => m.Save());

            var controller = new RequestLogic(mock.Object);
            Guid result = controller.Create(request);

            mock.VerifyAll();
            Assert.AreEqual(result, guid);
        }

        
        [TestMethod]
        public void CreateInvalidId() 
        {
            Request request = new Request();
            var mock = new Mock<IRepository<Request>>(MockBehavior.Strict);
            mock.Setup(m => m.Add(request)).Throws(new ArgumentException());

            var controller = new RequestLogic(mock.Object);
            Assert.ThrowsException<ArgumentException>(() => controller.Create(request));
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetIsOk() 
        {
            Guid guid = Guid.NewGuid();
	        Request request = new Request() 
            {
                Id = guid,
                RequestorsName = "Just Testing",
                RequestorsEmail = "first@test.com",
                RequestorsPhone = "489498948894",
                TypeId = Guid.NewGuid(),
                State = "State",
                Description = "description"
	        };
            
            var mock = new Mock<IRepository<Request>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(guid)).Returns(request);
            var controller = new RequestLogic(mock.Object);
            
            Request result = controller.Get(guid);
            Assert.AreEqual(request, result);
        }

        [TestMethod]
        public void GetIsNotOk() 
        {
            Guid guid = Guid.NewGuid();
            var mock = new Mock<IRepository<Request>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(guid)).Throws(new ArgumentException());
            var controller = new RequestLogic(mock.Object);

            Assert.ThrowsException<ArgumentException>(() => controller.Get(guid));
            mock.VerifyAll();
        }

        [TestMethod]
        public void GetAllIsOk() 
        {
            Request firstRequestExpected = new Request() 
            {
                Id = Guid.NewGuid(),
                RequestorsName = "Just Testing",
                RequestorsEmail = "first@test.com",
                RequestorsPhone = "489498948894",
                TypeId = Guid.NewGuid(),
                State = "State",
                Description = "description"
	        };
                
            Request secondRequestExpected = new Request() 
            {
                Id = Guid.NewGuid(),
                RequestorsName = "Second Testing",
                RequestorsEmail = "second@test.com",
                RequestorsPhone = "5445864565",
                TypeId = Guid.NewGuid(),
                State = "State",
                Description = "description"
	        };

            IEnumerable<Request> requests = new List<Request>(){ 
                firstRequestExpected, 
                secondRequestExpected 
            };

            var mock = new Mock<IRepository<Request>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Returns(requests);
            var controller = new RequestLogic(mock.Object);
            
            IEnumerable<Request> resultList = controller.GetAll();
            Assert.AreEqual(requests, resultList);
        }
        
        [TestMethod]
        public void GetAllNoElements() 
        {
            var mock = new Mock<IRepository<Request>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Throws(new ArgumentException());
            var controller = new RequestLogic(mock.Object);

            Assert.ThrowsException<ArgumentException>(() => controller.GetAll());
            mock.VerifyAll();
        }
    }
}