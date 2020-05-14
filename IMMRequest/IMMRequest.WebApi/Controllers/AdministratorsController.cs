using Microsoft.AspNetCore.Mvc;
using IMMRequest.BusinessLogic;
using IMMRequest.Domain;
using IMMRequest.DTO;
using System;
using System.Net;


namespace IMMRequest.WebApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AdministratorsController : ControllerBase {

        private readonly IAdministratorLogic<Administrator> Logic;
        public  AdministratorsController(IAdministratorLogic<Administrator> Logic) : base() { this.Logic = Logic; }

        [HttpGet]
        [AuthenticationFilter]
        public IActionResult Get()
        {
            return Ok(AdministratorDTO.ToModel(Logic.GetAll()));
        }

        [HttpGet("{id}", Name = "GetAdmins")]
        public IActionResult Get(Guid id)
        {
            Administrator AdminGet = Logic.Get(id);

            if (AdminGet == null) {
                return NotFound("Ese Administrador No Existe.");
            }

            return Ok(AdministratorDTO.ToModel(AdminGet));
        }

        [HttpPost]
        [AuthenticationFilter]
        public IActionResult Post([FromBody]AdministratorDTO model)
        {
            try {
                var adminResult = Logic.Create(AdministratorDTO.ToEntity(model));
                return CreatedAtRoute("GetAdmins", new { id = adminResult.Id }, AdministratorDTO.ToModel(adminResult));

            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

		[HttpPut("{id}")]
        [AuthenticationFilter]
        public IActionResult Put(Guid id, [FromBody]AdministratorDTO model)
        {
            try {
                var admin = Logic.Update(AdministratorDTO.ToEntity(model));

                return CreatedAtRoute("GetAdmins", new { id = admin.Id }, AdministratorDTO.ToModel(admin));
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
                return Ok("Se Elimino el Administrador: " + id);
                
            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }
    }
}
