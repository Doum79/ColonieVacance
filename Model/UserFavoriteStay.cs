using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("user_favorite_stay")]
    public class UserFavoriteStay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { set; get; }

        [Column("user_id")]
        public int? UserId { set; get; }
        public User User { set; get; }

        [Column("stay_id")]
        public int? StayId { set; get; }
        public Stay Stay { set; get; }


    }
}
