// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Tasky
{
	[Register ("DetailViewController")]
	partial class DetailViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnDel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnSave { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISwitch swDone { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField tfName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField tfNotes { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnDel != null) {
				btnDel.Dispose ();
				btnDel = null;
			}
			if (btnSave != null) {
				btnSave.Dispose ();
				btnSave = null;
			}
			if (swDone != null) {
				swDone.Dispose ();
				swDone = null;
			}
			if (tfName != null) {
				tfName.Dispose ();
				tfName = null;
			}
			if (tfNotes != null) {
				tfNotes.Dispose ();
				tfNotes = null;
			}
		}
	}
}
