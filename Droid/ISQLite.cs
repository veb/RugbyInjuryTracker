using System;
namespace RugbyTracker.Droid
{
	public interface ISQLite
	{
		SQLite.SQLiteConnection GetConnection(); 
	}
}
