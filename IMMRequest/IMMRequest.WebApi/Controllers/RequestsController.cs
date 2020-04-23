﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IMMRequest.BusinessLogic;

namespace IMMRequest.WebApi.Controllers
{
    [ApiController]
    [Route("api/requests")]
    public class RequestsController : ControllerBase
    {
        private readonly RequestLogic requestLogic;
        public RequestsController() 
        {
            this.requestLogic = new RequestLogic();
        }
        
        // GET api/requests
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(this.requestLogic.GetAll());
        }
    }
}