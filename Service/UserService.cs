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
    public class UserService
    {
        private readonly ILogger<UserService> logger;
        private readonly Context context;
        private readonly IConfiguration config;
        private readonly HashPasswordService hashPasswordService;
        private readonly StayService stayService;
        private readonly StructureService structureService;
        private readonly MailService mailService;

        public UserService(ILogger<UserService> logger, Context context, IConfiguration config, HashPasswordService hashPasswordService,
           StayService stayService, StructureService structureService, MailService mailService)
        {
            this.logger = logger;
            this.context = context;
            this.config = config;
            this.hashPasswordService = hashPasswordService;
            this.stayService = stayService;
            this.structureService = structureService;
            this.mailService = mailService;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await context.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserById(int userId)
        {
            return await context.User.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<List<User>> ListUsers()
        {
            return await context.User.ToListAsync();
        }

        public async Task<User> UpdateUser(User uUser)
        {
            if (uUser == null || uUser.Id == null || uUser.Email == null)
                throw new ArgumentNullException();

            await CheckExistEmail(uUser);

            var user = await GetUserById(uUser.Id.Value);

            user.City = uUser.City;
            user.Email = uUser.Email;
            user.FirstName = uUser.FirstName;
            user.Gender = uUser.Gender.HasValue ? uUser.Gender.Value : default(bool?);
            user.LastName = uUser.LastName;
            user.PhoneNumber = uUser.PhoneNumber.HasValue ? uUser.PhoneNumber.Value : default(int?);
            user.Street = uUser.Street;
            user.ZipCode = uUser.ZipCode.HasValue ? uUser.ZipCode.Value : default(int?);
            user.Country = uUser.Country;

            await context.SaveChangesAsync();

            return await GetUserById(user.Id.Value);
        }

        public async Task DeleteUser(int? userId)
        {
            if (userId == null)
                throw new ArgumentNullException();

            await stayService.DeleteFavoriteStaysList(userId);
            await structureService.DeleteFavoriteStructureList(userId);
            var user = await GetUserById(userId.Value);

            context.User.Remove(user);
            await context.SaveChangesAsync();
        }

        public async Task<User> Registration(User newUser)
        {
            await CheckExistEmail(newUser);
            CheckPassword(newUser);
            CheckUser(newUser);

            var userRegistration = new User
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Password = UtilsFunctionsService.SetHashPassword(newUser.Password),
                Profil = User.PROFIL_PARENT
            };

            context.User.Add(userRegistration);
            await context.SaveChangesAsync();

            await mailService.Registration(newUser.Email);
            return userRegistration;
        }

        private async Task CheckExistEmail(User usr)
        {
            if (string.IsNullOrEmpty(usr.Email))
                throw new NoUserEmailException();

            var structure = await context.Structure.FirstOrDefaultAsync(u => u.Email == usr.Email);

            if(structure != null)
                throw new EmailExistingException();

            var user = await GetUserByEmail(usr.Email);
            if (user != null && user.Id != usr.Id)
                throw new EmailExistingException();
        }

        private void CheckUser(User usr)
        {
            if (string.IsNullOrEmpty(usr.FirstName))
                throw new NoUserFirstNameException();

            if (string.IsNullOrEmpty(usr.LastName))
                throw new NoUserLastNameException();

            //if (string.IsNullOrEmpty(usr.Street))
            //    throw new NoUserStreetException();

            //if (usr.ZipCode == null)
            //    throw new NoUserZipCodeException();

            //if (string.IsNullOrEmpty(usr.City))
            //    throw new NoUserCityException();

            //if (string.IsNullOrEmpty(usr.Country))
            //    throw new NoUserCountryException();
        }

        private void CheckPassword(User user)
        {
            if (string.IsNullOrEmpty(user.Password)
                || string.IsNullOrEmpty(user.ConfirmPassword)
                || string.IsNullOrWhiteSpace(user.Password)
                || string.IsNullOrWhiteSpace(user.ConfirmPassword)
                || user.Password != user.ConfirmPassword)
                throw new InvalidPasswordException();
        }
    }
}
