using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameZone.Models
{
    public class Game : BaseEntity
    {
        public string Description { get; set; } = String.Empty;
        [MaxLength(500)]
        public string Cover { get; set; } = String.Empty;

        [ForeignKey("Category")]
        public int CategorieId { get; set; }
        public virtual Category? Category { get; set; }

        public virtual ICollection<GameDevice>? devices { get; set; } =new List<GameDevice>();
    }


}
