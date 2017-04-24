using System;
using Xamarin.Forms;
using RugbyTracker.Droid;
using System.IO;

[assembly: Dependency(typeof(SQLite_Android))]

namespace RugbyTracker.Droid
{
	public class SQLite_Android : ISQLite
	{
		public SQLite_Android()
		{
		}

		#region ISQLite implementation
		public SQLite.SQLiteConnection GetConnection()
		{
			var sqliteFilename = "SQLite.db3";
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var path = Path.Combine(documentsPath, sqliteFilename);

			Console.WriteLine(path);
			if (!File.Exists(path))
			{
				var s = Forms.Context.Resources.OpenRawResource(Resource.Raw.SQLite); 

				FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
				ReadWriteStream(s, writeStream);
			}

			var conn = new SQLite.SQLiteConnection(path);

			return conn;
		}
		#endregion

		void ReadWriteStream(Stream readStream, Stream writeStream)
		{
			int Length = 256;
			Byte[] buffer = new Byte[Length];
			int bytesRead = readStream.Read(buffer, 0, Length);
			// write the required bytes
			while (bytesRead > 0)
			{
				writeStream.Write(buffer, 0, bytesRead);
				bytesRead = readStream.Read(buffer, 0, Length);
			}
			readStream.Close();
			writeStream.Close();
		}
	}
}