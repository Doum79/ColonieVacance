using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    [Table("stay")]
    public class Stay
    {
        public const string ACTIVE_STATUS = "ACTIVATED";
        public const string EXPIRE_STATUS = "EXPIRED";
        public const int POPULAR = 5;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { set; get; }

        [Column("structure_id")]
        public int? StructureId { set; get; }
        public Structure Structure { get; set; }

        [Column("title")]
        public string Title { set; get; }

        [Column("min_year")]
        public int? MinYear { set; get; }

        [Column("max_year")]
        public int? MaxYear { set; get; }

        [Column("abstract")]
        public string Abstract { set; get; }

        [Column("description")]
        public string Description { set; get; }

        [Column("program")]
        public string Program { set; get; }

        [Column("created_date")]
        public DateTime CreatedDate { set; get; }

        [Column("view_count")]
        public int? ViewCount { set; get; }

        [Column("housing")]
        public string Housing { set; get; }

        [Column("more_informations")]
        public string MoreInformations { set; get; }

        [Column("street")]
        public string Street { set; get; }

        [Column("post_code")]
        public string PostCode { set; get; }

        [Column("city")]
        public string City { set; get; }

        [Column("state")]
        public string State { set; get; }

        [Column("country")]
        public string Country { set; get; }

        [Column("latitude")]
        public string Latitude { set; get; }

        [Column("longitude")]
        public string Longitude { set; get; }

        [Column("phone")]
        public int? Phone { set; get; }

        public List<StayTeam> PartnersList { set; get; }
        public List<StayEquipment> EquipmentsList { set; get; }
        public List<StayAccess> AccessesList { set; get; }
        public List<StayPicture> PicturesList { set; get; }
        public List<StayFurtherInformation> FurtherInformationsList { set; get; }
        public List<StayThematic> ThematicsList { set; get; }
        public List<StayActivity> ActivitiesList { set; get; }

        //[Column("name")]
        //public string Name { set; get; }
        //[Column("description")]
        //public string Description { set; get; }
        //[Column("start_date")]
        //public DateTime? StartDate { set; get; }
        //[Column("end_date")]
        //public DateTime? EndDate { set; get; }
        //[Column("structure_id")]
        //public int? StructureId { set; get; }
        //[ForeignKey("StructureId")]
        //public Structure Structure { set; get; }
        //[Column("abstract")]
        //public string Abstract { set; get; }
        //[Column("activities")]
        //public string Activities { set; get; }
        //[Column("street")]
        //public string Street { set; get; }
        //[Column("city")]
        //public string City { set; get; }
        //[Column("zip_code")]
        //public int? ZipCode { set; get; }
        //[Column("department")]
        //public string Department { set; get; }
        //[Column("region")]
        //public string Region { set; get; }
        //[Column("country")]
        //public string Country { set; get; }
        //[Column("nb_places")]
        //public int? NbPlaces { set; get; }
        //[Column("other_informations")]
        //public string OtherInformations { set; get; }
        //[Column("picture1")]
        //public string Picture1 { set; get; }
        //[Column("picture2")]
        //public string Picture2 { set; get; }
        //[Column("picture3")]
        //public string Picture3 { set; get; }
        //[Column("picture4")]
        //public string Picture4 { set; get; }
        //[Column("picture5")]
        //public string Picture5 { set; get; }
        //[Column("picture6")]
        //public string Picture6 { set; get; }
        //[Column("picture7")]
        //public string Picture7 { set; get; }
        //[Column("price")]
        //public double? Price { set; get; }
        //[Column("pratic_level")]
        //public string PraticLevel { set; get; }
        //[Column("longitude")]
        //public double? Longitude { set; get; }
        //[Column("latitude")]
        //public double? Latitude { set; get; }
        //[Column("nb_view")]
        //public int? NbView { set; get; }
        //[Column("status")]
        //public string Status { set; get; }
        //[Column("new_price")]
        //public double? NewPrice { set; get; }
        //[Column("min_year")]
        //public int? MinYear { set; get; }
        //[Column("max_year")]
        //public int? MaxYear { set; get; }
    }
}
