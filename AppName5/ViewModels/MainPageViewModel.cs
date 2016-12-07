using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;
using Xamarin.Forms;
//using AppName5.Models;

namespace AppName5.ViewModels
{
	public class MainPageViewModel : BindableBase
	{
		// ObservableCollection
		// ListViewのデータ
		public ReactiveCollection<MyItem> MyItems { get; private set; } = new ReactiveCollection<MyItem>();

		// ListView.IsRefreshingと同期させるプロパティ
		private bool isRefreshing;
		public bool IsRefreshing
		{
			get { return isRefreshing; }
			set
			{
				if (value == isRefreshing)
					return;
				isRefreshing = value;
				OnPropertyChanged();
			}
		}

		// ListViewを引っ張った時に実行させるコマンド
		public ICommand RefreshCommand
		{
			get;
			private set;
		}


		public ReactiveProperty<MyItem> SelectedItem { get; } = new ReactiveProperty<MyItem>();

		public MainPageViewModel(INavigationService navigationService)
		{
			MyItems.Add(new MyItem { Text = "text1", Note = "note1", Image = "mine1.png" });
			MyItems.Add(new MyItem { Text = "text2", Note = "note2", Image = "mine2.png" });
			MyItems.Add(new MyItem { Text = "text3", Note = "note3", Image = "mine3.png" });

			//var red = new Label { Text = "Red", BackgroundColor = Color.Transparent };

			var random = new Random(140);

			RefreshCommand = new Command(async (nothing) =>
			{
				MyItems.Clear();

				var cout = random.Next(1, 30);
				Debug.WriteLine("cout = " + cout);

				// ランダムに更新 MyItems.Count
				for (var i = 0; i < cout; i++)
				{
					await Task.Delay(100);
					//MyItems[i] = new MyItem { 
					//	Text = "text" + random.Next().ToString(),
					//	Note = "note" + random.Next().ToString(),
					//	Image = "mine" + random.Next(1,4).ToString() + ".png" };

					var item = new MyItem
					{
						Text = "text" + (i + 1),
						Note = "note" + (i + 1),
						Image = "mine" + random.Next(1, 4).ToString() + ".png"
					};

					MyItems.Add(item);
				}

				// Binding機構経由でListViewのIsRefreshingプロパティも変更する
				IsRefreshing = false;
			},
				// ICommand.CanExecuteにもバインドしたプロパティを利用できる
				(nothing) => !IsRefreshing
			);


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
