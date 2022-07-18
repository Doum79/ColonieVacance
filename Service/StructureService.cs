using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using Model.Exceptions;
using Provider.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class StructureService
    {
        private readonly ILogger<StructureService> logger;
        private readonly Context context;
        private readonly IConfiguration config;
        private readonly HashPasswordService hashPasswordService;
        private readonly MailService mailService;

        public StructureService(ILogger<StructureService> logger, Context context, IConfiguration config,
            HashPasswordService hashPasswordService, MailService mailService)
        {
            this.logger = logger;
            this.context = context;
            this.config = config;
            this.hashPasswordService = hashPasswordService;
            this.mailService = mailService;
        }
        public async Task<Structure> GetStructureByEmail(string email)
        {
            return await context.Structure.FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task<Structure> GetStructureById(int structureId)
        {
            return await context.Structure.FirstOrDefaultAsync(s => s.Id == structureId);
        }

        public async Task<List<Structure>> ListStructure()
        {
            return await context.Structure.ToListAsync();
        }

        public async Task<Structure> UpdateStructure(Structure uStructure)
        {
            if (uStructure == null || uStructure.Id == null || uStructure.Email == null)
                throw new ArgumentNullException();

            await CheckExistEmail(uStructure);

            var structure = await context.Structure.FirstOrDefaultAsync(u => u.Id == uStructure.Id);

            structure.City = uStructure.City;
            structure.Email = uStructure.Email;
            structure.Phone = uStructure.Phone.HasValue ? uStructure.Phone.Value : default(int?);
            structure.Street = uStructure.Street;
            structure.PostCode = uStructure.PostCode;
            structure.Country = uStructure.Country;
            structure.Longitude = uStructure.Longitude;
            structure.Latitude = uStructure.Latitude;
            structure.Department = uStructure.Department;
            structure.State = uStructure.State;
            structure.Siret = uStructure.Siret;
            structure.Name = uStructure.Name;

            await context.SaveChangesAsync();

            return await GetStructureById(structure.Id.Value);
        }

        public async Task DeleteStructure(int? structureId)
        {
            if (structureId == null)
                throw new ArgumentNullException();

            var structure = await GetStructureById(structureId.Value);

            context.Structure.Remove(structure);
            await context.SaveChangesAsync();
        }

        public async Task<List<Structure>> FavoriteStructuresListByUser(int? userId)
        {
            if (userId == null)
                throw new ArgumentNullException();

            var list = await context.UserFavoriteStructure.Include(us => us.Structure).Where(us => us.UserId == userId.Value).AsNoTracking().ToListAsync();
            return list.Select(fs => fs.Structure).ToList();
        }

        public async Task DeleteFavoriteStructure(int? structureId, int? userId)
        {
            if (!userId.HasValue || !structureId.HasValue)
                throw new ArgumentNullException();

            var favoriteStructure = await context.UserFavoriteStructure.FirstOrDefaultAsync(fs => fs.UserId == userId.Value && fs.StructureId == structureId.Value);
            context.UserFavoriteStructure.Remove(favoriteStructure);
            await context.SaveChangesAsync();
        }

        public async Task DeleteFavoriteStructureList(int? userId)
        {
            if (!userId.HasValue)
                throw new ArgumentNullException();

            var list = await FavoriteStructuresListByUser(userId.Value);
            list.ForEach(async s =>
            {
                await DeleteFavoriteStructure(userId.Value, s.Id);
            });
        }

        public async Task<UserFavoriteStructure> AddFavoriteStructure(int? structureId, int? userId)
        {
            if (!userId.HasValue || !structureId.HasValue)
                throw new ArgumentNullException();

            var checkFavorite = await context.UserFavoriteStructure.FirstOrDefaultAsync(ufs => ufs.UserId == userId && ufs.StructureId == structureId);

            if (checkFavorite != null)
                return new UserFavoriteStructure();

            var newFavoriteStructure = new UserFavoriteStructure
            {
                StructureId = structureId.Value,
                UserId = userId.Value
            };

            context.UserFavoriteStructure.Add(newFavoriteStructure);
            await context.SaveChangesAsync();

            return newFavoriteStructure;
        }

        public async Task<Structure> Registration(Structure newStructure)
        {
            await CheckExistEmail(newStructure);
            await CheckExistSiret(newStructure);
            CheckPassword(newStructure);
            CheckStructure(newStructure);

            var structureRegistration = new Structure
            {
                Name = newStructure.Name,
                Street = newStructure.Street,
                City = newStructure.City,
                PostCode = newStructure.PostCode,
                Department = newStructure.Department,
                State = newStructure.State,
                Country = newStructure.Country,
                Phone = newStructure.Phone,
                Email = newStructure.Email,
                Siret = newStructure.Siret,
                Longitude = newStructure.Longitude,
                Latitude = newStructure.Latitude,
                Password = UtilsFunctionsService.SetHashPassword(newStructure.Password),
                Profil = Structure.PROFIL_STRUCTURE,
            };

            context.Structure.Add(structureRegistration);
            await context.SaveChangesAsync();

            await mailService.Registration(newStructure.Email);
            return structureRegistration;
        }

        private async Task CheckExistEmail(Structure str)
        {
            if (str.Email == null)
                throw new NoStructureEmailException();

            var user = await context.User.FirstOrDefaultAsync(u => u.Email == str.Email);

            if(user != null)
                throw new EmailExistingException();

            var structure = await GetStructureByEmail(str.Email);
            if (structure != null && structure.Id != str.Id)
                throw new EmailExistingException();
        }

        private async Task CheckExistSiret(Structure str)
        {
            if (string.IsNullOrEmpty(str.Siret))
                throw new NoStructureSiretException();

            var structure = await context.Structure.FirstOrDefaultAsync(s => s.Siret == str.Siret);

            if (structure != null && structure.Id != str.Id)
                throw new StructureSiretExistException();
        }

        private void CheckStructure(Structure str)
        {
            if (string.IsNullOrEmpty(str.Name))
                throw new NoStructureNameException();

            if (string.IsNullOrEmpty(str.Street))
                throw new NoStructureStreetException();

            if (string.IsNullOrEmpty(str.Country))
                throw new NoStructureCountryException();

            if (string.IsNullOrEmpty(str.PostCode))
                throw new NoStructureZipCodeException();

            if (string.IsNullOrEmpty(str.City))
                throw new NoStructureCityException();

            if (string.IsNullOrEmpty(str.Country))
                throw new NoStructureCountryException();

            if(string.IsNullOrEmpty(str.Siret))
                throw new NoStructureSiretException();
        }

        private void CheckPassword(Structure structure)
        {
            if(string.IsNullOrEmpty(structure.Password)
                || string.IsNullOrEmpty (structure.ConfirmPassword)
                || string.IsNullOrWhiteSpace(structure.Password)
                || string.IsNullOrWhiteSpace(structure.ConfirmPassword)
                || structure.Password != structure.ConfirmPassword)
                throw new InvalidPasswordException();
        }
    }
}
