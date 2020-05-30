using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess.Test
{
    [TestClass]
    public class AdministratorRepositoryTest
    {

        public Administrator CreateEntity()
        {
            Administrator Admin = new Administrator();
            return Admin;
        }

        public Administrator ModifyEntity(Administrator Admin, string prop)
        {
            Administrator ModifiedAdmin = Admin;
            ModifiedAdmin.Name = prop;
            return ModifiedAdmin;
        }

        public string GetEntityProp()
        {
            return "AdminName";
        }

        public Boolean CompareProps(Administrator Admin, string prop)
        {
            return Admin.Name == prop;
        }

        public Administrator GetSavedEntity(BaseRepository<Administrator, Administrator> adminRepo, Administrator Admin)
        {
            Administrator AdminToReturn = adminRepo.Get(Admin.Id);
            return AdminToReturn;
        }


        [TestMethod]
        public void TestAdministratorGetAllOK()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AdministratorRepository adminRepo = new AdministratorRepository(IMMRequestContext);

            adminRepo.Add(new Administrator
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                Email = "first@test.com",
                Password = "notSecure"
            });

            adminRepo.Save();

            var admins = adminRepo.GetAll().ToList().Count();
            Assert.AreEqual(1, admins);
        }

         [TestMethod]
        public void TestAdministratorGetAllOK2()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AdministratorRepository adminRepo = new AdministratorRepository(IMMRequestContext);

            adminRepo.Add(new Administrator
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                Email = "first@test.com",
                Password = "notSecure"
            });

            adminRepo.Add(new Administrator
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing again",
                Email = "second@test.com",
                Password = "notSecure"
            });

            adminRepo.Save();

            var admins = adminRepo.GetAll().ToList().Count();
            Assert.AreEqual(2, admins);
        }

        
        [TestMethod]
        public void TestAdministratorGetAll3()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AdministratorRepository adminRepo = new AdministratorRepository(IMMRequestContext);

            Administrator admin = new Administrator()
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                Email = "first@test.com",
                Password = "notSecure"
            };

            adminRepo.Add(admin);
            adminRepo.Save();

            var admins = adminRepo.GetAll().ToList();

            Assert.AreEqual(admins.First(), admin);
        }

        [TestMethod]
        public void TestAdministratorGet()
        {
            var id = Guid.NewGuid();
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AdministratorRepository adminRepo = new AdministratorRepository(IMMRequestContext);

            Administrator admin = new Administrator()
            {
                Id = id,
                Name = "Just Testing",
                Email = "first@test.com",
                Password = "notSecure"
            };

            adminRepo.Add(admin);
            adminRepo.Save();

            Assert.AreEqual(adminRepo.Get(id), admin);
        }

        [TestMethod]
        public void TestAdministratorGet2()
        {
            var id = Guid.NewGuid();
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AdministratorRepository adminRepo = new AdministratorRepository(IMMRequestContext);

             Administrator admin1 = new Administrator()
            {
                Id = Guid.NewGuid(),
                Name = "Just Testing",
                Email = "first@test.com",
                Password = "notSecure"
            };

            Administrator admin2 = new Administrator()
            {
                Id = id,
                Name = "Just Testing again",
                Email = "second@test.com",
                Password = "notSecure"
            };

            adminRepo.Add(admin1);
            adminRepo.Add(admin2);
            adminRepo.Save();

            Assert.AreEqual(adminRepo.Get(id), admin2);
        }


        [TestMethod]
        [ExpectedException(typeof(ExceptionController), "El administrador no existe")]
        public void GetInvalid()
        {
            var id = Guid.NewGuid();
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AdministratorRepository adminRepo = new AdministratorRepository(IMMRequestContext);

            adminRepo.Get(id);
        }
    }
}
