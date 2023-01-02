using System;
using MangaApp.Services;
using MangaApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows;

namespace MangaApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();
            MainPage = new MangaApp.AppShell();
            //MainPage = new NavigationPage(new LoginPage2());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
