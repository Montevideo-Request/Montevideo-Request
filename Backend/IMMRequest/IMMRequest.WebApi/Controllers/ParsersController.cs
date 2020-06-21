using Microsoft.AspNetCore.Mvc;
using IMMRequest.BusinessLogic;
using IMMRequest.DTO;
using System;

namespace IMMRequest.WebApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ParsersController : ControllerBase 
    {
        private readonly IParserLogic Logic;
        public ParsersController(IParserLogic Logic) : base() { this.Logic = Logic; }

        [HttpGet]
        [AuthenticationFilter]
        public IActionResult Get()
        {
            return Ok(Logic.GetAvailableParsers());
        }

        [HttpPost]
        [AuthenticationFilter]
        public IActionResult Post([FromBody]ParserDTO model)
        {
            try {
                Logic.Convert(model.Type, model.Path);
                return Ok("The Parsing upload was successful.");

            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }
    }
}
