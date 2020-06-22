using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using IMMRequest.BusinessLogic;
using IMMRequest.Exceptions;

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

        [HttpGet("{type}", Name = "GetParserDetails")]
        [AuthenticationFilter]
        public IActionResult Get(string type)
        {
            var parserDetails = Logic.GetRequiredFields(type);
            if (parserDetails == null)
            {
                return NotFound();
            }

            return Ok(parserDetails);
        }

        [HttpPost]
        [AuthenticationFilter]
        public IActionResult Post([FromBody]Dictionary<string, string> parserModel)
        {
            try {
                Logic.Convert(parserModel);
                return Ok();

            } catch(ExceptionController e) {
                return BadRequest(e.Message);
            }
        }
    }
}
