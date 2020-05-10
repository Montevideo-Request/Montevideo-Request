using IMMRequest.BusinessLogic.Interface;
using System.Collections.Generic;
using IMMRequest.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using IMMRequest.Domain;
using System;

namespace IMMRequest.WebApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class RequestsController : ControllerBase {

        private readonly IRequestLogic<Request, TypeEntity> Logic;
        
        public  RequestsController(IRequestLogic<Request, TypeEntity> Logic) : base()
        {
			this.Logic = Logic;
		}

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(RequestModel.ToModel(Logic.GetAll()));
        }

        [HttpGet("{id}", Name = "GetRequests")]
        public IActionResult Get(Guid id)
        {
            Request RequestGet = null;
            RequestGet = Logic.Get(id);
            
            if (RequestGet == null) {
                //TODO: Exceptions
                return NotFound();
            }

            return Ok(RequestModel.ToModel(RequestGet));
        }

        [HttpPost]
        public IActionResult Post([FromBody]RequestModel model)
        {
            try {
                var RequestResult = Logic.Create(RequestModel.ToEntity(model));
                return CreatedAtRoute("GetRequests", new { id = RequestResult.Id }, RequestModel.ToModel(RequestResult));

            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [AuthenticationFilter]
        public IActionResult Put(Guid id, [FromBody]RequestModel model)
        {
            try {
                var request = Logic.Update(RequestModel.ToEntity(model));

                return CreatedAtRoute("GetRequests", new { id = request.Id }, RequestModel.ToModel(request));
            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }
    }
}
