//
//  CreateNewTrailDetails.cs
//  Automated UI Testing to validate signing into Twitter and create a new Walk Trail Entry
//
//  Created by Steven F. Daniel on 14/08/2018
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;

namespace TrackMyWalks.UITests
{
    // Set this attribute to indicate which platforms you would like to test i.e., iOS and Android
    [TestFixture(Platform.iOS)]
    public class CreateNewTrailDetails
    {
        // IApp interface is responsible for handling the communication with the app
        IApp app;

        // Platform parameter is responsible for indicating on which platform Xamarin should launch
        Platform platform;

        string entryCellPlatformClassName;

        // This is the class constructor for the CreateNewTrailDetails with setting for the platform
        public CreateNewTrailDetails(Platform platform)
        {
            this.platform = platform;
            entryCellPlatformClassName = (this.platform == Platform.iOS ? "UITextField" : "EntryCellEditText");
        }

        // The BeforeEachTest instance method is setup before each test is launched - app object is initialised
        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        // The AppLaunches instance method REPL console is invoked (with REPL we are able to test
        // our app manually and all actions will be displayed within the app screen
        [Test]
        public void AppLaunches()
        {
            app.Repl();    
        }

        // Create the CreateBrandNewTrailEntry Test to create a new Trail Entry
        [Test]         public void CreateBrandNewTrailEntry()         {
            // Sign in to Twitter (If using Two-Factor Authentication, you’ll need to comment this out)
            //  HandleTwitterSigningIn();
             // Wait for the Track My Walks Listing to appear by checking the navigation bar title             var navigationBarTitle = "Track My Walks Listing";             var mainScreen = app.WaitForElement(x => x.Marked(navigationBarTitle).Class("UINavigationBar"));              // Validate to ensure that our Track My Walks Listing screen was displayed             Assert.IsTrue(mainScreen.Any(), navigationBarTitle + " screen wasn't shown after signing in.");              // Click on the Add button from our Track My Walks Listing screen             app.Tap(x => x.Marked("Add"));             var WalkEntryPageScreenTitle = "Adding Trail Details";             var WalkEntryPageScreen = app.WaitForElement(x => x.Marked(WalkEntryPageScreenTitle).Class("UINavigationBar"));              // Validate to ensure that our Adding Trail Details screen was displayed             Assert.IsTrue(WalkEntryPageScreen.Any(), WalkEntryPageScreenTitle + " screen wasn't shown after tapping the Add button."); 
            // Populate our Adding Trail Details EntryCell Fields
            PopulateWalkEntryDetailsForm();

            // Tap on the Save button to save the details and exit
            app.Tap(x => x.Marked("Save"));
            var SaveWalkEntryDialogTitle = "Save Walk Entry Item";
            var SaveWalkEntryDialogScreen = app.WaitForElement(x => x.Marked(SaveWalkEntryDialogTitle));
            app.Tap(x => x.Marked("OK"));

            // Validate to ensure that our Save Walk Entry Item Details screen was displayed
            Assert.IsTrue(SaveWalkEntryDialogScreen.Any(), navigationBarTitle + " screen wasn't shown after tapping the Save button.");
        }          // Instance method to handle populating the Walk Entry Details Form         public void PopulateWalkEntryDetailsForm()         {
            // Clear the default text entry for our Title EntryCell             app.ClearText(x => x.Class(entryCellPlatformClassName).Index(0));             app.EnterText(x => x.Class(entryCellPlatformClassName).Index(0), "New UITest Walk Entry");             app.DismissKeyboard();              // Enter in some default text for our Description EntryCell
            app.ClearText(x => x.Class(entryCellPlatformClassName).Index(1));
            app.EnterText(x => x.Class(entryCellPlatformClassName).Index(1), "This is a new description entry, using the UITest automation features");             app.DismissKeyboard(); 
            // Enter in some default text for our Distance EntryCell
            app.ClearText(x => x.Class(entryCellPlatformClassName).Index(4));
            app.EnterText(x => x.Class(entryCellPlatformClassName).Index(4), "256");
            app.DismissKeyboard();
             // Enter in some default text for our Image URL
            app.ClearText(x => x.Class(entryCellPlatformClassName).Index(6));
            app.EnterText(x => x.Class(entryCellPlatformClassName).Index(6), "https://heuft.com/upload/image/400x267/no_image_placeholder.png");             app.DismissKeyboard();         }          // Instance methods that will handle signing into Twitter         public  void HandleTwitterSigningIn()         {             // Set up and initialise our Twitter Credentials             var TwitterUsername = "YOUR_TWITTER_USERNAME";             var TwitterPassword = "YOUR_TWITTER_PASSWORD";

            // Enter values for our username and password within the WebView
            app.Tap(x => x.WebView().Css("[id=username_or_email]"));
            app.EnterText(x => x.WebView().Css("[id=username_or_email]"), TwitterUsername);
            app.DismissKeyboard();
            app.Tap(x => x.WebView().Css("[id=password]"));
            app.EnterText(x => x.WebView().Css("[id=password]"), TwitterPassword);
            app.DismissKeyboard();

            // Tap the Authorize app button in the WebView use id=cancel for Cancel button
            app.ScrollDownTo(x => x.WebView().Css("[id=allow]"));             app.Tap(x => x.WebView().Css("[id=allow]"));         }     } }