using System;
using UIKit;
using Foundation;
using System.Collections.Generic ;

namespace Tasky
{
	class DataSource : UITableViewSource
	{
		static readonly NSString CellIdentifier = new NSString ("Cell");
		private MasterViewController controller;
		public static bool shouldLoadMore;
		public static bool shouldLoadMoreSize;
		public static bool shouldLoadMoreFlag;
		public DataSource (MasterViewController controller)
		{
			shouldLoadMore = true;
			this.controller = controller;
		}



		// Customize the number of sections in the table view.
		public override nint NumberOfSections (UITableView tableView)
		{
			return TaskDb.ListStrOfDate.Count;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			var index = Convert.ToInt32 (section);
				return TaskDb.ListOfTasks[index].Count;
		}
		public override string TitleForHeader (UITableView tableView, nint section)
		{
			var index = Convert.ToInt32 (section);

			return TaskDb.ListStrOfDate[index];
		}

		// Customize the appearance of table view cells.
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (CellIdentifier, indexPath);

			var thisTask = TaskDb.ListOfTasks [indexPath.Section] [indexPath.Row];

			cell.TextLabel.Text = thisTask.Id.ToString() +". " + thisTask.Name;

			//auto hight
			cell.TextLabel.LineBreakMode = UILineBreakMode.TailTruncation;
			cell.TextLabel.Lines = 0;

			// set checkbox
			if (thisTask.Done)
				cell.Accessory = UITableViewCellAccessory.Checkmark;
			else
				cell.Accessory = UITableViewCellAccessory.None;

			var index = FindIndex (indexPath);
			var size = ListSize (TaskDb.ListOfTasks);

			if (index < size * 0.8) {
				shouldLoadMoreSize = true;
			} else
				shouldLoadMoreSize = false;
			shouldLoadMore = shouldLoadMoreSize && shouldLoadMoreFlag;
			Console.WriteLine ("  {2} -   {0}    /  {1}", index, size * 0.8,shouldLoadMore);


			if (shouldLoadMore && size > 20 && index >= size * 0.8 - 1) {
				Console.WriteLine ("dang chay load more");
				shouldLoadMore = false;
				LoadMore ();

			}



			return cell;
		}

		public async void LoadMore(){
			
			 TaskDb.GetMore (TaskDb.tempTasks, 10);

			controller.TableView.ReloadData ();
		}

		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			// Return false if you do not want the specified item to be editable.
			return true;
		}
		private int ListSize(List<List<Task>> list){
			var result = 0;
			for (var i = 0; i < list.Count ; i++ ){
				result += list [i].Count;
			}
			return result;
		}


		private int FindIndex (NSIndexPath path){
			var result = 0;
			for (var i = 0; i < path.Section; i++) {
				result += TaskDb.ListOfTasks [i].Count;
			}
			result += path.Row;
			return result;

		}
		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if (editingStyle == UITableViewCellEditingStyle.Delete) {
				// Delete the row from the data source.
				var thisTask = TaskDb.ListOfTasks [indexPath.Section] [indexPath.Row];

				var section = TaskDb.DelRow (thisTask);
				Console.WriteLine (section);
				controller.TableView.DeleteRows (new [] { indexPath }, UITableViewRowAnimation.Fade);


				if (section != -1) {

					TaskDb.DelSection (section);
					controller.TableView.DeleteSections (NSIndexSet.FromIndex(indexPath.Section), UITableViewRowAnimation.Fade);
				}

				} else if (editingStyle == UITableViewCellEditingStyle.Insert) {
				// Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
				}
			}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
//			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
//				controller.DetailViewController.SetDetailItem (objects [indexPath.Row]);
		}
	}
}

