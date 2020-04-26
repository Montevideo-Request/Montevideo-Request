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
    public class AdditionalFieldsControllerTest
    {

        public AdditionalFieldLogic CreateLogic()
        {
            IMMRequestContext Context = ContextFactory.GetNewContext();
            var Repository = new AdditionalFieldRepository(Context);
            var Logic = new AdditionalFieldLogic(Repository);

            return Logic;
        }

        [TestMethod]
        public void AdditionalFieldsControllerGetAllTest()
        {

            var FirstAdditionalField = new AdditionalField
            {
                Id = Guid.NewGuid(),
                Name = "First Field",
            };
            
            var SecondAdditionalField = new AdditionalField
            {
                Id = Guid.NewGuid(),
                Name = "Second Field",
            };

            var Logic = CreateLogic();
            var Controller = new AdditionalFieldsController(Logic);

            Logic.Create(FirstAdditionalField);
            Logic.Create(SecondAdditionalField);

            List<AdditionalField> AdditionalFields = new List<AdditionalField>() { FirstAdditionalField, SecondAdditionalField };

            var Result = Controller.Get();
            var CreatedResult = Result as OkObjectResult;
            var FieldResults = CreatedResult.Value as IEnumerable<AdditionalFieldModel>;

            Assert.AreEqual(AdditionalFields.Count, FieldResults.ToList().Count);
        }

        [TestMethod]
        public void AdditionalFieldsControllerGetTest()
        {

            var Field = new AdditionalField
            {
                Id = Guid.NewGuid(),
                Name = "First Field",
            };

            var Logic = CreateLogic();
            var Controller = new AdditionalFieldsController(Logic);

            Logic.Create(Field);

            var Result = Controller.Get(Field.Id);
            var CreatedResult = Result as OkObjectResult;
            var Model = CreatedResult.Value as AdditionalFieldModel;
            
            Assert.AreEqual(Field.Name, Model.Name);
        }
    }
}
