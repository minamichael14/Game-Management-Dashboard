namespace GameZone.Models
{
    public class Category: BaseEntity
    {
        public virtual ICollection<Game>? games { get; set; }= new List<Game>();
    }
}
