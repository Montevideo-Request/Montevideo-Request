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
                Administrator admin = AdministratorModel.ToEntity(model);
                Logic.Add(admin);

                return CreatedAtRoute("Get", new { id = admin.Id }, AdministratorModel.ToModel(admin));

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
		public IActionResult Delete(Guid id) {
			Logic.Remove(id);
			return NoContent();
		}
    }
}
