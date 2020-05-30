using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.Exceptions;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess.Test
{
    [TestClass]
    public class RequestRepositoryTest 
    {
        public Request CreateEntity()
        {
            Request Request = new Request();
            return Request;
        }

        public Request ModifyEntity(Request Request, string prop)
        {
            Request ModifiedRequest = Request;
            ModifiedRequest.Description = prop;
            return ModifiedRequest;
        }

        public string GetEntityProp()
        {
            return "New Description to test";
        }

        public Boolean CompareProps(Request Request, string prop)
        {
            return Request.Description == prop;
        }
        

        public Request GetSavedEntity(RequestRepository RequestRepo, Request Request)
        {
            Request RequesToReturn = RequestRepo.Get(Request.Id);
            return RequesToReturn;
        }


        [TestMethod]
        public void TestRequestGetAllOK()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            RequestRepository requestRepo = new RequestRepository(IMMRequestContext);

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
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            RequestRepository requestRepo = new RequestRepository(IMMRequestContext);

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
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            RequestRepository requestRepo = new RequestRepository(IMMRequestContext);

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
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            RequestRepository requestRepo = new RequestRepository(IMMRequestContext);

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
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            RequestRepository requestRepo = new RequestRepository(IMMRequestContext);

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

        [TestMethod]
        [ExpectedException(typeof(ExceptionController), "El request no existe")]
        public void GetInvalid()
        {
            var id = Guid.NewGuid();
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            RequestRepository requestRepo = new RequestRepository(IMMRequestContext);

            requestRepo.Get(id);
        }

        [TestMethod]
        public void AddOk()
        {
            Request Entity = CreateEntity();
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            RequestRepository requestRepo = new RequestRepository(IMMRequestContext);

            requestRepo.Add(Entity);
            requestRepo.Save();

            Assert.AreEqual(1, requestRepo.GetAll().ToList().Count());
        }

        [TestMethod]
        public void AddOk2()
        {
            Request FirstEntity = CreateEntity();
            Request SecondEntity = CreateEntity();

            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            RequestRepository requestRepo = new RequestRepository(IMMRequestContext);

            requestRepo.Add(FirstEntity);
            requestRepo.Add(SecondEntity);
            requestRepo.Save();

            Assert.AreEqual(2, requestRepo.GetAll().ToList().Count());
        }

        [TestMethod]
        public void UpdateOk()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            RequestRepository requestRepo = new RequestRepository(IMMRequestContext);

            Request InitEntity = CreateEntity();

            requestRepo.Add(InitEntity);
            requestRepo.Save();

            var EntityProp = GetEntityProp();
            InitEntity = ModifyEntity(InitEntity, EntityProp);

            requestRepo.Update(InitEntity);
            requestRepo.Save();

            Assert.AreEqual(CompareProps(GetSavedEntity(requestRepo, InitEntity), EntityProp), true);
        }

        [TestMethod]
        public void UpdateNotOk()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            RequestRepository requestRepo = new RequestRepository(IMMRequestContext);

            Request InitEntity = CreateEntity();

            requestRepo.Add(InitEntity);
            requestRepo.Save();

            var EntityProp = GetEntityProp();

            requestRepo.Update(InitEntity);
            requestRepo.Save();

            Assert.AreNotEqual(CompareProps(GetSavedEntity(requestRepo, InitEntity), EntityProp), true);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionController), "La entidad no existe")]
        public void UpdateInvalid()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            RequestRepository requestRepo = new RequestRepository(IMMRequestContext);

            Request InitEntity = CreateEntity();

            requestRepo.Update(InitEntity);
            requestRepo.Save();
        }

        [TestMethod]
        public void SaveOk()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            RequestRepository requestRepo = new RequestRepository(IMMRequestContext);

            Request InitEntity = CreateEntity();

            requestRepo.Add(InitEntity);
            requestRepo.Save();

            Request RetrievedEntity = GetSavedEntity(requestRepo, InitEntity);

            Assert.AreEqual(InitEntity, RetrievedEntity);
        }

        [TestMethod]
        public void SaveOk2()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            RequestRepository requestRepo = new RequestRepository(IMMRequestContext);

            Request InitEntity = CreateEntity();
            Request InitEntity2 = CreateEntity();

            requestRepo.Add(InitEntity);
            requestRepo.Add(InitEntity2);
            requestRepo.Save();

            Request RetrievedEntity = GetSavedEntity(requestRepo, InitEntity2);

            Assert.AreEqual(InitEntity2, RetrievedEntity);
        }
    }
}
