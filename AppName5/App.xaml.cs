using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppName5.Views;
using System;
using System.Diagnostics;

namespace AppName5
{
	public partial class App : PrismApplication
	{
		public static UserDetails User;

		protected override void OnInitialized()
		{
			InitializeComponent();

			NavigationService.NavigateAsync("NavigationPage/MainPage?title=Hello%20from%20Xamarin.Forms");
		}

		protected override void RegisterTypes()
		{
			Container.RegisterTypeForNavigation<NavigationPage>();
			Container.RegisterTypeForNavigation<MainPage>();
			Container.RegisterTypeForNavigation<DetailPage>();
			Container.RegisterTypeForNavigation<LoginPage>();
		}

		public static Action SuccessfulLoginAction
		{
			get
			{
				return new Action(() =>
				{
					Debug.WriteLine("Action だってー");
				});
			}
		}
	}
}
