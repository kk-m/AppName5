using System;
using System.Diagnostics;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace AppName5.ViewModels
{
	public class DetailPageViewModel : BindableBase, INavigationAware
	{

		private string title;
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}

		//private string title;
		//public string Title
		//{
		//	get { return title; }
		//	set { SetProperty(ref title, value); }
		//}

		private MyItem myItem;
		public MyItem MyItem// { get; set; }
		{
			get { return myItem;}
			set { SetProperty(ref myItem, value); }
		}

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
		public string Image
		{
			get { return image; }
			set { SetProperty(ref image, value); }
		}

		public DetailPageViewModel()
		{
			// TODO: ViewModelからじゃだめなんかな？いやわからんぞー
			//var button = FindByName<StackLayout>("button");


			//StackLayout im = (StackLayout) //button;

			//Debug.WriteLine("** 2: Text = " + Text);
			//Debug.WriteLine("** 2: Note = " + Note);
			//Debug.WriteLine("** 2: Title = " + Title);
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{
			//throw new NotImplementedException();
		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			//throw new NotImplementedException();

			//int id = int.Parse(parameters["id"] as string);
			//MyItem.Value = await myItemRepository.FindFirstAsync(id);

			myItem = (AppName5.MyItem)parameters["item"];
			//Debug.WriteLine("** myItem = " + myItem.Text);

			//MyItem = (AppName5.MyItem)parameters["item"];
			//Debug.WriteLine("** Text = " + MyItem.Text);
			//Debug.WriteLine("** Note = " + MyItem.Note);
			//Debug.WriteLine("** Image = " + MyItem.Image);

			//text = item.Text;
			//note = item.Note;
			Text = myItem.Text;
			Note = myItem.Note;
			Image = myItem.Image;
			//Debug.WriteLine("** text = " + text);
			//Debug.WriteLine("** note = " + note);
			//Debug.WriteLine("** Text = " + Text);
			//Debug.WriteLine("** Note = " + Note);

			Title = "hhhhhhhhhhhhhhhhhhhhh" + " and Prism";
		}
	}
}
