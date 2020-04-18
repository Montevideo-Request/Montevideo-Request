using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;
using IMMRequest.DataAccess.Interface;

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
    }
}