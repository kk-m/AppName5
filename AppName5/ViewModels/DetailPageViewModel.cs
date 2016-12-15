using System;
using System.Diagnostics;
using System.Windows.Input;
using CoreTweet;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;
using Xamarin.Forms;
using System.ServiceModel.Channels;
using Prism.Common;
using System.Net;
using Xamarin.Auth;

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


		private double _numDouble = 10.0;
		public double NumDouble
		{
			get { return _numDouble; }
			set { SetProperty(ref _numDouble, value); }
		}

		private int _numInt = 0;
		public int NumInt
		{
			get { return _numInt; }
			set { SetProperty(ref _numInt, value); }
		}




		public DelegateCommand TwetterLoginCommand { get; }

		public ReactiveProperty<string> NewItemsText { get; } = new ReactiveProperty<string>();
		public ReactiveCommand AddNewItem { get; private set; }

		public DelegateCommand TwetterTokenCommand { get; }


		public ReactiveProperty<string> TweetText { get; } = new ReactiveProperty<string>();
		public DelegateCommand TweetCommand { get; }

		  

		private OAuth.OAuthSession session { get; set; }
		private Tokens tokens { get; set;}


		public DelegateCommand TCommand { get; }

		public DelegateCommand XACommand { get; }



		//Consumer Key(API Key)  HkD8Bn0qGFsvpdrE1vPvSz5k6
		//Consumer Secret (API Secret)	WIrmGHPVyzcIgtoyarOI4oAjCSNn0iy5Z5w90FI51e6NqiNAF0
		//private const string CONSUMER_KEY = "HkD8Bn0qGFsvpdrE1vPvSz5k6";
		//private const string CONSUMER_SECRET = "WIrmGHPVyzcIgtoyarOI4oAjCSNn0iy5Z5w90FI51e6NqiNAF0";

		private const string CONSUMER_KEY = "Z6KGCxcIGwkbmSmta3lRW6wWd";
		private const string CONSUMER_SECRET = "PmkewQNLyOyiELDSnkwvID73TBHsi9oKu6c1ER2ORtYQK7JquT";

		public DetailPageViewModel(INavigationService navigationService)
		{
			// TODO: ViewModelからじゃだめなんかな？いやわからんぞー
			//var button = FindByName<StackLayout>("button");


			//StackLayout im = (StackLayout) //button;

			//Debug.WriteLine("** 2: Text = " + Text);
			//Debug.WriteLine("** 2: Note = " + Note);
			//Debug.WriteLine("** 2: Title = " + Title);

			//MyCommand = new DelegateCommand(async () => await navigationService.NavigateAsync("MyMyPage"));

			// ------------------------
			//var auth = new OAuth2Authenticator(
			//  Constants.ClientId,
			//  Constants.ClientSecret,
			//  Constants.Scope,
			//  new Uri(Constants.AuthorizeUrl),
			//  new Uri(Constants.RedirectUrl),
			//  new Uri(Constants.AccessTokenUrl));

			//var auth = new OAuth2Authenticator(
			//CONSUMER_KEY,
			//	CONSUMER_SECRET,
			//	"",
			//	new Uri("https://api.twitter.com/oauth/authorize"),
			//	new Uri("https://twitter.com/"),
			//	new Uri("https://api.twitter.com/oauth/access_token")
			//);

			var auth = new OAuth1Authenticator(
				//Constants.TwitterConsumerKey,
				//Constants.TwitterConsumerSecret,
				CONSUMER_KEY,
				CONSUMER_SECRET,
				new Uri("https://api.twitter.com/oauth/request_token"),
				new Uri("https://api.twitter.com/oauth/authorize"),
				new Uri("https://api.twitter.com/oauth/access_token"),
				new Uri("http://twitter.com")
			);






			TwetterLoginCommand = new DelegateCommand(async () =>
			{
				//Debug.WriteLine("* DelegateCommand *");
				// TODO: これはユーザーごとにならない
				// OAuth2Token apponly = await OAuth2.GetTokenAsync(CONSUMER_KEY, CONSUMER_SECRET);
				//Debug.WriteLine("* apponly = " + apponly);

				// Twitter アプリ連携認証ページ Uri の取得
				 session = await OAuth.AuthorizeAsync(CONSUMER_KEY, CONSUMER_SECRET);
				Debug.WriteLine("* session = " + session);
				// アプリ連携認証ページに遷移
				Device.OpenUri(new Uri(session.AuthorizeUri.AbsoluteUri));
				// TODO: Custom Url Scheme は使えませんかぁぁぁぁ　困るぅぅぅぅ



				// Token を作る。
				//Tokens.Create("consumerKey", "consumerSecret", "accessToken", "accessSecret");
			});


			TwetterTokenCommand = new DelegateCommand(async () =>
			{
				Debug.WriteLine("* TwetterTokenCommand");
				string pin = NewItemsText.Value;
				Debug.WriteLine("* pin = " + pin);
				if (!string.IsNullOrEmpty(pin) && pin.Length == 7) {
					tokens = await CoreTweet.OAuth.GetTokensAsync(session, pin);
					Debug.WriteLine("!!! tokens = " + tokens);

				}
			});

			// TODO: これって何をTweetするの？webのリンクとかあんの？
			// TODO: twitterアプリを呼び出したりできるのか？
			TweetCommand = new DelegateCommand(async () =>
			{
				Debug.WriteLine("* TweetCommand");
				string tweetText = TweetText.Value;
				Debug.WriteLine("* tweetText = " + tweetText);
				if (!string.IsNullOrEmpty(tweetText)) {
					await tokens.Statuses.UpdateAsync(status => tweetText);
				}
			});

			//this.AddNewItem = this.NewItemsText
			//	.Select(x => !string.IsNullOrWhiteSpace(x)) // NewItemsText の状況から bool に変換
			//	.ToReactiveCommand();



			TCommand = new DelegateCommand(() =>
			{
				var baseUrl = "http://twitter.com/share?text=";
				var param = "My Favorite Item by MANT\\nContent Holder is カンガルーくん";
				Debug.WriteLine("* baseUrl = " + baseUrl);
				Debug.WriteLine("* param = " + param);

				//var uuu = new Uri(baseUrl + param);
				//Debug.WriteLine("* uuu = " + uuu);


				var encodeUrl = WebUtility.UrlEncode(param);
				Debug.WriteLine("* encodeUrl = " + encodeUrl);

				// uri 作成
				var uri = new Uri(baseUrl + encodeUrl);
				Debug.WriteLine("* uri = " + uri);

				// ブラウザで開く
				Device.OpenUri(uri);
			});


			XACommand = new DelegateCommand(async () => {
				await navigationService.NavigateAsync("LoginPage");
			});

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
