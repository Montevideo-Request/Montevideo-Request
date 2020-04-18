using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Domain;
using System.Linq;
using System;
using IMMRequest.DataAccess.Interface;

namespace IMMRequest.BusinessLogic.Test
{
    [TestClass]
    public class TypeTest : BaseLogicTest<Type>
    {
        public TypeTest() {}

        public override BaseLogic<Type> CreateBaseLogic(IRepository<Type> obj)
        {
            throw new NotImplementedException();
        }

        public override Type CreateEntity()
        {
            throw new NotImplementedException();
        }

        public override Guid GetId(Type entity)
        {
            throw new NotImplementedException();
        }

        public override Type GetSavedEntity(BaseLogic<Type> BaseLogic, Type Entity)
        {
            throw new NotImplementedException();
        }

        public override Type ModifyEntity(Type Entity)
        {
            throw new NotImplementedException();
        }
    }
}