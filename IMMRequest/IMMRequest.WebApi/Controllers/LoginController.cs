using Microsoft.AspNetCore.Mvc;
using IMMRequest.BusinessLogic;
using IMMRequest.DTO;
using System;
namespace IMMRequest.WebApi
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly ISessionLogic sessionLogic;

        public LoginController(ISessionLogic _sessionLogic)
        {
            this.sessionLogic = _sessionLogic;
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginDTO LoginDTO)
        {
            try
            {
                return Ok(this.sessionLogic.Login(LoginDTO.Email, LoginDTO.Password));
            }
            catch(Exception)
            {
                return BadRequest("Error credentials");
            }
        }
    }
}
