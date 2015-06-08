using System;
using System.Collections.Generic;
using System.Linq;


namespace Tasky
{
	public class TaskDb
	{
		public static List<String> ListStrOfDate = new List<String> ();
		public static List<List<Task>> ListOfTasks = new List<List<Task>>();
		public static List<Task> tempTasks = new List<Task>();


		public TaskDb ()
		{
			
			tempTasks = AppDelegate.DB.GetTasks ().ToList ();
			ListOfTasks.Clear ();
			ListStrOfDate.Clear ();
			GetMore (tempTasks, 30);


		}
		public static bool GetMore(List<Task> list, int cellOfPage){
			if (cellOfPage >= list.Count) {

				cellOfPage = list.Count;
				for (var i = 0; i < cellOfPage; i++) {
					sortedDate (list [list.Count - i - 1]);
				}
				DataSource.shouldLoadMoreFlag = false;
				Console.WriteLine("................return false");
				return false;

			} else {

				for (var i = 0; i < cellOfPage; i++) {
					sortedDate (list [list.Count - i - 1]);
				}
				tempTasks = tempTasks.GetRange (0, list.Count - cellOfPage + 1);
				DataSource.shouldLoadMoreFlag = true;
				Console.WriteLine("................return true");

				return true;
			}



		}


		public static void sortedDate(Task ThisTask){

			var strDate = ThisTask.DateCreated.ToShortDateString ();

			// kiem tra xem co date chua
			var i = 0;
			while (i < ListStrOfDate.Count) {
				if (ListStrOfDate [i] == strDate) {
					break;
				}
				i++;

			}

			if (i == ListStrOfDate.Count){
				
				ListStrOfDate.Add (strDate);
				var tmpList = new List<Task>();
				tmpList.Add(ThisTask);
				ListOfTasks.Add(tmpList);
			} else {
				ListOfTasks[i].Add(ThisTask);
			}


		}

		public static void Insert(Task thisTask) {
			if (thisTask.DateCreated == DateTime.MinValue){
				thisTask.DateCreated = DateTime.Now;
				Console.WriteLine (thisTask.DateCreated.ToShortDateString ());
			}

			AppDelegate.DB.SaveTask (thisTask);
		}
		public static int DelRow(Task thisTask){
			for (var i = 0; i < ListOfTasks.Count; i++) {
				Console.WriteLine (i);
				foreach (var item in ListOfTasks[i]) {
					Console.WriteLine ("{0}  / {1}",item.Id,thisTask.Id);
					if (item.Id == thisTask.Id) {

						ListOfTasks [i].Remove (item);
						AppDelegate.DB.DeleteTask (thisTask);
						if (ListOfTasks [i].Count == 0) {
							return i;
						}
						break;
							
					}
				}
			}

			return -1;

		}
		public static void DelSection (int section){
			if (section == -1)
				return;
			ListOfTasks.RemoveAt(section);
			ListStrOfDate.RemoveAt (section);
		}
		public static void DeleteTask (Task thisTask) {
			var section = DelRow (thisTask);
			DelSection (section);
		}
	}
}

