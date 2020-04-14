using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess.Test
{
    [TestClass]
    public class RequestRepositoryTest : BaseRepositoryTest<Request>
    {
        public override Request CreateEntity()
        {
            Request Request = new Request();
            return Request;
        }

        public override Request ModifyEntity(Request Request, string prop)
        {
            Request ModifiedRequest = Request;
            ModifiedRequest.Description = prop;
            return ModifiedRequest;
        }

        public override string GetEntityProp()
        {
            return "New Property to test";
        }

        public override Boolean CompareProps(Request Request, string prop)
        {
            return Request.Description == prop;
        }
        

        public override Request GetSavedEntity(BaseRepository<Request> RequestRepo, Request Request)
        {
            Request RequesToReturn = RequestRepo.Get(Request.Id);
            return RequesToReturn;
        }

        public override BaseRepository<Request> CreateRepository()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            RequestRepository RequestRepo = new RequestRepository(IMMRequestContext);

            return RequestRepo;
        }


        [TestMethod]
        public void TestRequestGetAllOK()
        {
            var requestRepo = CreateRepository();

            requestRepo.Add(new Request
            {
                Id = Guid.NewGuid(),
                Description = "Testing"
            });

            requestRepo.Save();

            var requests = requestRepo.GetAll().ToList().Count();
            Assert.AreEqual(1, requests);
        }

        [TestMethod]
        public void TestRequestGetAllOK2()
        {
            var requestRepo = CreateRepository();

            requestRepo.Add(new Request
            {
                Id = Guid.NewGuid(),
                Description = "Testing"
            });

            requestRepo.Add(new Request
            {
                Id = Guid.NewGuid(),
                Description = "Testing 2"
            });

            requestRepo.Save();

            var requests = requestRepo.GetAll().ToList().Count();
            Assert.AreEqual(2, requests);
        }


        [TestMethod]
        public void TestRequestGetAllOK3()
        {
            var requestRepo = CreateRepository();

            Request request = new Request()
            {
                Id = Guid.NewGuid(),
                Description = "Testing"
            };

            requestRepo.Add(request);
            requestRepo.Save();

            var requests = requestRepo.GetAll().ToList();

            Assert.AreEqual(requests.First(), request);
        }

        [TestMethod]
        public void TestRequestGet()
        {
            var id = Guid.NewGuid();
            var requestRepo = CreateRepository();

            Request request = new Request()
            {
                Id = id,
                Description = "Testing"
            };

            requestRepo.Add(request);
            requestRepo.Save();

            Assert.AreEqual(requestRepo.Get(id), request);
        }

        [TestMethod]
        public void TestRequestGet2()
        {
            var id = Guid.NewGuid();
            var requestRepo = CreateRepository();

            Request request1 = new Request()
            {
                Id = id,
                Description = "Testing"
            };

            Request request2 = new Request()
            {
                Id = Guid.NewGuid(),
                Description = "Testing 2"
            };

            requestRepo.Add(request1);
            requestRepo.Add(request2);
            requestRepo.Save();

            Assert.AreEqual(requestRepo.Get(id), request1);
        }
    }
}
