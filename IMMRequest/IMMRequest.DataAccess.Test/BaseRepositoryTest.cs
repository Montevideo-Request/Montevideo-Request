using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.DataAccess;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess.Test
{
    [TestClass]
    public abstract class BaseRepositoryTest<T> where T : class
    {
        public abstract T CreateEntity();
        public abstract string GetEntityProp(); 
        public abstract BaseRepository<T> CreateRepository();
        public abstract T ModifyEntity(T Entity, string Prop);
        public abstract Boolean CompareProps(T Entity, string Prop);
        public abstract T GetSavedEntity(BaseRepository<T> BaseRepository, T Entity);

        [TestMethod]
        public void AddOk()
        {
            T Entity = CreateEntity();
            BaseRepository<T> baseRepo = CreateRepository();

            baseRepo.Add(Entity);
            baseRepo.Save();

            Assert.AreEqual(1, baseRepo.GetAll().ToList().Count());
        }

        [TestMethod]
        public void AddOk2()
        {
            T FirstEntity = CreateEntity();
            T SecondEntity = CreateEntity();
            BaseRepository<T> baseRepo = CreateRepository();

            baseRepo.Add(FirstEntity);
            baseRepo.Add(SecondEntity);
            baseRepo.Save();

            Assert.AreEqual(2, baseRepo.GetAll().ToList().Count());
        }

        [TestMethod]
        public void RemoveOk()
        {
            T FirstEntity = CreateEntity();
            T SecondEntity = CreateEntity();
            BaseRepository<T> baseRepo = CreateRepository();

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
            T FirstEntity = CreateEntity();
            T SecondEntity = CreateEntity();
            BaseRepository<T> baseRepo = CreateRepository();

            baseRepo.Add(FirstEntity);
            baseRepo.Add(SecondEntity);
            baseRepo.Save();

            baseRepo.Remove(FirstEntity);
            baseRepo.Remove(SecondEntity);
            baseRepo.Save();

            Assert.AreEqual(0, baseRepo.GetAll().ToList().Count());
        }

        [TestMethod]
        public void UpdateOk()
        {
            BaseRepository<T> baseRepo = CreateRepository();

            T InitEntity = CreateEntity();

            baseRepo.Add(InitEntity);
            baseRepo.Save();

            var EntityProp = GetEntityProp();
            InitEntity = ModifyEntity(InitEntity, EntityProp);

            baseRepo.Update(InitEntity);
            baseRepo.Save();

            Assert.AreEqual(CompareProps(GetSavedEntity(baseRepo, InitEntity), EntityProp), true);
        }

         [TestMethod]
        public void UpdateNotOk()
        {
            BaseRepository<T> baseRepo = CreateRepository();

            T InitEntity = CreateEntity();

            baseRepo.Add(InitEntity);
            baseRepo.Save();

            var EntityProp = GetEntityProp();

            baseRepo.Update(InitEntity);
            baseRepo.Save();

            Assert.AreNotEqual(CompareProps(GetSavedEntity(baseRepo, InitEntity), EntityProp), true);
        }

        [TestMethod]
        public void SaveOk()
        {
            BaseRepository<T> baseRepo = CreateRepository();
            T InitEntity = CreateEntity();

            baseRepo.Add(InitEntity);
            baseRepo.Save();

            T RetrievedEntity = GetSavedEntity(baseRepo, InitEntity);

            Assert.AreEqual(InitEntity, RetrievedEntity);
        }

        [TestMethod]
        public void SaveOk2()
        {
            BaseRepository<T> baseRepo = CreateRepository();
            T InitEntity = CreateEntity();
            T InitEntity2 = CreateEntity();

            baseRepo.Add(InitEntity);
            baseRepo.Add(InitEntity2);
            baseRepo.Save();

            T RetrievedEntity = GetSavedEntity(baseRepo, InitEntity2);

            Assert.AreEqual(InitEntity2, RetrievedEntity);
        }

    }
}
