using System;
using Android.App;
using AppName5;
using AppName5.Droid;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Diagnostics;
using AppName5.Views;
using Android.Util;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace AppName5.Droid
{
	public class LoginPageRenderer : PageRenderer
	{
		private const string CONSUMER_KEY = "Z6KGCxcIGwkbmSmta3lRW6wWd";
		private const string CONSUMER_SECRET = "PmkewQNLyOyiELDSnkwvID73TBHsi9oKu6c1ER2ORtYQK7JquT";
		bool showLogin = true;

		protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged(e);

			if (showLogin && App.User == null)
			{
				//Twitter with OAuth1
				var auth = new OAuth1Authenticator(
					//consumerKey: "Add your consumer key from Twitter",
					//consumerSecret: "Add your consumer secrete from Twitter",
					consumerKey: CONSUMER_KEY,
					consumerSecret: CONSUMER_SECRET,
					requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
					authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
					accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
					callbackUrl: new Uri("http://twitter.com")
				);

				auth.Completed += (sender, eventArgs) =>
				{
					//DismissViewController(true, null);
					System.Diagnostics.Debug.WriteLine("!!!!!");
					System.Diagnostics.Debug.WriteLine("eventArgs = " + eventArgs);
					System.Diagnostics.Debug.WriteLine("Account = " + eventArgs.Account);
					System.Diagnostics.Debug.WriteLine("Username = " + eventArgs.Account.Username);
					//var activity1 = Context as Activity;
					//activity1.Finish();

					if (eventArgs.IsAuthenticated)
					{
						// success
						var account = eventArgs.Account;
						System.Diagnostics.Debug.WriteLine("--------------------");
						System.Diagnostics.Debug.WriteLine("account = " + account);
						System.Diagnostics.Debug.WriteLine("account.Username = " + account.Username);
						//Log.Debug("kkk", "account = " + account);
						//Log.Debug("kkk", "account.Username = " + account.Username);


						App.User = new UserDetails();
						App.User.Token = eventArgs.Account.Properties["oauth_token"];
						App.User.TokenSecret = eventArgs.Account.Properties["oauth_token_secret"];
						App.User.TwitterId = eventArgs.Account.Properties["user_id"];
						App.User.Name = eventArgs.Account.Properties["screen_name"];
						System.Diagnostics.Debug.WriteLine("--------------------");
						System.Diagnostics.Debug.WriteLine("App.User.Token = " + App.User.Token);
						System.Diagnostics.Debug.WriteLine("App.User.TokenSecret = " + App.User.TokenSecret);
						System.Diagnostics.Debug.WriteLine("App.User.TwitterId = " + App.User.TwitterId);
						System.Diagnostics.Debug.WriteLine("App.User.Name = " + App.User.Name);

						//Store details for future use, 
						//so we don't have to prompt authentication screen everytime
						AccountStore.Create().Save(eventArgs.Account, "Twitter");

						App.SuccessfulLoginAction.Invoke();
					}
				};

				// Display the UI
				var activity = Context as Activity;
				activity.StartActivity(auth.GetUI(activity));
			}
		}
	}
}
