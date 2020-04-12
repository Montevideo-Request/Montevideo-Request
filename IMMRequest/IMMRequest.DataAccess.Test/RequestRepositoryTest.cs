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
            Request Type = new Request();
            return Type;
        }

        public override Request ModifiedEntity(Request Request)
        {
            Request ModifiedRequest = Request;
            ModifiedRequest.Description = "Test";
            return ModifiedRequest;
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

    }
}
