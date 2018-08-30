//
//  App.xaml.cs
//  Main class that gets called whenever our PlanetaryApp is started
//
//  Created by Steven F. Daniel on 17/02/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
// 
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PlanetaryApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

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
