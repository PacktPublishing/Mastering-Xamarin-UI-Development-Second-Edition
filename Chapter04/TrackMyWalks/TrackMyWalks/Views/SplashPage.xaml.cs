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

            // Create a new navigation page, using the WalksMainPage
            Application.Current.MainPage = new NavigationPage(new WalksMainPage());
        }
    }
}