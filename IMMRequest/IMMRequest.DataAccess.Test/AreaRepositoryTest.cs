using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess.Test
{
    [TestClass]
    public class AreaRepositoryTest : BaseRepositoryTest<Area>

    {
        public override Area CreateEntity()
        {
            Area Area = new Area();
            return Area;
        }

        public override Area ModifiedEntity(Area Area)
        {
            Area ModifiedArea = Area;
            Area.Name = "Test";
            return ModifiedArea;
        }

        public override Area GetSavedEntity(BaseRepository<Area> areaRepo, Area Area)
        {
            Area AreaToReturn = areaRepo.Get(Area.Id);
            return AreaToReturn;
        }

        public override BaseRepository<Area> CreateRepository()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AreaRepository areaRepo = new AreaRepository(IMMRequestContext);

            return areaRepo;
        }

        
        [TestMethod]
        public void TestAreaGetAllOK()
        {
            var areaRepo = CreateRepository();

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
    }
}
