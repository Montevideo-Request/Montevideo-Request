using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.WebApi.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using IMMRequest.BusinessLogic;
using IMMRequest.WebApi.Models;
using IMMRequest.DataAccess;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
using System.Linq;
using System;

namespace IMMRequest.WebApi.Test
{
    [TestClass]
    public class AdministratorsControllerTest
    {

        public AdministratorLogic CreateLogic()
        {
            IMMRequestContext Context = ContextFactory.GetNewContext();
            var Repository = new AdministratorRepository(Context);
            var Logic = new AdministratorLogic(Repository);

            return Logic;
        }

        [TestMethod]
        public void AdministratorsControllerGetAllTest()
        {

            var FirstAdministrator = new Administrator
            {
                Id = Guid.NewGuid(),
                Name = "First Admin",
                Email = "test@test.com",
                Password = "qwe123"
            };
            
            var SecondAdministrator = new Administrator
            {
                Id = Guid.NewGuid(),
                Name = "Second Admin",
                Email = "new@test.com",
                Password = "qwe123"
            };

            var Logic = CreateLogic();
            var Controller = new AdministratorsController(Logic);

            Logic.Create(FirstAdministrator);
            Logic.Create(SecondAdministrator);

            List<Administrator> Administrators = new List<Administrator>() { FirstAdministrator, SecondAdministrator };

            var Result = Controller.Get();
            var CreatedResult = Result as OkObjectResult;
            var AdminResults = CreatedResult.Value as IEnumerable<AdministratorModel>;

            Assert.AreEqual(Administrators.Count, AdminResults.ToList().Count);
        }

        [TestMethod]
        public void AdministratorsControllerGetTest()
        {

            var Admin = new Administrator
            {
                Id = Guid.NewGuid(),
                Name = "First Admin",
                Email = "test@test.com",
                Password = "qwe123"
            };

            var Logic = CreateLogic();
            var Controller = new AdministratorsController(Logic);

            Logic.Create(Admin);

            var Result = Controller.Get(Admin.Id);
            var CreatedResult = Result as OkObjectResult;
            var Model = CreatedResult.Value as AdministratorModel;
            
            Assert.AreEqual(Admin.Name, Model.Name);
        }

        [TestMethod]
        public void AdministratorsControllerPostTest()
        {
            
            var Admin = new Administrator
            {
                Id = Guid.NewGuid(),
                Name = "First Admin",
                Password = "Test Password",
                Email = "test@test.com"
            };

            var Logic = CreateLogic();
            var Controller = new AdministratorsController(Logic);
            var adminModel = AdministratorModel.ToModel(Admin);
            
            adminModel.Password = Admin.Password;

            var result = Controller.Post(adminModel);
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as AdministratorModel;

            Assert.AreEqual(Admin.Name, model.Name);
        }

         
        [TestMethod]
        public void AdministratorControllerUpdateTest()
        {
            var AdministratorId = Guid.NewGuid();
            var Logic = CreateLogic();
            var Controller = new AdministratorsController(Logic);

             var Admin = new Administrator
            {
                Id = AdministratorId,
                Name = "First Admin",
                Password = "Test Password",
                Email = "test@test.com"
            };

            Logic.Create(Admin);

            AdministratorModel UpdatedRequest = new AdministratorModel()
            {
                Id = AdministratorId,
                Name = "Updated Admin",
                Email = "updated@email.com"
            };

            var result = Controller.Put( AdministratorId, UpdatedRequest);
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as AdministratorModel;

            Assert.AreEqual("updated@email.com", model.Email);
        }


        [TestMethod]
        public void AdministratorControllerDeleteTest()
        {
            var id = Guid.NewGuid();
            var Administrator = new Administrator
            {
                Id = id,
                Name = "Admin",
                Email = "test@test.com",
                Password = "qwe123",
            };

            var Logic = new AdministratorLogic();
            var Controller = new AdministratorsController(Logic);

            Logic.Create(Administrator);
            Controller.Delete(Administrator.Id);

            Assert.ThrowsException<ExceptionController>(() => Logic.Get(Administrator.Id));
        }
    }
}
