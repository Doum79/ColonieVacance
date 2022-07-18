using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO
{
    public class User
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public string Password { set; get; }
        public string Profil { set; get; }
        public bool? Gender { set; get; }
        public string LastName { set; get; }
        public string FirstName { set; get; }
        public string Street { set; get; }
        public string City { set; get; }
        public int? ZipCode { set; get; }
        public int? PhoneNumber { set; get; }
    }
}
