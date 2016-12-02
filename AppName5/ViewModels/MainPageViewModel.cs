using System;
using Prism.Mvvm;
using Reactive.Bindings;
//using AppName5.Models;

namespace AppName5.ViewModels
{
	public class MainPageViewModel : BindableBase
	{
		// ObservableCollection
		public ReactiveCollection<MyItem> MyItems { get; } = new ReactiveCollection<MyItem>();


		public MainPageViewModel()
		{
			MyItems.Add(new MyItem { Text = "text1", Note = "note1", Image="mine1.png" });
			MyItems.Add(new MyItem { Text = "text2", Note = "note2", Image="mine2.png" });
			MyItems.Add(new MyItem { Text = "text3", Note = "note3", Image="mine3.png" });
}
	}
}
