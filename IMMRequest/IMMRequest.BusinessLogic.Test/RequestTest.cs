using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.Domain;
using System;
using IMMRequest.DataAccess.Interface;
using Moq;
using System.Collections.Generic;
using IMMRequest.Exceptions;

namespace IMMRequest.BusinessLogic.Test
{
    [TestClass]
    public class RequestTest
    {
        public RequestTest() {}

        public Request CreateEntity()
        {
            Request request = new Request() 
            {
                Id = Guid.NewGuid(),
                RequestorsName = "Just Testing",
                RequestorsEmail = "first@test.com",
                RequestorsPhone = "489498948894",
                TypeId = Guid.NewGuid(),
                State = "State",
                Description = "description"
	        };
            return request;
        }

        public Guid GetId(Request entity)
        {
            return entity.Id;
        }

        public Request ModifyEntity(Request entity)
        {
            entity.RequestorsName = "New Name";
            return entity;
        }

        [TestMethod]
        public void CreateCaseNotExist() 
        {
            Guid guid = Guid.NewGuid();
            Guid typeId = Guid.NewGuid();
	        Request request = new Request() 
            {
                Id = guid,
                RequestorsName = "Just Testing",
                RequestorsEmail = "first@test.com",
                RequestorsPhone = "489498948894",
                TypeId = typeId,
                State = "State",
                Description = "description",
                AdditionalFieldValues = new List<AdditionalFieldValue>()
	        };
            
            TypeEntity type = new TypeEntity();
            type.Id = typeId;

            var mock = new Mock<IRequestRepository<Request, TypeEntity>>(MockBehavior.Strict);
            mock.Setup(m => m.GetTypeWithFields(request.TypeId)).Returns(type);
            mock.Setup(m => m.Add(It.IsAny<Request>()));
            mock.Setup(m => m.Save());

            var controller = new RequestLogic(mock.Object);
            Request result = controller.Create(request);

            mock.VerifyAll();
            Assert.AreEqual(result, request);
        }

        
        [TestMethod]
        public void CreateInvalidId() 
        {
            Guid guid = Guid.NewGuid();
            Guid typeId = Guid.NewGuid();
            Request request = new Request();
            request.Id = guid;
            request.TypeId = typeId;
            request.RequestorsName = "test name";
            request.RequestorsEmail = "test@test.com";
            request.RequestorsPhone = "087898778";
            request.AdditionalFieldValues = new List<AdditionalFieldValue>();

            TypeEntity type = new TypeEntity();
            type.Id = typeId;

            var mock = new Mock<IRequestRepository<Request, TypeEntity>>(MockBehavior.Strict);
            mock.Setup(m => m.GetTypeWithFields(request.TypeId)).Returns(type);
            mock.Setup(m => m.Add(request)).Throws(new ExceptionController());

            var controller = new RequestLogic(mock.Object);
            Assert.ThrowsException<ExceptionController>(() => controller.Create(request));
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
            Request dummyRequest = new Request();
            dummyRequest.Id = guid;
            
            var mock = new Mock<IRequestRepository<Request, TypeEntity>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyRequest)).Returns(true);
            mock.Setup(m => m.Get(guid)).Returns(request);
            var controller = new RequestLogic(mock.Object);
            
            Request result = controller.Get(guid);
            Assert.AreEqual(request, result);
        }

        [TestMethod]
        public void GetIsNotOk() 
        {
            Guid guid = Guid.NewGuid();
            Request dummyRequest = new Request();
            dummyRequest.Id = guid;

            var mock = new Mock<IRequestRepository<Request, TypeEntity>>(MockBehavior.Strict);
            mock.Setup(m => m.Exist(dummyRequest)).Returns(true);
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

            var mock = new Mock<IRequestRepository<Request, TypeEntity>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Returns(requests);
            var controller = new RequestLogic(mock.Object);
            
            IEnumerable<Request> resultList = controller.GetAll();
            Assert.AreEqual(requests, resultList);
        }
        
        [TestMethod]
        public void GetAllNoElements() 
        {
            var mock = new Mock<IRequestRepository<Request, TypeEntity>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Throws(new ArgumentException());
            var controller = new RequestLogic(mock.Object);

            Assert.ThrowsException<ArgumentException>(() => controller.GetAll());
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateCorrect() 
        {
	        Request entity = CreateEntity();
            var mock = new Mock<IRequestRepository<Request, TypeEntity>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(GetId(entity))).Returns(entity);
            mock.Setup(m => m.Update(ModifyEntity(entity)));
            mock.Setup(m => m.Save());

            var controller = new RequestLogic(mock.Object);

            controller.Update(entity);
            mock.VerifyAll();
        }

        [TestMethod]
        public void UpdateInvalid() 
        {
            Request entity = CreateEntity();
            Guid entityGuid = GetId(entity);
            var mock = new Mock<IRequestRepository<Request, TypeEntity>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(entityGuid)).Throws(new ExceptionController());
            
            var controller = new RequestLogic(mock.Object);

            Assert.ThrowsException<ExceptionController>(() => controller.Update(entity));
            mock.VerifyAll();
        } 
    }
}
