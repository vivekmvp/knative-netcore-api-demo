using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TodoApiLatest.Controllers.v2
{
    [ApiVersion("2.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Obsolete(message: "This endpoint is Obsolete and No longer supported")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Todos from api v2";
        }
    }
}
