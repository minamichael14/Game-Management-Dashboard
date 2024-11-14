namespace GameZone.Services
{
    public interface IGameService
    {

        IEnumerable<Game> GetAll();
        Task Create(CreateGameFormViewModel gameModel);
        
        Game? GetById(int id);

        Task<Game?> Edit(EditGameFormViewModel newGame);

        bool Delete(int  id);

        
    }
}

