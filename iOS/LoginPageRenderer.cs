using System;
using AppName5;
using AppName5.iOS;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.Diagnostics;
using AppName5.Views;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace AppName5.iOS
{
	public class LoginPageRenderer : PageRenderer
	{
		bool showLogin = true;
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			if (showLogin && App.User == null)
			{
				//Twitter with OAuth1
				var auth = new OAuth1Authenticator(
					consumerKey: "Add your consumer key from Twitter",
					consumerSecret: "Add your consumer secrete from Twitter",
					requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
					authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
					accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
					callbackUrl: new Uri("http://twitter.com")
				);

				auth.Completed += (sender, eventArgs) =>
				{
					DismissViewController(true, null);

					if (eventArgs.IsAuthenticated)
					{
						// success
						var account = eventArgs.Account;
						Debug.WriteLine("--------------------");
						Debug.WriteLine("account = " + account);
						Debug.WriteLine("account.Username = " + account.Username);


						App.User = new UserDetails();
						App.User.Token = eventArgs.Account.Properties["oauth_token"];
						App.User.TokenSecret = eventArgs.Account.Properties["oauth_token_secret"];
						App.User.TwitterId = eventArgs.Account.Properties["user_id"];
						App.User.Name = eventArgs.Account.Properties["screen_name"];
						Debug.WriteLine("--------------------");
						Debug.WriteLine("App.User.Token = " + App.User.Token);
						Debug.WriteLine("App.User.TokenSecret = " + App.User.TokenSecret);
						Debug.WriteLine("App.User.TwitterId = " + App.User.TwitterId);
						Debug.WriteLine("App.User.Name = " + App.User.Name);

						//Store details for future use, 
						//so we don't have to prompt authentication screen everytime
						AccountStore.Create().Save(eventArgs.Account, "Twitter");

						App.SuccessfulLoginAction.Invoke();
					}
				};

				PresentViewController(auth.GetUI(), true, null);
			}
		}
	}
}
