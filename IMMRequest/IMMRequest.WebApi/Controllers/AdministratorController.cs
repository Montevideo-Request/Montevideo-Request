using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMMRequest.BusinessLogic;
using IMMRequest.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IMMRequest.WebApi.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class AdministratorController : ControllerBase {

        private AdministratorLogic administratorsLogic;
        
        public  AdministratorController() {
			administratorsLogic = new AdministratorLogic();
		}

		[HttpGet("{id}", Name = "Get")]
		public IActionResult Get(Guid  id) {
		  	Administrator administratortoGet = null;
			administratortoGet = administratorsLogic.Get(id);	
			if (administratortoGet == null) {
			    //TODO: Manejar de forma choerente los códigos
				return NotFound();
			}
			return Ok(administratortoGet);
		}

		[HttpPost]
		public IActionResult Post([FromBody] Administrator administrator) {
			try {
				Administrator createdAdministrator = administratorsLogic.Create(administrator);
				return CreatedAtRoute("Get", new { id = administrator.Id }, createdAdministrator);
			} 
			catch(ArgumentException e) {
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id}")]
		public IActionResult Put(Guid id, [FromBody] Administrator administrator) {
			try {
				Administrator updatedAdministrator = administratorsLogic.Update(id, administrator);
				return CreatedAtRoute("Get", new { id = administrator.Id }, updatedAdministrator);
			} 
            catch(ArgumentException e) {
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(Guid id) {
			administratorsLogic.Remove(id);
			return NoContent();
		}
    }
}
