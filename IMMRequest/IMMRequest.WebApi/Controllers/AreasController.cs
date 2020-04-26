using IMMRequest.BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;
using IMMRequest.WebApi.Models;
using IMMRequest.Domain;
using System;

namespace IMMRequest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreasController : ControllerBase
    {
        private readonly ILogic<Area> Logic;

        public AreasController(ILogic<Area> Logic) : base()
        {
            this.Logic = Logic;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(AreaModel.ToModel(Logic.GetAll()));
        }

        [HttpGet("{id}", Name = "GetAreas")]
        public IActionResult Get(Guid id)
        {
            Area AreaGet = null;
            try
            {
                AreaGet = Logic.Get(id);
            }
            catch (Exception e)
            {
                //TODO: Log the problem
            }

            if (AreaGet == null)
            {
                //TODO: Manejar de forma choerente los códigos
                return NotFound();
            }

            return Ok(AreaModel.ToModel(AreaGet));
        }

        [HttpPost]
        public IActionResult Post([FromBody]AreaModel model)
        {
            try {
                var areaResult = Logic.Create(AreaModel.ToEntity(model));
                return CreatedAtRoute("GetAreas", new { id = areaResult.Id }, AreaModel.ToModel(areaResult));

            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

        // [HttpPost("{id}/Topics", Name = "AddTopic")]
        // public IActionResult PostExercise(Guid id, [FromBody]TopicModel model)
        // {
        //     var topicResult = Logic.Create(id, TopicModel.ToEntity(model));
        //     if (topicResult == null) {
        //         return BadRequest();
        //     }
        //     return CreatedAtRoute("GetExercise", new { id = topicResult.Id }, TopicModel.ToModel(topicResult));
        // }

    }
}
