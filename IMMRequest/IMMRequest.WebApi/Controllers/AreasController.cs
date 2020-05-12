using IMMRequest.BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;
using IMMRequest.Domain;
using IMMRequest.DTO;
using System;

namespace IMMRequest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreasController : ControllerBase
    {
        private readonly IAreaLogic<Area> Logic;

        public AreasController(IAreaLogic<Area> Logic) : base()
        {
            this.Logic = Logic;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(AreaModel.ToModel(Logic.GetAll()));
        }

        [HttpGet("{id}", Name = "GetAreas")]
        public IActionResult Get(Guid id)
        {
            Area AreaGet = null;
            try
            {
                AreaGet = Logic.Get(id);
            }
            catch (Exception e)
            {
                //TODO: Log the problem
            }

            if (AreaGet == null)
            {
                //TODO: Manejar de forma choerente los códigos
                return NotFound();
            }

            return Ok(AreaModel.ToModel(AreaGet));
        }

        [HttpPost]
        [AuthenticationFilter]
        public IActionResult Post([FromBody]AreaModel model)
        {
            try {
                var areaResult = Logic.Create(AreaModel.ToEntity(model));
                return CreatedAtRoute("GetAreas", new { id = areaResult.Id }, AreaModel.ToModel(areaResult));

            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }


        [HttpPut("{id}")]
        [AuthenticationFilter]
        public IActionResult Put(Guid id, [FromBody]AreaModel model)
        {
            try {
                var area = Logic.Update(AreaModel.ToEntity(model));

                return CreatedAtRoute("GetAreas", new { id = area.Id }, AreaModel.ToModel(area));
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
                return Ok("Se Elimino el Area: " + id);
                
            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }
    }
}
