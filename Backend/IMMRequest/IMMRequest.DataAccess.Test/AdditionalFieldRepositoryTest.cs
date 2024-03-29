using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess.Test
{
    [TestClass]
    public class AdditionalFieldRepositoryTest : BaseRepositoryTest<AdditionalField, TypeEntity>
    {
        public override AdditionalField CreateEntity()
        {
            AdditionalField Field = new AdditionalField();
            return Field;
        }

        public override AdditionalField ModifyEntity(AdditionalField AdditionalField, string prop)
        {
            AdditionalField ModifiedAdditionalField = AdditionalField;
            ModifiedAdditionalField.Name = prop;
            return ModifiedAdditionalField;
        }

        public override string GetEntityProp()
        {
            return "New Property to test";
        }

        public override Boolean CompareProps(AdditionalField AdditionalField, string prop)
        {
            return AdditionalField.Name == prop;
        }

        public override AdditionalField GetSavedEntity(BaseRepository<AdditionalField, TypeEntity> AdditionalFieldRepo, AdditionalField AdditionalField)
        {
            AdditionalField AdditionalFieldToReturn = AdditionalFieldRepo.Get(AdditionalField.Id);
            return AdditionalFieldToReturn;
        }

        public override BaseRepository<AdditionalField, TypeEntity> CreateRepository()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AdditionalFieldRepository AdditionalFieldRepo = new AdditionalFieldRepository(IMMRequestContext);

            return AdditionalFieldRepo;
        }

        [TestMethod]
        public void TestAdditionalFieldGetAllOK()
        {
            var adFieldRepo = CreateRepository();

            adFieldRepo.Add(new AdditionalField
            {
                Id = Guid.NewGuid(),
                Name = "Fecha Comienzo"
            });

            adFieldRepo.Save();

            var adFields = adFieldRepo.GetAll().ToList().Count();
            Assert.AreEqual(1, adFields);
        }

        [TestMethod]
        public void TestAdditionalFieldGetAllOK2()
        {
            var adFieldRepo = CreateRepository();

            adFieldRepo.Add(new AdditionalField
            {
                Id = Guid.NewGuid(),
                Name = "Fecha Comienzo"
            });

            adFieldRepo.Add(new AdditionalField
            {
                Id = Guid.NewGuid(),
                Name = "Fecha Fin"
            });

            adFieldRepo.Save();

            var adFields = adFieldRepo.GetAll().ToList().Count();
            Assert.AreEqual(2, adFields);
        }


        [TestMethod]
        public void TestAdditionalFieldGetAllOK3()
        {
            var adFieldRepo = CreateRepository();

            AdditionalField adField = new AdditionalField()
            {
                Id = Guid.NewGuid(),
                Name = "Fecha Comienzo"
            };

            adFieldRepo.Add(adField);
            adFieldRepo.Save();

            var adFields = adFieldRepo.GetAll().ToList();

            Assert.AreEqual(adFields.First(), adField);
        }

        [TestMethod]
        public void TestAdditionalFieldGet()
        {
            var id = Guid.NewGuid();
            var adFieldRepo = CreateRepository();

            AdditionalField adField = new AdditionalField()
            {
                Id = id,
                Name = "Fecha Comienzo"
            };

            adFieldRepo.Add(adField);
            adFieldRepo.Save();

            Assert.AreEqual(adFieldRepo.Get(id), adField);
        }

        [TestMethod]
        public void TestAdditionalFieldGet2()
        {
            var id = Guid.NewGuid();
            var adFieldRepo = CreateRepository();

            AdditionalField adField1 = new AdditionalField()
            {
                Id = id,
                Name = "Fecha Comienzo"
            };

            AdditionalField adField2 = new AdditionalField()
            {
                Id = Guid.NewGuid(),
                Name = "Fecha Fin"
            };

            adFieldRepo.Add(adField1);
            adFieldRepo.Add(adField2);
            adFieldRepo.Save();

            Assert.AreEqual(adFieldRepo.Get(id), adField1);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionController), "El campo adicional no existe")]
        public void GetInvalid()
        {
            var id = Guid.NewGuid();
            var adFieldRepo = CreateRepository();

            adFieldRepo.Get(id);
        }


        [TestMethod]
        public void TestAdditionalFieldGetParent()
        {
            var adFieldRepo = CreateRepository();

            TypeEntity type = new TypeEntity()
            {
                Name = "Parent Type"
            };

            AdditionalField field = new AdditionalField()
            {
                Name = "Alumbrado",
                Type = type
            };

            adFieldRepo.Add(field);
            adFieldRepo.Save();

            TypeEntity parentType = adFieldRepo.GetParent(type.Id);

            Assert.AreEqual(parentType.Name, type.Name);
        }
    }
}
