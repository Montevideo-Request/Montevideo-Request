using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess.Test
{
    [TestClass]
    public class AreaRepositoryTest

    {
        public Area CreateEntity()
        {
            Area Area = new Area();
            return Area;
        }

        public Area ModifyEntity(Area Area, string prop)
        {
            Area ModifiedArea = Area;
            ModifiedArea.Name = prop;
            return ModifiedArea;
        }

        public string GetEntityProp()
        {
            return "New Property to test";
        }

        public Boolean CompareProps(Area Area, string prop)
        {
            return Area.Name == prop;
        }


        [TestMethod]
        public void AddOk()
        {
            Area Entity = CreateEntity();

            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AreaRepository baseRepo = new AreaRepository(IMMRequestContext);

            baseRepo.Add(Entity);
            baseRepo.Save();

            Assert.AreEqual(1, baseRepo.GetAll().ToList().Count());
        }

        [TestMethod]
        public void AddOk2()
        {
            Area FirstEntity = CreateEntity();
            Area SecondEntity = CreateEntity();
            
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AreaRepository baseRepo = new AreaRepository(IMMRequestContext);

            baseRepo.Add(FirstEntity);
            baseRepo.Add(SecondEntity);
            baseRepo.Save();

            Assert.AreEqual(2, baseRepo.GetAll().ToList().Count());
        }

        [TestMethod]
        public void RemoveOk()
        {
            Area FirstEntity = CreateEntity();
            Area SecondEntity = CreateEntity();

            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AreaRepository baseRepo = new AreaRepository(IMMRequestContext);

            baseRepo.Add(FirstEntity);
            baseRepo.Add(SecondEntity);
            baseRepo.Save();

            baseRepo.Remove(SecondEntity);
            baseRepo.Save();

            Assert.AreEqual(1, baseRepo.GetAll().ToList().Count());
        }

        [TestMethod]
        public void RemoveOk2()
        {
            Area FirstEntity = CreateEntity();
            Area SecondEntity = CreateEntity();
            
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AreaRepository baseRepo = new AreaRepository(IMMRequestContext);

            baseRepo.Add(FirstEntity);
            baseRepo.Add(SecondEntity);
            baseRepo.Save();

            baseRepo.Remove(FirstEntity);
            baseRepo.Remove(SecondEntity);
            baseRepo.Save();

            Assert.AreEqual(0, baseRepo.GetAll().ToList().Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionController), "La entidad que desea eliminar no existe")]
        public void RemoveInvalid()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AreaRepository baseRepo = new AreaRepository(IMMRequestContext);

            Area InitEntity = CreateEntity();

            baseRepo.Remove(InitEntity);
            baseRepo.Save();
        }

        [TestMethod]
        public void UpdateOk()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AreaRepository baseRepo = new AreaRepository(IMMRequestContext);

            Area InitEntity = CreateEntity();

            baseRepo.Add(InitEntity);
            baseRepo.Save();

            var EntityProp = GetEntityProp();
            InitEntity = ModifyEntity(InitEntity, EntityProp);

            baseRepo.Update(InitEntity);
            baseRepo.Save();

            Area AreaToReturn = baseRepo.Get(InitEntity.Id);

            Assert.AreEqual(CompareProps(AreaToReturn, EntityProp), true);
        }

        [TestMethod]
        public void UpdateNotOk()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AreaRepository baseRepo = new AreaRepository(IMMRequestContext);

            Area InitEntity = CreateEntity();

            baseRepo.Add(InitEntity);
            baseRepo.Save();

            var EntityProp = GetEntityProp();

            baseRepo.Update(InitEntity);
            baseRepo.Save();

            Area AreaToReturn = baseRepo.Get(InitEntity.Id);

            Assert.AreNotEqual(CompareProps(AreaToReturn, EntityProp), true);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionController), "La entidad no existe")]
        public void UpdateInvalid()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AreaRepository baseRepo = new AreaRepository(IMMRequestContext);

            Area InitEntity = CreateEntity();

            baseRepo.Update(InitEntity);
            baseRepo.Save();
        }

        [TestMethod]
        public void SaveOk()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AreaRepository baseRepo = new AreaRepository(IMMRequestContext);

            Area InitEntity = CreateEntity();

            baseRepo.Add(InitEntity);
            baseRepo.Save();

            Area AreaToReturn = baseRepo.Get(InitEntity.Id);

            Assert.AreEqual(InitEntity, AreaToReturn);
        }

        [TestMethod]
        public void SaveOk2()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AreaRepository baseRepo = new AreaRepository(IMMRequestContext);

            Area InitEntity = CreateEntity();
            Area InitEntity2 = CreateEntity();

            baseRepo.Add(InitEntity);
            baseRepo.Add(InitEntity2);
            baseRepo.Save();

            Area AreaToReturn = baseRepo.Get(InitEntity2.Id);

            Assert.AreEqual(InitEntity2, AreaToReturn);
        }

        
        [TestMethod]
        public void TestAreaGetAllOK()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AreaRepository areaRepo = new AreaRepository(IMMRequestContext);

            areaRepo.Add(new Area
            {
                Id = Guid.NewGuid(),
                Name = "Limpieza",
                Topics = new List<Topic>()
            });

            areaRepo.Save();

            var areas = areaRepo.GetAll().ToList().Count();
            Assert.AreEqual(1, areas);
        }

        [TestMethod]
        public void TestAreaGetAllOK2()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AreaRepository areaRepo = new AreaRepository(IMMRequestContext);

            areaRepo.Add(new Area
            {
                Id = Guid.NewGuid(),
                Name = "Limpieza",
                Topics = new List<Topic>()
            });

            areaRepo.Add(new Area
            {
                Id = Guid.NewGuid(),
                Name = "Transporte",
                Topics = new List<Topic>()
            });

            areaRepo.Save();

            var areas = areaRepo.GetAll().ToList().Count();
            Assert.AreEqual(2, areas);
        }

        [TestMethod]
        public void TestAreaGetAll3()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AreaRepository areaRepo = new AreaRepository(IMMRequestContext);
            Area area = new Area()
            {
                Id = Guid.NewGuid(),
                Name = "Limpieza",
                Topics = new List<Topic>()
            };

            areaRepo.Add(area);
            areaRepo.Save();

            var areas = areaRepo.GetAll().ToList();

            Assert.AreEqual(areas.First(), area);
        }

        [TestMethod]
        public void TestAreaGet()
        {
            var id = Guid.NewGuid();

            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AreaRepository areaRepo = new AreaRepository(IMMRequestContext);
            Area area = new Area()
            {
                Id = id,
                Name = "Limpieza",
                Topics = new List<Topic>()
            };

            areaRepo.Add(area);
            areaRepo.Save();

            Assert.AreEqual(areaRepo.Get(id), area);
        }

        [TestMethod]
        public void TestAreaGet2()
        {
            var id = Guid.NewGuid();

            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AreaRepository areaRepo = new AreaRepository(IMMRequestContext);

            Area area1 = new Area()
            {
                Id = id,
                Name = "Limpieza",
                Topics = new List<Topic>()
            };

            Area area2 = new Area()
            {
                Id = Guid.NewGuid(),
                Name = "Transporte",
                Topics = new List<Topic>()
            };

            areaRepo.Add(area1);
            areaRepo.Add(area2);
            areaRepo.Save();

            Assert.AreEqual(areaRepo.Get(id), area1);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionController), "El Area no existe")]
        public void GetInvalid()
        {
            var id = Guid.NewGuid();

            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AreaRepository areaRepo = new AreaRepository(IMMRequestContext);

            areaRepo.Get(id);
        }
    }
}
