using Microsoft.AspNetCore.Mvc;
using IMMRequest.BusinessLogic;
using IMMRequest.Domain;
using IMMRequest.DTO;
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
        public IActionResult Get()
        {
            return Ok(TopicDTO.ToModel(Logic.GetAll()));
        }

        [HttpGet("{id}", Name = "GetTopics")]
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

            return Ok(TopicDTO.ToModel(TopicGet));
        }

        [HttpPost]
        [AuthenticationFilter]
        public IActionResult Post([FromBody]TopicDTO model)
        {
            try {
                var topicResult = Logic.Create(TopicDTO.ToEntity(model));
                return CreatedAtRoute("GetTopics", new { id = topicResult.Id }, TopicDTO.ToModel(topicResult));

            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [AuthenticationFilter]
        public IActionResult Put(Guid id, [FromBody]TopicDTO model)
        {
            try {
                var topic = Logic.Update(TopicDTO.ToEntity(model));

                return CreatedAtRoute("GetTopics", new { id = topic.Id }, TopicDTO.ToModel(topic));
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
                return Ok("Se Elimino el Tema: " + id);
                
            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }
    }
}
