using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;
using IMMRequest.DataAccess.Interface;

namespace IMMRequest.BusinessLogic.Test 
{
    [TestClass]
    public class AreaTest : BaseLogicTest<Area>
    {
        public AreaTest() {}

        public override BaseLogic<Area> CreateBaseLogic(IRepository<Area> obj)
        {
            throw new NotImplementedException();
        }

        public override Area CreateEntity()
        {
            throw new NotImplementedException();
        }

        public override Guid GetId(Area entity)
        {
            throw new NotImplementedException();
        }

        public override Area GetSavedEntity(BaseLogic<Area> BaseLogic, Area Entity)
        {
            throw new NotImplementedException();
        }

        public override Area ModifyEntity(Area Entity)
        {
            throw new NotImplementedException();
        }
    }
}