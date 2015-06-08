using System;
using System.Collections.Generic;

using UIKit;
using Foundation;
using CoreGraphics;

namespace Tasky
{
	public partial class MasterViewController : UITableViewController
	{
		public DetailViewController DetailViewController { get; set; }

		DataSource dataSource;
		TaskDb taskDb;

		public MasterViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("Master", "Master");
			
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad) {
				PreferredContentSize = new CGSize (320f, 600f);
				ClearsSelectionOnViewWillAppear = false;
			}
		}

		public override void ViewWillAppear(bool animate){
			base.ViewWillAppear (animate);
			taskDb = new TaskDb ();
			TableView.ReloadData ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.
			NavigationItem.LeftBarButtonItem = EditButtonItem;

			var addButton = new UIBarButtonItem (UIBarButtonSystemItem.Add, AddNewItem);
			addButton.AccessibilityLabel = "addButton";
			NavigationItem.RightBarButtonItem = addButton;

			DetailViewController = (DetailViewController)((UINavigationController)SplitViewController.ViewControllers [1]).TopViewController;

			TableView.Source = dataSource = new DataSource (this);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		void AddNewItem (object sender, EventArgs args)
		{
			var newTask = new Task ();

			DetailViewController.SetDetailItem (newTask);
			NavigationController.PushViewController (DetailViewController, true);


		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "showDetail") {
				var indexPath = TableView.IndexPathForSelectedRow;
				var item = TaskDb.ListOfTasks [indexPath.Section][indexPath.Row];
				var controller = (DetailViewController)((UINavigationController)segue.DestinationViewController).TopViewController;
				controller.SetDetailItem (item);
				controller.NavigationItem.LeftBarButtonItem = SplitViewController.DisplayModeButtonItem;
				controller.NavigationItem.LeftItemsSupplementBackButton = true;
			}
		}


}

}
