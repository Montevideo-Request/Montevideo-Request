using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess.Test
{
    [TestClass]
    public class TypeRepositoryTest : BaseRepositoryTest<TypeEntity>
    {
        public override TypeEntity CreateEntity()
        {
            TypeEntity Type = new TypeEntity();
            return Type;
        }

        public override TypeEntity ModifiedEntity(TypeEntity Type)
        {
            TypeEntity ModifiedType = Type;
            ModifiedType.Name = "Test";
            return ModifiedType;
        }

        public override TypeEntity GetSavedEntity(BaseRepository<TypeEntity> TypeRepo, TypeEntity Type)
        {
            TypeEntity TypeToReturn = TypeRepo.Get(Type.Id);
            return TypeToReturn;
        }

        public override BaseRepository<TypeEntity> CreateRepository()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            TypeRepository TypeRepo = new TypeRepository(IMMRequestContext);

            return TypeRepo;
        }

    }
}
