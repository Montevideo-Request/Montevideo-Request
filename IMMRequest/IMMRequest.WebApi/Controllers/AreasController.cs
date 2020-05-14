using Microsoft.AspNetCore.Mvc;
using IMMRequest.BusinessLogic;
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
        public AreasController(IAreaLogic<Area> Logic) : base() { this.Logic = Logic; }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(AreaDTO.ToModel(Logic.GetAll()));
        }

        [HttpGet("{id}", Name = "GetAreas")]
        public IActionResult Get(Guid id)
        {
            Area AreaGet = AreaGet = Logic.Get(id);

            if (AreaGet == null)
            {
                return NotFound("Esa Area No Existe.");
            }

            return Ok(AreaDTO.ToModel(AreaGet));
        }

        [HttpPost]
        [AuthenticationFilter]
        public IActionResult Post([FromBody]AreaDTO model)
        {
            try {
                var areaResult = Logic.Create(AreaDTO.ToEntity(model));
                return CreatedAtRoute("GetAreas", new { id = areaResult.Id }, AreaDTO.ToModel(areaResult));

            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }


        [HttpPut("{id}")]
        [AuthenticationFilter]
        public IActionResult Put(Guid id, [FromBody]AreaDTO model)
        {
            try {
                var area = Logic.Update(AreaDTO.ToEntity(model));

                return CreatedAtRoute("GetAreas", new { id = area.Id }, AreaDTO.ToModel(area));
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
