﻿using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppName5.Views;

namespace AppName5
{
	public partial class App : PrismApplication
	{
		protected override void OnInitialized()
		{
			InitializeComponent();

			NavigationService.NavigateAsync("NavigationPage/MainPage?title=Hello%20from%20Xamarin.Forms");
		}

		protected override void RegisterTypes()
		{
			Container.RegisterTypeForNavigation<NavigationPage>();
			Container.RegisterTypeForNavigation<MainPage>();
		}
	}
}
