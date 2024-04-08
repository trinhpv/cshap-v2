using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Shopping.Presentation.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportController : ControllerBase
    {
        [Route("/[controller]/GetReport")]
        [HttpGet]
        public string GetReport()
        {
            return "oke";
        }
    }

    
}
