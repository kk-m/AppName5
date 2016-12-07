using System;
using AppName5.Controls;
using AppName5.iOS.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace AppName5.iOS.Controls
{
	public class MyEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null) {
				// あなたがここでUITextFieldにしたいことを何でもしてください！
				Control.BackgroundColor = UIColor.FromRGB(255, 204, 204);
				Control.BorderStyle = UITextBorderStyle.RoundedRect;
			}
		}
	}
}
