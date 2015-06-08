using System;
using SQLite;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Tasky
{
	public class ResoponseToDatabase : SQLiteConnection
	{
		static object locker = new object ();
		public static string DatabaseFilePath {
			get { 
				var sqliteFilename = "TaskyDB.db3";
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "../Library/"); // Library folder
				var path = Path.Combine (libraryPath, sqliteFilename);
				return path;
			}
		}
		public ResoponseToDatabase (string path) : base (path)
		{
			CreateTable<Task> ();
		}

		public IEnumerable<Task> GetTasks () 
		{
			lock (locker) {
				return (from i in Table<Task> () select i).ToList ();
			}
		}

		public Task GetTask (int id)
		{
			lock (locker) {
				return Table<Task>().FirstOrDefault(x => x.Id == id);
			}
		}

		public int SaveTask (Task item) 
		{
			lock (locker) {
				if (item.Id != 0) {
					
					return Update (item);
				} else {
					return Insert (item);
				}
			}
		}
			
		public int DeleteTask(Task task) 
		{
			lock (locker) {
				return Delete<Task> (task.Id);
			}
		}
	}
}

