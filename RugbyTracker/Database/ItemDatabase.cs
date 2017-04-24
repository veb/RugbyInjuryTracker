using System;
using SQLite;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RugbyTracker
{
	public class RugbyTeam
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
	}

	public class RugbyPlayer
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int TeamId { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
	}

	public class RugbyItem
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int PlayerId { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }
	}

	public class ItemDatabase
	{
		/*
		 * Optimise this file... lots of repeated methods
		 * 
		 */

		readonly SQLiteAsyncConnection database;

		public ItemDatabase(string dbPath)
		{
			database = new SQLiteAsyncConnection(dbPath);
			database.CreateTableAsync<RugbyTeam>();
			database.CreateTableAsync<RugbyPlayer>();
			database.CreateTableAsync<RugbyItem>();
		}

		public Task<List<RugbyItem>> GetItems()
		{
			return database.Table<RugbyItem>().ToListAsync();
		}

		public Task<List<RugbyTeam>> GetTeams()
		{
			return database.Table<RugbyTeam>().ToListAsync();
		}

		public Task<List<RugbyPlayer>> GetPlayers(int teamId)
		{
			return database.QueryAsync<RugbyPlayer>("SELECT * FROM [RugbyPlayer] WHERE [TeamId] = ?", teamId);
		}

		public Task<List<RugbyPlayer>> PlayerItemExists(int playerId)
		{
			return database.QueryAsync<RugbyPlayer>("SELECT * FROM [RugbyItem] WHERE [PlayerId] = ?", playerId);
		}

		public Task<List<RugbyItem>> GetItemsNotDone()
		{
			return database.QueryAsync<RugbyItem>("SELECT * FROM [RugbyItem] WHERE [Done] = 0");
		}

		public Task<List<RugbyItem>> GetItems(int playerID)
		{
			return database.QueryAsync<RugbyItem>("SELECT * FROM [RugbyItem] WHERE [PlayerId] = ?", playerID);
		}

		public Task<RugbyItem> GetItem(int id)
		{
			return database.Table<RugbyItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
		}

		public Task<int> SaveItem(RugbyItem item)
		{
			if (item.ID != 0)
			{
				return database.UpdateAsync(item);
			}
			else
			{
				return database.InsertAsync(item);
			}
		}

		public Task<int> SaveTeam(RugbyTeam item)
		{
			if (item.ID != 0)
			{
				return database.UpdateAsync(item);
			}
			else
			{
				return database.InsertAsync(item);
			}
		}

		public Task<int> SavePlayer(RugbyPlayer item)
		{
			if (item.ID != 0)
			{
				return database.UpdateAsync(item);
			}
			else
			{
				return database.InsertAsync(item);
			}
		}

		public Task<int> DeleteItem(RugbyItem item)
		{
			return database.DeleteAsync(item);
		}

		public Task<int> DeleteTeam(RugbyTeam item)
		{
			return database.DeleteAsync(item);
		}				
	}
}
