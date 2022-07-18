using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    [Table("user")]
    public class User
    {
        public const string PROFIL_PARENT = "PARENT";

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { set; get; }
        [Column("email")]
        public string Email { set; get; }
        [Column("password")]
        public string Password { set; get; }
        [Column("profil")]
        public string Profil { set; get; }
        [Column("gender")]
        public bool? Gender { set; get; }
        [Column("last_name")]
        public string LastName { set; get; }
        [Column("first_name")]
        public string FirstName { set; get; }
        [Column("street")]
        public string Street { set; get; }
        [Column("city")]
        public string City { set; get; }
        [Column("zip_code")]
        public int? ZipCode { set; get; }
        [Column("country")]
        public string Country { set; get; }
        [Column("phone_number")]
        public int? PhoneNumber { set; get; }

        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
}
