using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class DefaultController : ControllerBase
    {
        private readonly UserService userService;
        private readonly StructureService structureService;

        public DefaultController(UserService userService, StructureService structureService)
        {
            this.userService = userService;
            this.structureService = structureService;
        }

        protected async Task<User> GetAuthenticatedUser()
        {

            User user = null;
            if (User.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
            {
                string email = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                user = await userService.GetUserByEmail(email);
            }

            return user;
        }

        protected async Task<Structure> GetAuthenticatedStructure()
        {

            Structure structure = null;
            if (User.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
            {
                string email = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                structure = await structureService.GetStructureByEmail(email);
            }

            return structure;
        }

        protected static bool IsAuthorizedUser(User user)
        {
            return user != null;
        }

        protected static bool IsAuthorizedStructure(Structure structure)
        {
            return structure != null;
        }
    }
}
