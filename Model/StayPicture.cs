using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("stay_picture")]
    public class StayPicture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { set; get; }

        [Column("stay_id")]
        public int? StayId { set; get; }
        public Stay Stay { get; set; }

        [Column("picture_url")]
        public string PictureUrl { set; get; }

        [Column("picture_name")]
        public string PictureName { set; get; }
    }
}
