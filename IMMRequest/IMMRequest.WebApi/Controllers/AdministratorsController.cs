using IMMRequest.BusinessLogic.Interface;
using System.Collections.Generic;
using IMMRequest.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using IMMRequest.Domain;
using System;

namespace IMMRequest.WebApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AdministratorsController : ControllerBase {

        private readonly IAdministratorLogic<Administrator> Logic;
        
        public  AdministratorsController(IAdministratorLogic<Administrator> Logic) : base()
        {
			this.Logic = Logic;
		}

        [HttpGet]
        [AuthenticationFilter]
        public IActionResult Get()
        {
            return Ok(AdministratorModel.ToModel(Logic.GetAll()));
        }

        [HttpGet("{id}", Name = "GetAdmins")]
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
        [AuthenticationFilter]
        public IActionResult Post([FromBody]AdministratorModel model)
        {
            try {
                var adminResult = Logic.Create(AdministratorModel.ToEntity(model));
                return CreatedAtRoute("GetAdmins", new { id = adminResult.Id }, AdministratorModel.ToModel(adminResult));

            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

		[HttpPut("{id}")]
        [AuthenticationFilter]
        public IActionResult Put(Guid id, [FromBody]AdministratorModel model)
        {
            try {
                var admin = Logic.Update(AdministratorModel.ToEntity(model));

                return CreatedAtRoute("GetRequests", new { id = admin.Id }, AdministratorModel.ToModel(admin));
            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

		// [HttpDelete("{id}")]
		// public IActionResult Delete(Administrator admin) {
		// 	Logic.Remove(admin);
		// 	return NoContent();
		// }
    }
}
