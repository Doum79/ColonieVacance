using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("stay_further_information")]
    public class StayFurtherInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { set; get; }

        [Column("stay_id")]
        public int? StayId { set; get; }
        public Stay Stay { set; get; }

        [Column("start_date")]
        public DateTime? StartDate { set; get; }

        [Column("end_date")]
        public DateTime? EndDate { set; get; }

        [Column("with_transport")]
        public bool? WithTransport { set; get; }

        [Column("start_city")]
        public string StartCity { set; get; }

        [Column("price")]
        public double? Price { set; get; }

        [Column("redirection_link")]
        public string RedirectionLink { set; get; }
    }
}
