using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi
{
    public interface IRepository
    {
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player> Create(Player player);
        Task<Player> Modify(Guid id, ModifiedPlayer player);
        Task<Player> Delete(Guid id);

        //Item stuff
        Task<Item> CreateItem(Guid playerId, Item item);
        Task<Item> GetItem(Guid playerId, Guid itemId);
        Task<Item[]> GetAllItems(Guid playerId);
        Task<Item> UpdateItem(Guid playerId, Item item);
        Task<Item> DeleteItem(Guid playerId, Item item);
        Task<Player[]> GetPlayersWithXscore(int minScore);
        Task<UpdateResult> PushItem(Guid id, Item item);
        Task<UpdateResult> IncrementScore(Guid id, int score_add);
        Task<UpdateResult> ChangePlayerName(Guid id, string name);
        Task<Player[]> GetPlayersWithNumItems(int itemAmount);
        Task<Player[]> GetPlayersWithTag(string tag);
        Task<Player> GetPlayerWithName(string name);

        Task <Player[]>GetBestPlayers();
    }

}
