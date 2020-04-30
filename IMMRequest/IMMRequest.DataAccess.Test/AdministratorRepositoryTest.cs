using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.DataAccess.Test
{
    [TestClass]
    public class AdministratorRepositoryTest : BaseRepositoryTest<Administrator>
    {

        public override Administrator CreateEntity()
        {
            Administrator Admin = new Administrator();
            return Admin;
        }

        public override Administrator ModifyEntity(Administrator Admin, string prop)
        {
            Administrator ModifiedAdmin = Admin;
            ModifiedAdmin.Name = prop;
            return ModifiedAdmin;
        }

        public override string GetEntityProp()
        {
            return "AdminName";
        }

        public override Boolean CompareProps(Administrator Admin, string prop)
        {
            return Admin.Name == prop;
        }

        public override Administrator GetSavedEntity(BaseRepository<Administrator> adminRepo, Administrator Admin)
        {
            Administrator AdminToReturn = adminRepo.Get(Admin.Id);
            return AdminToReturn;
        }

        public override BaseRepository<Administrator> CreateRepository()
        {
            IMMRequestContext IMMRequestContext = ContextFactory.GetNewContext();
            AdministratorRepository adminRepo = new AdministratorRepository(IMMRequestContext);

            return adminRepo;
        }

        [TestMethod]
        public void TestAdministratorGetAllOK()
        {
            var adminRepo = CreateRepository();

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
            var adminRepo = CreateRepository();

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
            var adminRepo = CreateRepository();

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
            var adminRepo = CreateRepository();

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
            var adminRepo = CreateRepository();

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
        [ExpectedException(typeof(InvalidOperationException), "El administrador no existe")]
        public void GetInvalid()
        {
            var id = Guid.NewGuid();
            var adminRepo = CreateRepository();

            adminRepo.Get(id);
        }


        [TestMethod]
        public void TestAdditionalFieldGetParent()
        {
            var adminRepo = CreateRepository();

            Administrator admin = new Administrator()
            {
                Name = "Parent admin"
            };

            adminRepo.Add(admin);
            adminRepo.Save();

            Administrator parentAdmin = adminRepo.GetParent(admin.Id);

            Assert.AreEqual(parentAdmin.Name, admin.Name);
        }
    }
}
