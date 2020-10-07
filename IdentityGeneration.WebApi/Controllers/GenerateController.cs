using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityGeneration.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenerateController : ControllerBase
    {
        private readonly IIdGenerator _idGenerator;

        public GenerateController(IIdGenerator idGenerator)
        {
            _idGenerator = idGenerator;
        }

        [HttpPost]
        public long Post() => _idGenerator.Generate();
    }
}
