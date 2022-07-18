using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StayThematicController : DefaultController
    {
        private readonly string secret;
        private readonly StayThematicService stayThematicService;
        private readonly UserService userService;
        private readonly StructureService structureService;

        public StayThematicController(IConfiguration conf, StayThematicService stayThematicService, UserService userService, StructureService structureService) : base(userService, structureService)
        {
            secret = conf["ApplicationSettings:Secret"];
            this.stayThematicService = stayThematicService;
        }

        [AllowAnonymous]
        [HttpGet("stay/{stayId}")]
        public async Task<ActionResult> ListThematic([FromRoute] int stayId)
        {
            return Ok(await stayThematicService.ListThematicOfStay(stayId));
        }
    }
}
