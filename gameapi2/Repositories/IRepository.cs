using System;
using System.Threading.Tasks;
using gameapi.Models;

namespace gameapi.Repositories
{
  //All logic related to data-access happens on the classes that implement this
  public interface IRepository
  {
    Task<Player> CreatePlayer(Player player); 
    Task<Player> GetPlayer(Guid playerId); 
    Task<Player[]> GetAllPlayers(); 
    Task<Player> UpdatePlayer(Player player); 
    Task<Player> DeletePlayer(Guid playerId);
    Task<Item> CreateItem(Guid playerId, Item item); 
    Task<Item> GetItem(Guid playerId, Guid itemId); 
    Task<Item[]> GetAllItems(Guid playerId); 
    Task<Item> UpdateItem(Guid playerId, Item item); 
    Task<Item> DeleteItem(Guid playerId, Item item);
  }
}