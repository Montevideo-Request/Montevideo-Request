using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.WebApi.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using IMMRequest.BusinessLogic;
using IMMRequest.WebApi.Models;
using IMMRequest.DataAccess;
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
                Email = "test@test.com"
            };
            
            var SecondAdministrator = new Administrator
            {
                Id = Guid.NewGuid(),
                Name = "Second Admin",
                Email = "new@test.com"
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
                Email = "test@test.com"
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

            var result = Controller.Post(AdministratorModel.ToModel(Admin));
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as AdministratorModel;

            Assert.AreEqual(Admin.Name, model.Name);
        }
    }
}
