using IMMRequest.BusinessLogic.Interface;
using System.Collections.Generic;
using IMMRequest.BusinessLogic;
using IMMRequest.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using IMMRequest.Domain;
using System;

namespace IMMRequest.WebApi.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class AdministratorsController : ControllerBase {

        private readonly ILogic<Administrator> Logic;
        
        public  AdministratorsController(ILogic<Administrator> Logic) : base()
        {
			this.Logic = Logic;
		}

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(AdministratorModel.ToModel(Logic.GetAll()));
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(Guid id)
        {
            Administrator AdminGet = null;
            try {
                AdminGet = Logic.Get(id);
            }
            catch (Exception e){
                //TODO: Log the problem
            }
           
            if (AdminGet == null) {
                //TODO: Manejar de forma choerente los códigos
                return NotFound();
            }

            return Ok(AdministratorModel.ToModel(AdminGet));
        }

         [HttpPost]
        public IActionResult Post([FromBody]AdministratorModel model)
        {
            try {
                var adminResult = Logic.Create(AdministratorModel.ToEntity(model));
                return CreatedAtRoute("Get", new { id = adminResult.Id }, AdministratorModel.ToModel(adminResult));

            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

		[HttpPut("{id}")]
		public IActionResult Put(Guid id, [FromBody] Administrator administrator) {
			try
            {
                administrator.Id = id;
                Logic.Update(administrator);
                return Ok("Persona actualizada");
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(Administrator admin) {
			Logic.Remove(admin);
			return NoContent();
		}
    }
}
