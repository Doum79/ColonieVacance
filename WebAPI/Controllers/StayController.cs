using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using Newtonsoft.Json;
using Service;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StayController : DefaultController
    {
        private readonly string secret;
        private readonly StayService stayService;
        private readonly UserService userService;
        private readonly StructureService structureService;
        private readonly DocumentService documentService;

        public StayController(IConfiguration conf, StayService stayService, UserService userService, StructureService structureService,
            DocumentService documentService) : base(userService, structureService)
        {
            secret = conf["ApplicationSettings:Secret"];
            this.stayService = stayService;
            this.documentService = documentService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> StayList()
        {
            return Ok(await stayService.StayList());
        }

        [HttpGet("structure/{id}")]
        public async Task<IActionResult> StayListByStructure([FromRoute] int id)
        {
            Structure structure = await GetAuthenticatedStructure();
            if (!IsAuthorizedStructure(structure))
                return Forbid();

            return Ok(await stayService.StayListByStructure(id));
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> StayListByUser([FromRoute] int id)
        {
            User user = await GetAuthenticatedUser();
            Structure structure = await GetAuthenticatedStructure();
            if (!IsAuthorizedUser(user) && !IsAuthorizedStructure(structure))
                return Forbid();

            return Ok(await stayService.FavoriteStaysListByUser(id));
        }

        [AllowAnonymous]
        [HttpGet("{stayId}")]
        public async Task<IActionResult> GetStay([FromRoute] int stayId)
        {
            return Ok(await stayService.GetStay(stayId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStay()
        {
            Structure structure = await GetAuthenticatedStructure();
            if (!IsAuthorizedStructure(structure))
                return Forbid();

            var stay = JsonConvert.DeserializeObject<Stay>(Request.Form["newStay"].Last());
            stay.PicturesList = new List<StayPicture>();

            IFormFileCollection files = HttpContext.Request.Form.Files;
            for (int i = 0; i < files.Count; i++)
            {
                if (files[i] != null && files[i].Length > 0)
                {
                    var stayPicture = new StayPicture();
                    byte[] fileBytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        files[i].CopyTo(memoryStream);
                        fileBytes = memoryStream.ToArray();
                    }

                    Stream stream = new MemoryStream(fileBytes);
                    stayPicture.PictureName = files[i].FileName;
                    stayPicture.PictureUrl = await documentService.UploadSharedFile(stream, files[i].FileName);
                    stay.PicturesList.Add(stayPicture);
                }
            }
            return Ok(await stayService.CreateStay(stay, structure));
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateStay([FromBody] DTO.StayConfig stayConfig)
        {
            Structure structure = await GetAuthenticatedStructure();
            if (!IsAuthorizedStructure(structure))
                return Forbid();

            return Ok(await stayService.UpdateStay(stayConfig.Stay, structure, stayConfig.Activities, stayConfig.Thematics));
        }

        [HttpPost("removeList")]
        public async Task<ActionResult> DeleteStay([FromBody] List<Stay> stays)
        {
            Structure structure = await GetAuthenticatedStructure();
            if (!IsAuthorizedStructure(structure))
                return Forbid();

            await stayService.DeleteStaysList(stays, structure);
            return Ok();
        }

        [HttpPost("favorite/{stayId}")]
        public async Task<IActionResult> AddFavoriteStay([FromRoute] int stayId)
        {
            User user = await GetAuthenticatedUser();
            Structure structure = await GetAuthenticatedStructure();
            if (!IsAuthorizedUser(user) && !IsAuthorizedStructure(structure))
                return Forbid();

            await stayService.AddFavoriteStay(stayId, user.Id);

            return Ok();
        }

        [HttpDelete("favorite/{stayId}")]
        public async Task<IActionResult> DeleteFavoriteStay([FromRoute] int stayId)
        {
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedUser(user))
                return Forbid();

            await stayService.DeleteFavoriteStay(stayId, user.Id);

            return Ok();
        }

        [HttpGet("favorite")]
        public async Task<IActionResult> FavoriteStaysList([FromRoute] int stayId)
        {
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedUser(user))
                return Forbid();

            return Ok(await stayService.FavoriteStaysListByUser(user.Id));
        }

        [AllowAnonymous]
        [HttpPost("filters")]
        public async Task<IActionResult> FilterStaysList([FromBody] DTO.StayFilter filters)
        {
            return Ok(await stayService.FilterStaysList(filters.wordText, filters.StartDate, filters.StayCity,
                filters.Thematics, filters.YearList, filters.DurationList, filters.Activities));
        }

        [AllowAnonymous]
        [HttpGet("populars")]
        public async Task<IActionResult> GetPopularStays()
        {
            return Ok(await stayService.GetPopularStays());
        }

        //[AllowAnonymous]
        //[HttpGet("lastMinutes")]
        //public async Task<IActionResult> GetLastMinutesStays()
        //{
        //    return Ok(await stayService.GetLastMinutesStays());
        //}
    }
}
