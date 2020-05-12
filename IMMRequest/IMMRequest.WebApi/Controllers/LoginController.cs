using IMMRequest.BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Login([FromBody]LoginModel loginModel)
        {
            try
            {
                return Ok(this.sessionLogic.Login(loginModel.Email, loginModel.Password));
            }
            catch(Exception)
            {
                return BadRequest("Error credentials");
            }
        }
    }
}
