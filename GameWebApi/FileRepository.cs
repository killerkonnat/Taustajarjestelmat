using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using MongoDB.Driver;

namespace GameWebApi
{
    public class ListClass
    {
        public List<Player> p_list = new List<Player>();
    }

    public class FileRepository : IRepository
    {

        string path = @"c:\temp\game-dev.txt";

        public async Task<Player> Create(Player player)
        {
            ListClass players = await ReadFile();
            players.p_list.Add(player);
            File.WriteAllText(path, JsonConvert.SerializeObject(players));

            return player;
        }

        public async Task<Player> Delete(Guid pid)
        {
            ListClass players = await ReadFile();

            for (int i = 0; i < players.p_list.Count; i++)
            {
                if (players.p_list[i].Id == pid)

                {
                    players.p_list.RemoveAt(i);
                    File.WriteAllText(path, JsonConvert.SerializeObject(players));
                    return null;
                }
            }


            return null;
        }

        public async Task<Player> Get(Guid pid)
        {
            ListClass players = await ReadFile();
            var thisPlayer= new Player();
            foreach (var p in players.p_list)
            {
                if (p.Id == pid)
                {
                    thisPlayer = p;
                    break;
                }
            }
            return thisPlayer;
        }

        public async Task<Player[]> GetAll()
        {

            ListClass players = await ReadFile();
            return players.p_list.ToArray();
        }

        public async Task<Player> Modify(Guid pid, ModifiedPlayer player)
        {
            ListClass players = await ReadFile();
            var result = new Player();

            foreach (var p in players.p_list)
            {
                if (p.Id == pid)
                {
                    p.Score = player.Score;
                    File.WriteAllText(path, JsonConvert.SerializeObject(players));
                    break;
                }
            }
            return result;
        }

        public async Task<ListClass> ReadFile()
        {
            var players = new ListClass();
            string json = await File.ReadAllTextAsync(path);
 

            if (File.ReadAllText(path).Length != 0)
            {
                return JsonConvert.DeserializeObject<ListClass>(json);
            }

            return players;
        }

        public void WriteFile(String text)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(text));
        }

        //Dummy functions to get rid of build errors from IRepository changes
        public Task<Item> CreateItem(Guid playerId, Item item) { return null; }
        public  Task<Item> GetItem(Guid playerId, Guid itemId) { return null; }
        public Task<Item[]> GetAllItems(Guid playerId) { return null; }
        public  Task<Item> UpdateItem(Guid playerId, Item item) { return null; }
        public  Task<Item> DeleteItem(Guid playerId, Item item) { return null; }

        public Task<Player[]> GetPlayersWithXscore(int minScore) { return null;}
        public Task<UpdateResult> PushItem(Guid id, Item item) { return null; }
        public Task<UpdateResult> IncrementScore(Guid id, int score_add) { return null; }
        public Task<UpdateResult> ChangePlayerName(Guid id, string name) { return null; }
        public Task<Player[]> GetPlayersWithNumItems(int itemAmount) { return null; }
        public Task<Player[]> GetPlayersWithTag(string tag) { return null; }
        public Task<Player> GetPlayerWithName(string name) { return null; }

        public Task<Player[]> GetBestPlayers() { return null; }

    }

 
}
