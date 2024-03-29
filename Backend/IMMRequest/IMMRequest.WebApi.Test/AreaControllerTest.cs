using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMMRequest.WebApi.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using IMMRequest.BusinessLogic;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
using IMMRequest.DTO;
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
            var AreaResults = CreatedResult.Value as IEnumerable<AreaDTO>;

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
            var Model = CreatedResult.Value as AreaDTO;
            
            Assert.AreEqual(Area.Name, Model.Name);
        }


        [TestMethod]
        public void AreaControllerPostTest()
        {
            var Area = new Area
            {
                Id = Guid.NewGuid(),
                Name = "First Area",
            };

            var Logic = new AreaLogic();
            var Controller = new AreasController(Logic);
            var result = Controller.Post(AreaDTO.ToModel(Area));
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as AreaDTO;

            Assert.AreEqual(Area.Name, model.Name);
        }

         [TestMethod]
        public void AreasControllerUpdateTest()
        {
            var AreaId = Guid.NewGuid();
            var Logic = new AreaLogic();
            var Controller = new AreasController(Logic);

            var Area = new Area
            {
                Id = AreaId,
                Name = "First Area",
            };

            Logic.Create(Area);

            AreaDTO UpdatedArea = new AreaDTO()
            {
                Id = AreaId,
                Name = "Updated Area"
            };

            var result = Controller.Put( AreaId, UpdatedArea);
            var createdResult = result as CreatedAtRouteResult;
            var model = createdResult.Value as AreaDTO;

            Assert.AreEqual("Updated Area", model.Name);
        }

        [TestMethod]
        public void AreaControllerDeleteTest()
        {
            var Area = new Area
            {
                Id = Guid.NewGuid(),
                Name = "First Area",
            };

            var Logic = new AreaLogic();
            var Controller = new AreasController(Logic);

            Logic.Create(Area);
            Controller.Delete(Area.Id);

            Assert.ThrowsException<ExceptionController>(() => Logic.Get(Area.Id));
        }
    }
}
