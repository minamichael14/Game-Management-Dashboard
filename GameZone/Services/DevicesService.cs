
namespace GameZone.Services
{
    public class DevicesService : IDevicesService
    {

        private readonly ApplicationDbContext DbContext;

        public DevicesService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;

        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return DbContext.Devices.Select(D => new SelectListItem { Value = D.Id.ToString(), Text = D.Name })
                            .OrderBy(D => D.Text)
                            .AsNoTracking()
                            .ToList();
        }
    }
}
