using System;

using UIKit;

namespace Tasky
{
	public partial class DetailViewController : UIViewController
	{
		public Task DetailItem { get; set; }

		public DetailViewController (IntPtr handle) : base (handle)
		{
		}

		public void SetDetailItem (Task newDetailItem)
		{
			if (DetailItem != newDetailItem) {
				DetailItem = newDetailItem;
				
				// Update the view
				ConfigureView ();
			}
		}

		void ConfigureView ()
		{
			

			// Update the user interface for the detail item
			if (IsViewLoaded && DetailItem != null){
				
				tfName.Text = DetailItem.Name;
				tfNotes.Text = DetailItem.Notes;
				swDone.On = DetailItem.Done;
				btnSave.Enabled = true;
				btnDel.Enabled = true;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.NavigationItem.LeftItemsSupplementBackButton = true;
			tfName.BecomeFirstResponder ();
			// Perform any additional setup after loading the view, typically from a nib.
			ConfigureView ();
//			
//			tfName.ShouldReturn += (textField) => {
//				tfNotes.BecomeFirstResponder;
//				return true;
//			};
			var tap = new UITapGestureRecognizer();
			tap.AddTarget (() => handle (tap));
				View.AddGestureRecognizer(tap);
			btnSave.TouchDown += (sender, e) => {
				DetailItem.Name = tfName.Text;
				DetailItem.Notes = tfNotes.Text;
				DetailItem.Done = swDone.On;

				if (tfName.Text != ""){
					btnDel.Enabled = false;
				TaskDb.Insert(DetailItem);
				NavigationController.PopViewController(true);
				}

			};
			btnDel.TouchDown += (sender, e) => {
				tfName.Text = "";
				tfNotes.Text = "";
				swDone.On = false;

				btnSave.Enabled = false;
				btnDel.Enabled = false;
				TaskDb.DeleteTask(DetailItem);
				NavigationController.PopViewController(true);

					
			};

		}

				private void handle(UITapGestureRecognizer tap) {
					View.EndEditing(true);
				}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


