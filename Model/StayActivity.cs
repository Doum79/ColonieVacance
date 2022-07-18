using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("stay_activity")]
    public class StayActivity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { set; get; }

        [Column("stay_id")]
        public int? StayId { set; get; }
        public Stay Stay { set; get; }

        [Column("activity_id")]
        public int? ActivityId { set; get; }
        public Activity Activity { set; get; }
    }
}
