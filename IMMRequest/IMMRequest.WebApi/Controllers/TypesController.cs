using Microsoft.AspNetCore.Mvc;
using IMMRequest.BusinessLogic;
using IMMRequest.Domain;
using IMMRequest.DTO;
using System;

namespace IMMRequest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypesController : ControllerBase
    {
        private readonly ILogic<TypeEntity> Logic;
        public TypesController(ILogic<TypeEntity> Logic) : base() { this.Logic = Logic; }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(TypeDTO.ToModel(Logic.GetAll()));
        }

        [HttpGet("{id}", Name = "GetTypes")]
        public IActionResult Get(Guid id)
        {
            TypeEntity TypeGet = Logic.Get(id);

            if (TypeGet == null)
            {
                return NotFound();
            }

            return Ok(TypeDTO.ToModel(TypeGet));
        }

        [HttpPost]
        [AuthenticationFilter]
        public IActionResult Post([FromBody]TypeDTO model)
        {
            try {
                var typeResult = Logic.Create(TypeDTO.ToEntity(model));
                return CreatedAtRoute("GetTypes", new { id = typeResult.Id }, TypeDTO.ToModel(typeResult));

            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [AuthenticationFilter]
        public IActionResult Put(Guid id, [FromBody]TypeDTO model)
        {
            try {
                var type = Logic.Update(TypeDTO.ToEntity(model));

                return CreatedAtRoute("GetTypes", new { id = type.Id }, TypeDTO.ToModel(type));
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
                return Ok("The Type was Deleted: " + id);
                
            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }
    }
}
