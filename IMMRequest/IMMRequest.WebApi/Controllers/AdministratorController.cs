using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMMRequest.BusinessLogic;
using IMMRequest.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IMMRequest.WebApi.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class AdministratorController : ControllerBase {

        private AdministratorLogic administratorsLogic;
        
        public  AdministratorController() {
			administratorsLogic = new AdministratorLogic();
		}
    }
}
