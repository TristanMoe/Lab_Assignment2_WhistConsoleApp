using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_Assignment2_WhistPointCalculator;
using Microsoft.EntityFrameworkCore;

namespace Lab_Assignment2_WhistConsoleApp.Repositories
{
    public class ShowGameRepository
    {
        private DataContext _db;
        public ShowGameRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<List<Games>> ListGameWithPlayers()
        {
            var games = await _db.Games               
                .ToListAsync();
            return games;
        }

        public async Task<Games> GetRoundInformation(int gamesId)
        {
            var game = await _db.Games
                .Include(d => d.GameRounds)
                .ThenInclude(p => p.GRPs)
                .ThenInclude(p => p.GamePlayer)
                .ThenInclude(p => p.Player)
                .SingleAsync(p => p.GamesId == gamesId);
            return game;

        }
    }
}
