using System;
using System.Reactive.Linq;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;
//using AppName5.Models;

namespace AppName5.ViewModels
{
	public class MainPageViewModel : BindableBase
	{
		// ObservableCollection
		public ReactiveCollection<MyItem> MyItems { get; } = new ReactiveCollection<MyItem>();

		public ReactiveProperty<MyItem> SelectedItem { get; } = new ReactiveProperty<MyItem>();

		public MainPageViewModel(INavigationService navigationService)
		{
			MyItems.Add(new MyItem { Text = "text1", Note = "note1", Image = "mine1.png" });
			MyItems.Add(new MyItem { Text = "text2", Note = "note2", Image = "mine2.png" });
			MyItems.Add(new MyItem { Text = "text3", Note = "note3", Image = "mine3.png" });


			// アイテムタップ時の動作を定義 Where= System.Reactive.Linq;
			SelectedItem.Where(item => item != null)
						.Subscribe(async item =>
						{
							// これが選択を解除する処理
							// これやらないと、同じところを連続で選ぶことができないよー
							SelectedItem.Value = null;

							var parameters = new NavigationParameters();
							parameters["item"] = item;
							await navigationService.NavigateAsync("DetailPage", parameters);
						});

		}
	}
}
