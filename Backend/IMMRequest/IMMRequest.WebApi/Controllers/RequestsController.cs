using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using IMMRequest.BusinessLogic;
using IMMRequest.Exceptions;
using IMMRequest.Domain;
using IMMRequest.DTO;
using System;

namespace IMMRequest.WebApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class RequestsController : ControllerBase {

        private readonly IRequestLogic<Request, TypeEntity> Logic;
        public  RequestsController(IRequestLogic<Request, TypeEntity> Logic) : base() { this.Logic = Logic; }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(RequestDTO.ToModel(Logic.GetAll()));
        }

        [HttpGet("{id}", Name = "GetRequests")]
        public IActionResult Get(Guid id)
        {
            Request RequestGet = Logic.Get(id);
            
            if (RequestGet == null) {
                return NotFound();
            }

            return Ok(RequestDTO.ToModel(RequestGet));
        }

        [HttpGet]
        [Route("states")]
        public IActionResult GetStates()
        {
            IEnumerable<string> states = Logic.GetValidStates();
            if (states == null) {
                return NotFound();
            }
            
            return Ok(states);
        }

        [HttpPost]
        public IActionResult Post([FromBody]RequestDTO model)
        {
            try {
                var RequestResult = Logic.Create(RequestDTO.ToEntity(model));
                return CreatedAtRoute("GetRequests", new { id = RequestResult.Id }, RequestDTO.ToModel(RequestResult));

            } catch(ExceptionController e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [AuthenticationFilter]
        public IActionResult Put(Guid id, [FromBody]RequestDTO model)
        {
            try {
                model.Id = id;
                var request = Logic.Update(RequestDTO.ToEntity(model));

                return CreatedAtRoute("GetRequests", new { id = request.Id }, RequestDTO.ToModel(request));
            } catch(ExceptionController e) {
                return BadRequest(e.Message);
            }
        }
    }
}
