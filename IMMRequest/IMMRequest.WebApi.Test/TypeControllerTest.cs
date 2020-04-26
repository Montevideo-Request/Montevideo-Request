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
    public class TypesControllerTest
    {

        public TypeLogic CreateLogic()
        {
            IMMRequestContext Context = ContextFactory.GetNewContext();
            var Repository = new TypeRepository(Context);
            var Logic = new TypeLogic(Repository);

            return Logic;
        }

        [TestMethod]
        public void TypesControllerGetAllTest()
        {

            var FirstType = new TypeEntity
            {
                Id = Guid.NewGuid(),
                Name = "First Type",
            };
            
            var SecondType = new TypeEntity
            {
                Id = Guid.NewGuid(),
                Name = "Second Type",
            };

            var Logic = CreateLogic();
            var Controller = new TypesController(Logic);

            Logic.Create(FirstType);
            Logic.Create(SecondType);

            List<TypeEntity> Types = new List<TypeEntity>() { FirstType, SecondType };

            var Result = Controller.Get();
            var CreatedResult = Result as OkObjectResult;
            var TypeResults = CreatedResult.Value as IEnumerable<TypeModel>;

            Assert.AreEqual(Types.Count, TypeResults.ToList().Count);
        }

        [TestMethod]
        public void TypesControllerGetTest()
        {

            var Type = new TypeEntity
            {
                Id = Guid.NewGuid(),
                Name = "First Type",
            };

            var Logic = CreateLogic();
            var Controller = new TypesController(Logic);

            Logic.Create(Type);

            var Result = Controller.Get(Type.Id);
            var CreatedResult = Result as OkObjectResult;
            var Model = CreatedResult.Value as TypeModel;
            
            Assert.AreEqual(Type.Name, Model.Name);
        }

        [TestMethod]
        public void TypeControllerPostTest()
        {
            
            var Type = new TypeEntity
            {
                Id = Guid.NewGuid(),
                Name = "First Type"
            };

            var Logic = CreateLogic();
            var Controller = new TypesController(Logic);

            var result = Controller.Post(TypeModel.ToModel(Type));
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as TypeModel;

            Assert.AreEqual(Type.Name, model.Name);
        }
    }
}
