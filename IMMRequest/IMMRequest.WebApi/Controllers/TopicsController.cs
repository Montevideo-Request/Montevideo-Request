using IMMRequest.BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;
using IMMRequest.WebApi.Models;
using IMMRequest.Domain;
using System;

namespace IMMRequest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicsController : ControllerBase
    {
        private readonly ILogic<Topic> Logic;

        public TopicsController(ILogic<Topic> Logic) : base()
        {
            this.Logic = Logic;
        }

        [HttpGet]
        [AuthenticationFilter]
        public IActionResult Get()
        {
            return Ok(TopicModel.ToModel(Logic.GetAll()));
        }

        [HttpGet("{id}", Name = "GetTopics")]
        [AuthenticationFilter]
        public IActionResult Get(Guid id)
        {
            Topic TopicGet = null;
            try
            {
                TopicGet = Logic.Get(id);
            }
            catch (Exception e)
            {
                //TODO: Log the problem
            }

            if (TopicGet == null)
            {
                //TODO: Manejar de forma choerente los códigos
                return NotFound();
            }

            return Ok(TopicModel.ToModel(TopicGet));
        }

        [HttpPost]
        [AuthenticationFilter]
        public IActionResult Post([FromBody]TopicModel model)
        {
            try {
                var topicResult = Logic.Create(TopicModel.ToEntity(model));
                return CreatedAtRoute("GetTopics", new { id = topicResult.Id }, TopicModel.ToModel(topicResult));

            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }


    }
}
