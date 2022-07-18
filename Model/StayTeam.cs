using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("stay_team")]
    public class StayTeam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { set; get; }
        [Column("stay_id")]
        public int? StayId { set; get; }
        public Stay Stay { set; get; }

        [Column("partner_name")]
        public string PartnerName { set; get; }
    }
}
