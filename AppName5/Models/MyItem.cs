using System;
using Prism.Mvvm;

namespace AppName5
{
	public class MyItem : BindableBase
	{
		private string text;
		public string Text
		{
			get { return text; }
			set { SetProperty(ref text, value); }
		}

		private string note;
		public string Note
		{
			get { return note; }
			set { SetProperty(ref note, value); }
		}

		private string image;
		public string Image {
			get { return image;}
			set { SetProperty(ref image, value);}
		}

		public MyItem()
		{
			
		}
	}
}
