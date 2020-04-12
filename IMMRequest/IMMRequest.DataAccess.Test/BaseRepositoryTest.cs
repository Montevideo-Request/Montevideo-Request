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
        public abstract T ModifiedEntity(T Entity);
        public abstract T GetSavedEntity(BaseRepository<T> BaseRepository, T Entity);
        public abstract BaseRepository<T> CreateRepository();

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
            // BaseRepository<T> baseRepo = CreateRepository();

            // T InitEntity = CreateEntity();
            // T EntiToModify = ModifiedEntity(InitEntity);

            // baseRepo.Add(InitEntity);
            // baseRepo.Save();

            // baseRepo.Update(EntiToModify);
            // baseRepo.Save();

            // Assert.AreNotEqual(InitEntity, EntiToModify);
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

    }
}
