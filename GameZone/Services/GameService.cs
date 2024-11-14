
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameZone.Services
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private readonly string ImagesPath;

        public GameService(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            DbContext = dbContext;
            WebHostEnvironment = webHostEnvironment;
            ImagesPath = $"{WebHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        }

        public IEnumerable<Game> GetAll()
        {
            return DbContext.Games
                .Include(g=>g.Category)
                .Include(g=>g.devices)
                .ThenInclude(d=>d.devices)
                .ToList();
        }

        public Game? GetById(int id)
        {
            return DbContext.Games
                .Include(g => g.Category)
                .Include(g => g.devices)
                .ThenInclude(d => d.devices)
                .FirstOrDefault(g => g.Id == id);
        }
        public async Task Create(CreateGameFormViewModel gameModel)
        {
            String coverName = await SaveCover(gameModel.Cover);

            var game = new Game()
            {
                Name = gameModel.Name,
                Description = gameModel.Description,
                CategorieId = gameModel.CategorieId,
                devices = gameModel.SelectedDevices.Select(D => new GameDevice { DeviceId = D }).ToList(),  //  ICollection<GameDevice> = list<int>
                Cover = coverName
            };

            DbContext.Add(game);
            DbContext.SaveChanges();
        }

        public async Task<Game?> Edit(EditGameFormViewModel newGame)
        {
            var game = DbContext.Games
                .Include(g=> g.devices)
                .FirstOrDefault(g=>g.Id == newGame.Id);
            if (game is null)
            {
                return null;
            }

            var hasNewCover = newGame.Cover is not null;

            game.Name = newGame.Name;
            game.Description = newGame.Description;
            game.CategorieId = newGame.CategorieId;
            game.devices = newGame.SelectedDevices.Select(D => new GameDevice { DeviceId = D }).ToList(); //  ICollection<GameDevice> = list<int>

            //In condition the cover has been changed.
            if (hasNewCover)
            {
                // delete the old cover
                var oldCoverPath = Path.Combine(ImagesPath, game.Cover);
                File.Delete(oldCoverPath);
                // save the new cover
                var coverName = await SaveCover(newGame.Cover);
                // save cover name in database
                game.Cover = coverName;
            }

            DbContext.SaveChanges();
            return game;
        }

        private async Task<String> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(ImagesPath, coverName);

            // To save the cover image to the wwwroot (inside the server)
            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;

            var game = DbContext.Games.Find(id);
            if (game is not null)
            {
                DbContext.Remove(game);
                var affectedRows = DbContext.SaveChanges();
                if (affectedRows > 0)
                {
                    isDeleted = true;
                    // delete the old cover
                    var deletedCoverPath = Path.Combine(ImagesPath, game.Cover);
                    File.Delete(deletedCoverPath);
                }
            }

            return isDeleted;
        }
    }
}

