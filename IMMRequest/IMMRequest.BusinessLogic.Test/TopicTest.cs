using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;
using IMMRequest.DataAccess.Interface;

namespace IMMRequest.BusinessLogic.Test
{
    [TestClass]
    public class TopicTest : BaseLogicTest<Topic>
    {
        public TopicTest() {}

        public override BaseLogic<Topic> CreateBaseLogic(IRepository<Topic> obj)
        {
            throw new NotImplementedException();
        }

        public override Topic CreateEntity()
        {
            throw new NotImplementedException();
        }

        public override Guid GetId(Topic entity)
        {
            throw new NotImplementedException();
        }

        public override Topic GetSavedEntity(BaseLogic<Topic> BaseLogic, Topic Entity)
        {
            throw new NotImplementedException();
        }

        public override Topic ModifyEntity(Topic Entity)
        {
            throw new NotImplementedException();
        }
    }
}