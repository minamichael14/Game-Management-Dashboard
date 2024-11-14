using System.ComponentModel.DataAnnotations.Schema;

namespace GameZone.Models
{
    public class GameDevice
    {
        [ForeignKey("games")]
        public int GameId { get; set; }
        [ForeignKey("devices")]
        public int DeviceId { get; set; }

        public virtual Game games { get; set; } = default!;
        public virtual Device? devices { get; set; }
    }
}