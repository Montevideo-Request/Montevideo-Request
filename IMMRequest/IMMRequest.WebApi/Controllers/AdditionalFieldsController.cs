using IMMRequest.BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;
using IMMRequest.WebApi.Models;
using IMMRequest.Domain;
using System;

namespace IMMRequest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdditionalFieldsController : ControllerBase
    {
        private readonly IAdditionalFieldLogic<AdditionalField, FieldRange> Logic;

        public AdditionalFieldsController(IAdditionalFieldLogic<AdditionalField, FieldRange> Logic) : base()
        {
            this.Logic = Logic;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(AdditionalFieldModel.ToModel(Logic.GetAll()));
        }

        [HttpGet("{id}", Name = "GetAdditionalFields")]
        public IActionResult Get(Guid id)
        {
            AdditionalField Fields = null;
            try
            {
                Fields = Logic.Get(id);
            }
            catch (Exception e)
            {
                //TODO: Log the problem
            }

            if (Fields == null)
            {
                //TODO: Manejar de forma choerente los códigos
                return NotFound();
            }

            return Ok(AdditionalFieldModel.ToModel(Fields));
        }

        [HttpPost("{id}/FieldRanges", Name = "AddFieldRange")]
        public IActionResult PostExercise(Guid id, [FromBody]FieldRangeModel model)
        {
            var newFieldRange = Logic.AddFieldRange(id, FieldRangeModel.ToEntity(model));
            if (newFieldRange == null) {
                return BadRequest();
            }
            return CreatedAtRoute("GetExercise", new { id = newFieldRange.Id }, FieldRangeModel.ToModel(newFieldRange));
        }

        [HttpPost]
        public IActionResult Post([FromBody]AdditionalFieldModel model)
        {
            try {
                var FieldsResult = Logic.Create(AdditionalFieldModel.ToEntity(model));
                return CreatedAtRoute("GetAdditionalFields", new { id = FieldsResult.Id }, AdditionalFieldModel.ToModel(FieldsResult));

            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

    }
}
