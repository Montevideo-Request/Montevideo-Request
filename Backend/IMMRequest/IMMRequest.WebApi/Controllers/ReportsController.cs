using Microsoft.AspNetCore.Mvc;
using IMMRequest.BusinessLogic;

namespace IMMRequest.WebApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase 
    {
        private readonly IReportLogic ReportLogic;
        public ReportsController(IReportLogic ReportLogic) : base() { this.ReportLogic = ReportLogic; }

        [AuthenticationFilter]
        [HttpGet("filter")]
        [Route("A")]
        public IActionResult GetFilteredRequests([FromQuery] string email, [FromQuery] string from, [FromQuery] string to)
        {
            var response = ReportLogic.GetFilteredRequests(email, from, to);
            return Ok(response);
        }

        [AuthenticationFilter]
        [HttpGet("filter")]
        [Route("B")]
        public IActionResult GetFilteredTypes([FromQuery] string from, [FromQuery] string to)
        {
            string response = this.ReportLogic.GetFilteredTypes(from, to);
            return Ok(response);
        }
    }
}
