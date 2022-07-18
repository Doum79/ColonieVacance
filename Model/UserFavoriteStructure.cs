using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("user_favorite_structure")]
    public class UserFavoriteStructure
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { set; get; }

        [Column("user_id")]
        public int? UserId { set; get; }
        public User User { set; get; }
        [Column("structure_id")]
        public int? StructureId { set; get; }
        public Structure Structure { set; get; }
    }
}
