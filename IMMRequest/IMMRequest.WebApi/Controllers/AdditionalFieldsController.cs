using Microsoft.AspNetCore.Mvc;
using IMMRequest.BusinessLogic;
using IMMRequest.Domain;
using IMMRequest.DTO;
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

        /* START Additional Field Logic */
        #region Additional Field Logic

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(AdditionalFieldDTO.ToModel(Logic.GetAll()));
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

            return Ok(AdditionalFieldDTO.ToModel(Fields));
        }

        [HttpPost]
        [AuthenticationFilter]
        public IActionResult Post([FromBody]AdditionalFieldDTO model)
        {
            try
            {
                var FieldsResult = Logic.Create(AdditionalFieldDTO.ToEntity(model));
                return CreatedAtRoute("GetAdditionalFields", new { id = FieldsResult.Id }, AdditionalFieldDTO.ToModel(FieldsResult));

            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [AuthenticationFilter]
        public IActionResult Put(Guid id, [FromBody]AdditionalFieldDTO model)
        {
            try {
                var field = Logic.Update(AdditionalFieldDTO.ToEntity(model));

                return CreatedAtRoute("GetAdditionalFields", new { id = field.Id }, AdditionalFieldDTO.ToModel(field));
            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [AuthenticationFilter]
        public IActionResult Delete(Guid id)
        {
            try {
                Logic.Remove(id);
                return Ok("Se Elimino el Campo Adicional: " + id);
                
            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }


        #endregion
        /* END Additional Field Logic */

        /* START Field Range Logic */
        #region Field Range Logic

        [HttpPost("{id}/FieldRanges", Name = "AddFieldRange")]
        [AuthenticationFilter]
        public IActionResult PostFieldRange(Guid id, [FromBody]FieldRangeDTO model)
        {
            var newFieldRange = Logic.AddFieldRange(id, FieldRangeDTO.ToEntity(model));
            if (newFieldRange == null)
            {
                return BadRequest();
            }
            return CreatedAtRoute("GetFields", new { id = newFieldRange.Id }, FieldRangeDTO.ToModel(newFieldRange));
        }

        [HttpGet("{id}/FieldRanges", Name = "GetFields")]
        public IActionResult GetFields(Guid id)
        {
            return Ok(FieldRangeDTO.ToModel(Logic.GetAllRanges(id)));
        }

        #endregion
        /* END Field Range Logic */
    }
}
