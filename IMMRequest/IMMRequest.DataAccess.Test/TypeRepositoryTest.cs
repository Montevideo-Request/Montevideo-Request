using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess.Test
{
    [TestClass]
    public class TypeRepositoryTest : BaseRepositoryTest<TypeEntity, Topic>
    {
        public override TypeEntity CreateEntity()
        {
            TypeEntity Type = new TypeEntity();
            return Type;
        }

        public override TypeEntity ModifyEntity(TypeEntity TypeEntity, string prop)
        {
            TypeEntity ModifiedTypeEntity = TypeEntity;
            ModifiedTypeEntity.Name = prop;
            return ModifiedTypeEntity;
        }

        public override string GetEntityProp()
        {
            return "New Property to test";
        }

        public override Boolean CompareProps(TypeEntity TypeEntity, string prop)
        {
            return TypeEntity.Name == prop;
        }

        public override TypeEntity GetSavedEntity(BaseRepository<TypeEntity, Topic> TypeRepo, TypeEntity Type)
        {
            TypeEntity TypeToReturn = TypeRepo.Get(Type.Id);
            return TypeToReturn;
        }

        public override BaseRepository<TypeEntity, Topic> CreateRepository()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            TypeRepository TypeRepo = new TypeRepository(IMMRequestContext);

            return TypeRepo;
        }

        [TestMethod]
        public void TestTypeGetAllOK()
        {
            var typeRepo = CreateRepository();

            typeRepo.Add(new TypeEntity
            {
                Id = Guid.NewGuid(),
                Name = "Contenedor roto"
            });

            typeRepo.Save();

            var types = typeRepo.GetAll().ToList().Count();
            Assert.AreEqual(1, types);
        }

         [TestMethod]
        public void TestTypeGetAllOK2()
        {
            var typeRepo = CreateRepository();

            typeRepo.Add(new TypeEntity
            {
                Id = Guid.NewGuid(),
                Name = "Contenedor roto"
            });

            typeRepo.Add(new TypeEntity
            {
                Id = Guid.NewGuid(),
                Name = "“No tiene goma”",
            });

            typeRepo.Save();

            var types = typeRepo.GetAll().ToList().Count();
            Assert.AreEqual(2, types);
        }

        
        [TestMethod]
        public void TestTypeGetAll3()
        {
            var typeRepo = CreateRepository();

            TypeEntity type = new TypeEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Contenedor roto"
            };

            typeRepo.Add(type);
            typeRepo.Save();

            var types = typeRepo.GetAll().ToList();

            Assert.AreEqual(types.First(), type);
        }


        [TestMethod]
        public void TestTypeGet()
        {
            var id = Guid.NewGuid();
            var typeRepo = CreateRepository();

            TypeEntity type = new TypeEntity()
            {
                Id = id,
                Name = "Contenedor roto"
            };

            typeRepo.Add(type);
            typeRepo.Save();

            Assert.AreEqual(typeRepo.Get(id), type);
        }

        [TestMethod]
        public void TestTypeGet2()
        {
            var typeRepo = CreateRepository();
            var id = Guid.NewGuid();   
            
            TypeEntity type1 = new TypeEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Contenedor roto"
            };

            TypeEntity type2 = new TypeEntity()
            {
                Id = id,
                Name = "“No tiene goma”",
            };

            
            typeRepo.Add(type1);
            typeRepo.Add(type2);
            typeRepo.Save();

            Assert.AreEqual(typeRepo.Get(id), type2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "El tipo no existe")]
        public void GetInvalid()
        {
            var id = Guid.NewGuid();
            var typeRepo = CreateRepository();

            typeRepo.Get(id);
        }

        [TestMethod]
        public void TestAdditionalFieldGetParent()
        {
            var typeRepo = CreateRepository();

            Topic topic = new Topic()
            {
                Name = "Parent Topic"
            };

            TypeEntity type = new TypeEntity()
            {
                Name = "Type", 
                Topic = topic
            };

            typeRepo.Add(type);
            typeRepo.Save();

            Topic parentTopic = typeRepo.GetParent(topic.Id);

            Assert.AreEqual(parentTopic.Name, topic.Name);
        }

    }
}
