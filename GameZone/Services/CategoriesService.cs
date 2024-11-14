namespace GameZone.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext DbContext;

        public CategoriesService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return DbContext.Categories
                            .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                            .OrderBy(c => c.Text)
                            .AsNoTracking()
                            .ToList();
        }
    }
}
