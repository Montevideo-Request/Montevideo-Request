using IMMRequest.BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;
using IMMRequest.WebApi.Models;
using IMMRequest.Domain;
using System;

namespace IMMRequest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypesController : ControllerBase
    {
        private readonly ILogic<TypeEntity> Logic;

        public TypesController(ILogic<TypeEntity> Logic) : base()
        {
            this.Logic = Logic;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(TypeModel.ToModel(Logic.GetAll()));
        }

        [HttpGet("{id}", Name = "GetTypes")]
        public IActionResult Get(Guid id)
        {
            TypeEntity TypeGet = null;
            try
            {
                TypeGet = Logic.Get(id);
            }
            catch (Exception e)
            {
                //TODO: Log the problem
            }

            if (TypeGet == null)
            {
                //TODO: Manejar de forma choerente los códigos
                return NotFound();
            }

            return Ok(TypeModel.ToModel(TypeGet));
        }

        [HttpPost]
        [AuthenticationFilter]
        public IActionResult Post([FromBody]TypeModel model)
        {
            try {
                var typeResult = Logic.Create(TypeModel.ToEntity(model));
                return CreatedAtRoute("GetTypes", new { id = typeResult.Id }, TypeModel.ToModel(typeResult));

            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [AuthenticationFilter]
        public IActionResult Put(Guid id, [FromBody]TypeModel model)
        {
            try {
                var type = Logic.Update(TypeModel.ToEntity(model));

                return CreatedAtRoute("GetRequests", new { id = type.Id }, TypeModel.ToModel(type));
            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

    }
}
