﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;


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

    }
}