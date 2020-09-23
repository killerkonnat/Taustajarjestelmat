using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;


namespace GameWebApi
{
    public class MongoDbRepository : IRepository
    {
        private readonly IMongoCollection<Player> _playerCollection;
        private readonly IMongoCollection<BsonDocument> _bsonDocumentCollection;

        public MongoDbRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("game");
            _playerCollection = database.GetCollection<Player>("players");

            _bsonDocumentCollection = database.GetCollection<BsonDocument>("players");
        }

        public async Task<Player> Create(Player player)
        {
            await _playerCollection.InsertOneAsync(player);
            return player;
        }

        public async Task<Player> Delete(Guid id)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            return await _playerCollection.FindOneAndDeleteAsync(filter);
        }

        public async Task<Player> Get(Guid id)
        {
            var filter = Builders<Player>.Filter.Eq(player => player.Id, id);
            return await _playerCollection.Find(filter).FirstAsync();
        }

        public async Task<Player[]> GetAll()
        {
            var players = await _playerCollection.Find(new BsonDocument()).ToListAsync();
            return players.ToArray();
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            Player returnPlayer = await _playerCollection.Find(filter).FirstAsync();
            returnPlayer.Score = player.Score;
            await _playerCollection.ReplaceOneAsync(filter, returnPlayer);
            return returnPlayer;
        }

        public async Task<Item> CreateItem(Guid playerId, Item item)
        {
            Player player = await Get(playerId);
            if (player == null)
            {
                throw new NotFoundException();
            }
            else
            {
                player.itemList.Add(item);
                var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
                await _playerCollection.ReplaceOneAsync(filter, player);
                return item;
            }
        }
        public async Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            Player player = await Get(playerId);
            //var filter = Builders<Item>.Filter.Eq(item => item.Id, itemId);

            for (int i = 0; i < player.itemList.Count; i++)
            {
                if (player.itemList[i].Id == itemId)
                    return player.itemList[i];
            }

            return null;
        }
        public async Task<Item[]> GetAllItems(Guid playerId)
        {
            Player player = await Get(playerId);
            return player.itemList.ToArray();
        }

        public async Task<Item> UpdateItem(Guid playerId, Item item)
        {
            Player player = await Get(playerId);

            foreach (var i in player.itemList)
            {
                if (i.Id == item.Id)
                {
                    i.Level = item.Level;
                    var filter_player = Builders<Player>.Filter.Eq(player => player.Id, playerId);
                    await _playerCollection.ReplaceOneAsync(filter_player, player);
                    return i;
                }
            }

            return null;
        }
        public async Task<Item> DeleteItem(Guid playerId, Item item)
        {
            Player player = await Get(playerId);

            for (int i = 0; i < player.itemList.Count; i++)
            {
                if (player.itemList[i].Id == item.Id)
                {
                    player.itemList.RemoveAt(i);
                    var filter_player = Builders<Player>.Filter.Eq(player => player.Id, playerId);
                    await _playerCollection.ReplaceOneAsync(filter_player, player);
                    return item;
                }
            }

            return null;
        }
    }
}
