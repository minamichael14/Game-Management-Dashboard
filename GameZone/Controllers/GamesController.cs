using GameZone.Models;
using GameZone.Services;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext DbContext;
        private readonly ICategoriesService categoriesService;
        private readonly IDevicesService devicesService;
        private readonly IGameService gamesService;

        public GamesController(ApplicationDbContext dbContext 
            , ICategoriesService categoriesService
            , IDevicesService devicesService
            , IGameService gamesService)
        {
            DbContext = dbContext;
            this.categoriesService = categoriesService;
            this.devicesService = devicesService;
            this.gamesService = gamesService;
        }
        public IActionResult Index()
        {
           var games = gamesService.GetAll();
            return View(games);
        }

        public IActionResult Create()
        {
            CreateGameFormViewModel ViewModel = new()
            {
                Categories = categoriesService.GetSelectList(),
                Devices = devicesService.GetSelectList()
            };
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel game)
        {
            if (!ModelState.IsValid)
            {
                game.Categories = categoriesService.GetSelectList();
                game.Devices = devicesService.GetSelectList();
                return View(game);
            }
            else
            {
                await gamesService.Create(game);
                
                return RedirectToAction("Index");
                
            }
        }
        
        public IActionResult Details(int id)
        {
            var game = gamesService.GetById(id);
            if(game is null)
            {
                return NotFound();
            }
            return View(game);
        }
       
        public IActionResult Edit(int id)
        {
            var game = gamesService.GetById(id);
            if (game is null)
            {
                return NotFound();
            }
            else
            {
                var newGame = new EditGameFormViewModel
                {
                    Id = game.Id,
                    Name = game.Name,
                    Description = game.Description,
                    CategorieId = game.CategorieId,
                    SelectedDevices = game.devices.Select(g => g.DeviceId).ToList(),
                    Categories = categoriesService.GetSelectList(),
                    Devices = devicesService.GetSelectList(),
                    CurrentCover = game.Cover
                };
                
                return View(newGame);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel game)
        {
            if (!ModelState.IsValid)
            {
                game.Categories = categoriesService.GetSelectList();
                game.Devices = devicesService.GetSelectList();
                return View(game);
            }

            var newGame = await gamesService.Edit(game);
            if(newGame is null)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int id)
        {
            bool isDeleted = gamesService.Delete(id);

            return isDeleted? RedirectToAction("index") : BadRequest();
        }
    }
}
