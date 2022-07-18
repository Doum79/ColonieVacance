using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    [Table("structure")]
    public class Structure
    {
        public const string PROFIL_STRUCTURE = "STRUCTURE";

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
        [Column("street")]
        public string Street { set; get; }
        [Column("city")]
        public string City { set; get; }
        [Column("post_code")]
        public string PostCode { set; get; }
        [Column("department")]
        public string Department { set; get; }
        [Column("state")]
        public string State { set; get; }
        [Column("country")]
        public string Country { set; get; }
        [Column("phone")]
        public int? Phone { set; get; }
        [Column("longitude")]
        public string Longitude { set; get; }
        [Column("latitude")]
        public string Latitude { set; get; }
        [Column("siret")]
        public string Siret { set; get; }
        [Column("name")]
        public string Name { set; get; }

        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
}
