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
    public class AreaControllerTest
    {

        [TestMethod]
        public void AreasControllerGetAllTest()
        {

            var FirstArea = new Area
            {
                Id = Guid.NewGuid(),
                Name = "First Area",
            };
            
            var SecondArea = new Area
            {
                Id = Guid.NewGuid(),
                Name = "Second Area",
            };

            var Logic = new AreaLogic();
            var Controller = new AreasController(Logic);

            Logic.Create(FirstArea);
            Logic.Create(SecondArea);

            List<Area> Areas = new List<Area>() { FirstArea, SecondArea };

            var Result = Controller.Get();
            var CreatedResult = Result as OkObjectResult;
            var AreaResults = CreatedResult.Value as IEnumerable<AreaModel>;

            Assert.AreEqual(Areas.Count, AreaResults.ToList().Count);
        }

        [TestMethod]
        public void AreasControllerGetTest()
        {

            var Area = new Area
            {
                Id = Guid.NewGuid(),
                Name = "First Area",
            };

            var Logic = new AreaLogic();
            var Controller = new AreasController(Logic);

            Logic.Create(Area);

            var Result = Controller.Get(Area.Id);
            var CreatedResult = Result as OkObjectResult;
            var Model = CreatedResult.Value as AreaModel;
            
            Assert.AreEqual(Area.Name, Model.Name);
        }
    }
}
