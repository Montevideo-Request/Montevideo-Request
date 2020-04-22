using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IMMRequest.BusinessLogic;
using IMMRequest.BusinessLogic.Interface;
using IMMRequest.Domain;
using IMMRequest.WebApi.Models;

namespace IMMRequest.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdditionalFieldsController : ControllerBase
    {

        private readonly ILogic<AdditionalField> Logic;

        public AdditionalFieldsController(ILogic<AdditionalField> Logic) : base()
        {
            this.Logic = Logic;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(AdditionalFieldModel.ToModel(Logic.GetAll()));
        }

        // [HttpGet("{id}", Name = "Get")]
        // public IActionResult Get(Guid id)
        // {
        //     Administrator AdminGet = null;
        //     try
        //     {
        //         AdminGet = Logic.Get(id);
        //     }
        //     catch (Exception e)
        //     {
        //         //TODO: Log the problem
        //     }

        //     if (AdminGet == null)
        //     {
        //         //TODO: Manejar de forma choerente los códigos
        //         return NotFound();
        //     }

        //     return Ok(AdministratorModel.ToModel(AdminGet));
        // }

        // [HttpPost]
        // public IActionResult Post([FromBody]AdministratorModel model)
        // {
        //     try
        //     {
        //         Administrator admin = AdministratorModel.ToEntity(model);
        //         Logic.Add(admin);

        //         return CreatedAtRoute("Get", new { id = admin.Id }, AdministratorModel.ToModel(admin));

        //     }
        //     catch (ArgumentException e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }


    }
}
