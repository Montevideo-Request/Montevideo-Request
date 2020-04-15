using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic.Test
{/*
    [TestClass]
    public class RequestTest 
    {
        public RequestLogic requestLogic;

        public RequestTest() {}

        [TestInitialize()]
        public void Initialize()
        {
            this.requestLogic = new RequestLogic();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.requestLogic = new RequestLogic();
        }

        [TestMethod]
        public void CreateIsOk() 
        {
            Guid guid = Guid.NewGuid();
	        Request request = new Request() 
            {
                Id = guid,
                RequestorsName = "Requestors Name",
                RequestorsEmail = "Requestors Email",
                RequestorsPhone = "RequestorsPhone",
                Area = new Area(),
                Topic = new Topic(),
                Type = new Type(),
                State = "State",
                Description = "Description"
	        };
            Request result = this.requestLogic.Create(request);
            Assert.AreEqual(request, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateIdExists() 
        {
            Guid guid = Guid.NewGuid();
            Guid anotherGuid = Guid.NewGuid();

	        Request requestExpected = new Request() 
            {
                Id = guid,
                RequestorsName = "Just Testing",
                RequestorsEmail = "Requestors Email",
                RequestorsPhone = "RequestorsPhone",
                Area = new Area(),
                Topic = new Topic(),
                Type = new Type(),
                State = "State",
                Description = "Description"
	        };
            this.requestLogic.Add(requestExpected);
            this.requestLogic.Save();

            this.requestLogic.Add(requestExpected);
            this.requestLogic.Save();
            
            Assert.AreEqual(requestExpected, requestExpected);
        }

        [TestMethod]
        public void RemoveCorrectId() 
        {
            Guid firstGuid = Guid.NewGuid();
            Request firstRequestExpected = new Request() 
            {
                Id = guid,
                RequestorsName = "Just Testing",
                RequestorsEmail = "Requestors Email",
                RequestorsPhone = "RequestorsPhone",
                Area = new Area(),
                Topic = new Topic(),
                Type = new Type(),
                State = "State",
                Description = "Description"
	        };
            this.requestLogic.Add(firstRequestExpected);
            
	        Request secondRequestExpected = new Request() 
            {
                Id = guid,
                RequestorsName = "Just Second Testing",
                RequestorsEmail = "Requestors Second Email",
                RequestorsPhone = "Requestors Second Phone",
                Area = new Area(),
                Topic = new Topic(),
                Type = new Type(),
                State = "State",
                Description = "Description"
	        };
            this.requestLogic.Add(secondRequestExpected);
            this.requestLogic.Save();

            this.requestLogic.Remove(firstGuid);

            IEnumerable<Request> resultList = this.requestLogic.GetRequests();
            
            Assert.AreEqual(1, resultList.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveInvalidId() 
        {
            Guid randomGuid = Guid.NewGuid();
	        Request request = new Request() 
            {
                Id = guid,
                RequestorsName = "Just Testing",
                RequestorsEmail = "Requestors Email",
                RequestorsPhone = "RequestorsPhone",
                Area = new Area(),
                Topic = new Topic(),
                Type = new Type(),
                State = "State",
                Description = "Description"
	        };
            this.requestLogic.Add(request);
            this.requestLogic.Save();

            this.requestLogic.Remove(randomGuid);
            IEnumerable<Request> resultList = this.requestLogic.GetRequests();
            Assert.AreEqual(1, resultList.Count());
        }

        [TestMethod]
        public void GetIsOk() 
        {
            Guid guid = Guid.NewGuid();

	        Request requestExpected = new Request() 
            {
                Id = guid,
                RequestorsName = "Just Testing",
                RequestorsEmail = "Requestors Email",
                RequestorsPhone = "RequestorsPhone",
                Area = new Area(),
                Topic = new Topic(),
                Type = new Type(),
                State = "State",
                Description = "Description"
	        };
            this.requestLogic.Add(requestExpected);
            this.requestLogic.Save();

            Request result = this.requestLogic.Get(guid);
            
            Assert.AreEqual(requestExpected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetIsNotOk() 
        {
            Guid guid = Guid.NewGuid();
            Guid anotherGuid = Guid.NewGuid();

	        Request requestExpected = new Request() 
            {
                Id = guid,
                RequestorsName = "Just Testing",
                RequestorsEmail = "Requestors Email",
                RequestorsPhone = "RequestorsPhone",
                Area = new Area(),
                Topic = new Topic(),
                Type = new Type(),
                State = "State",
                Description = "Description"
	        };
            this.requestLogic.Add(requestExpected);
            this.requestLogic.Save();

            Area result = this.requestLogic.Get(anotherGuid);
            
            Assert.AreEqual(requestExpected, result);
        }

        [TestMethod]
        public void GetRequestsIsOk() 
        {
	        Request firstRequestExpected = new Request() 
            {
                Id = Guid.NewGuid(),
                RequestorsName = "Just Testing",
                RequestorsEmail = "Requestors Email",
                RequestorsPhone = "RequestorsPhone",
                Area = new Area(),
                Topic = new Topic(),
                Type = new Type(),
                State = "State",
                Description = "Description"
	        };
            this.requestLogic.Add(firstRequestExpected);
            
	        Request secondRequestExpected = new Request() 
            {
                Id = Guid.NewGuid(),
                RequestorsName = "Just Testing",
                RequestorsEmail = "Requestors Email",
                RequestorsPhone = "RequestorsPhone",
                Area = new Area(),
                Topic = new Topic(),
                Type = new Type(),
                State = "State",
                Description = "Description"
	        };
            this.requestLogic.Add(secondRequestExpected);
            this.requestLogic.Save();

            IEnumerable<Request> resultList = this.requestLogic.GetRequests();
            
            Assert.AreEqual(2, resultList.Count());
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetRequestsNoElements() 
        {
            IEnumerable<Request> resultList = this.requestLogic.GetRequests();
            Assert.AreEqual(0, resultList.Count());
        }
    }*/
}