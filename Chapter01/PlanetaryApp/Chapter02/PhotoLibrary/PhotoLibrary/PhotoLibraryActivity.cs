//
//  PhotoLibraryActivity.cs
//  Main Activity for the Photo Library Gallery PhotoLibraryUI XML representing the 
//  application user interface elements.
//
//  Created by Steven F. Daniel on 13/03/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Widget;

// Xamarin Media Plugin
using Plugin.Media;
using Plugin.Media.Abstractions;

// Xamarin Permissions Plugin
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace PhotoLibrary
{
    [Activity(Label = "PhotoLibraryActivity", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/MyCustomTheme")]
    public class PhotoLibraryActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.PhotoLibraryUI);

            // Get our chooseGallery button from the layout resource, and attach an event to it
            Button useCamera = FindViewById<Button>(Resource.Id.takePicture);
            useCamera.Click += TakePictureButton_Clicked;

            Button chooseFromGallery = FindViewById<Button>(Resource.Id.chooseFromGallery);
            chooseFromGallery.Click += ChooseFromGalleryButton_Clicked;
        }

        #region Take Picture using the Android device camera
        public async void TakePictureButton_Clicked(object sender, System.EventArgs args)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                // Display alert dialog - Device has no camera and photo support is denied
                ShowMessageDialog("Permission Denied", "Unable to gain access to the camera.");
                return;
            }
            // Check to see if we have the appropriate permissions
            if (!await Task.Run(() => CheckCameraAlbumPermissions()))
            {
                // Display alert dialog - Permission denied to Camera 
                ShowMessageDialog("Permission Denied", "Unable to gain access to the camera.");
                return;
            }

            var imageFilename = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
            {
                Name = $"{DateTime.UtcNow}.jpg",
                DefaultCamera = CameraDevice.Rear,
                PhotoSize = PhotoSize.Medium,
                SaveToAlbum = true,
            });

            if (imageFilename == null)
                return;

            // Get our chooseGallery button from the layout resource,
            // and attach an event to it
            ImageView photoImageView = FindViewById<ImageView>(Resource.Id.photoImageView);
            photoImageView.SetImageURI(Android.Net.Uri.Parse(imageFilename.Path));
        }
        #endregion

        #region Allow the user to choose a Picture from the phone
        async void ChooseFromGalleryButton_Clicked(object sender, System.EventArgs args)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                // Display our message dialog if choosing a photo is not supported
                ShowMessageDialog("Not Supported", "Choosing a photo is not supported.");
                return;
            }

            // Check to see if we have the appropriate permissions
            if (!await Task.Run(() => CheckCameraAlbumPermissions()))
            {
                // Display our message dialog if we are unable to gain access to the photo album
                ShowMessageDialog("Permission Denied", "Unable to gain access to the Photo Album.");
                return;
            }

            var imageFilename = await CrossMedia.Current.PickPhotoAsync();
            if (imageFilename != null)
            {
                ImageView photoImageView = FindViewById<ImageView>(Resource.Id.photoImageView);
                photoImageView.SetImageURI(Android.Net.Uri.Parse(imageFilename.Path));
            }
        }
        #endregion

        #region Checking for Camera and Photo Album Permissions
        public async Task<bool> CheckCameraAlbumPermissions()
        {
            // Determine if we have permission to our Camera and photo album
            var deviceCameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var deviceAlbumStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (deviceCameraStatus != PermissionStatus.Granted || deviceAlbumStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                deviceCameraStatus = results[Permission.Camera];
                deviceAlbumStatus = results[Permission.Storage];
            }
            // Check to see if we have access to the camera and photo album
            return (deviceCameraStatus == PermissionStatus.Granted && deviceAlbumStatus == PermissionStatus.Granted);
        }
        #endregion

        #region Shows a Message Dialog using the parameters specified
        public void ShowMessageDialog(string title, string message)
        {
            var dialog = new AlertDialog.Builder(this);
            var alert = dialog.Create();

            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetButton("OK", (c, ev) => CrossPermissions.Current.OpenAppSettings());
            alert.Show();
        }
    }
}