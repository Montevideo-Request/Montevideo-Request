using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;
using IMMRequest.DataAccess.Interface;

namespace IMMRequest.BusinessLogic.Test 
{
    [TestClass]
    public class AdditionalFieldTest : BaseLogicTest<AdditionalField>
    {
        public AdditionalFieldTest() {}

        public override BaseLogic<AdditionalField> CreateBaseLogic(IRepository<AdditionalField> obj)
        {
            throw new NotImplementedException();
        }

        public override AdditionalField CreateEntity()
        {
            throw new NotImplementedException();
        }

        public override Guid GetId(AdditionalField entity)
        {
            throw new NotImplementedException();
        }

        public override AdditionalField GetSavedEntity(BaseLogic<AdditionalField> BaseLogic, AdditionalField Entity)
        {
            throw new NotImplementedException();
        }

        public override AdditionalField ModifyEntity(AdditionalField Entity)
        {
            throw new NotImplementedException();
        }
    }
}