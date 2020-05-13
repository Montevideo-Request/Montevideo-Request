using Microsoft.AspNetCore.Mvc;
using IMMRequest.BusinessLogic;
using IMMRequest.Domain;
using IMMRequest.DTO;
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
            return Ok(RequestDTO.ToModel(Logic.GetAll()));
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

            return Ok(RequestDTO.ToModel(RequestGet));
        }

        [HttpPost]
        public IActionResult Post([FromBody]RequestDTO model)
        {
            try {
                var RequestResult = Logic.Create(RequestDTO.ToEntity(model));
                return CreatedAtRoute("GetRequests", new { id = RequestResult.Id }, RequestDTO.ToModel(RequestResult));

            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [AuthenticationFilter]
        public IActionResult Put(Guid id, [FromBody]RequestDTO model)
        {
            try {
                var request = Logic.Update(RequestDTO.ToEntity(model));

                return CreatedAtRoute("GetRequests", new { id = request.Id }, RequestDTO.ToModel(request));
            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }
    }
}
