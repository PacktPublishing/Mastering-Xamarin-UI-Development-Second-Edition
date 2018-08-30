//
//  App.xaml.cs
//  Main class that gets called whenever our TrackMyWalks app is started
//
//  Created by Steven F. Daniel on 14/05/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TrackMyWalks.Views;
using TrackMyWalks.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TrackMyWalks
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            // Check what Target OS Platform we are running on whenever the app starts
            if (Device.RuntimePlatform.Equals(Device.Android))
            {
                MainPage = new SplashPage();
            }
            else
            {
                // Set the root page for our application
                MainPage = new NavigationPage(new WalksMainPage());
            }
        }

        // Declare our WalkDataModel that will store our Walk Trail details
        public static WalkDataModel SelectedItem { get; set; }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}