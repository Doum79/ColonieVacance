using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("stay_thematic")]
    public class StayThematic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { set; get; }

        [Column("stay_id")]
        public int? StayId { set; get; }
        public Stay Stay { set; get; }

        [Column("thematic_id")]
        public int? ThematicId { set; get; }
        public Thematic Thematic { set; get; }
    }
}
