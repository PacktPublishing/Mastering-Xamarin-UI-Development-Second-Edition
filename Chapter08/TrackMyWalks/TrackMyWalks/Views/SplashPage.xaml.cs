//
//  SplashPage.xaml.cs
//  Displays a timed splash screen for the TrackMyWalks application
//
//  Created by Steven F. Daniel on 14/05/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TrackMyWalks.Views
{
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Set a wait delay of 3 seconds on our Splash Screen
            await Task.Delay(3000);

            // Update the Main Page and update the NavigationBar color for our app
            Application.Current.MainPage = new NavigationPage(new WalksMainPage())
            {
                BarBackgroundColor = Color.CadetBlue,
                BarTextColor = Color.White,
            };
            // Update the Application's Navigation Service property
            App.NavService.XFNavigation = Application.Current.MainPage.Navigation;
        }
    }
}