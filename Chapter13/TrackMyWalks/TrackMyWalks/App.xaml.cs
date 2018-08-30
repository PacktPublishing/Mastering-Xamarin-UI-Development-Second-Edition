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
using TrackMyWalks.Services;
using TrackMyWalks.ViewModels;
using System;
using System.Threading.Tasks;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TrackMyWalks
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Initialise and create an instance of our navigation service class
            NavService = DependencyService.Get<INavigationService>() as NavigationService;
        }

        protected override void OnStart()
        {
            // Check what Target OS Platform we are running on whenever the app starts
            if (Device.RuntimePlatform.Equals(Device.Android))
            {
                // Set the Root Page for our application
                MainPage = new NavigationPage(new SplashPage());
            }
            else
            {
                // Set the Root Page and update the NavigationBar color for our app
                MainPage = new NavigationPage(new WalksMainPage())
                {
                    BarBackgroundColor = Color.IndianRed,
                    BarTextColor = Color.White,
                };
            }

            // Set the current main page to our Navigation Service
            NavService.XFNavigation = MainPage.Navigation;

            // Register each of our View Models on our Navigation Stack
            NavService.RegisterViewMapping(typeof(WalksMainPageViewModel), typeof(WalksMainPage));
            NavService.RegisterViewMapping(typeof(WalkEntryPageViewModel), typeof(WalkEntryPage));
            NavService.RegisterViewMapping(typeof(WalkTrailInfoPageViewModel), typeof(WalkTrailInfoPage));
            NavService.RegisterViewMapping(typeof(WalkDistancePageViewModel), typeof(WalkDistancePage));
            NavService.RegisterViewMapping(typeof(TwitterSignInPageViewModel), typeof(TwitterSignInPage));
        }

        // Declare our SelectedItem property that will store our Walk Trail details
        public static WalkDataModel SelectedItem { get; set; }

        // Declare our NavService property that will be used to navigate between ViewModels
        public static NavigationService NavService { get; set; }

        #region Twitter Sign In Page Property and Instance methods to remove and Navigate (Android Only)

        // Action property method to remove our TwitterSignInPage from the NavigationStack
        public static Action RemoveTwitterSignInPage => new Action(() => NavService.XFNavigation.PopAsync());

        // Navigate to our WalksMainPage, once we have successfully signed in
        public async static Task NavigateToWalksMainPage()
        {
            await NavService.XFNavigation.PushAsync(new WalksMainPage());
        }
        #endregion

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