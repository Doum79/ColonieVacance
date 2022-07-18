using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("stay_equipment")]
    public class StayEquipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { set; get; }
        [Column("stay_id")]
        public int? StayId { set; get; }
        public Stay Stay { set; get; }

        [Column("label")]
        public string Label { set; get; }

        [Column("is_included")]
        public bool? IsIncluded { set; get; }
    }
}
