using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TodoApiLatest.Controllers.v3
{
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        public List<string> Get()
        {
            return new List<string>()
            {
                "Todos from api v3",
                "Go for a world tour",
                "Do meaningful work!",
                "Call daily to parents"
            } ;
        }
    }
}
