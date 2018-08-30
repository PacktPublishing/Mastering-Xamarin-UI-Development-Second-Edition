// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace SlidingTiles
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView gameBoardView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel gameTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton resetGameButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton shuffleButton { get; set; }

        [Action ("ResetGame_Clicked:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ResetGame_Clicked (UIKit.UIButton sender);

        [Action ("ShuffleBoardTiles_Clicked:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ShuffleBoardTiles_Clicked (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (gameBoardView != null) {
                gameBoardView.Dispose ();
                gameBoardView = null;
            }

            if (gameTitle != null) {
                gameTitle.Dispose ();
                gameTitle = null;
            }

            if (resetGameButton != null) {
                resetGameButton.Dispose ();
                resetGameButton = null;
            }

            if (shuffleButton != null) {
                shuffleButton.Dispose ();
                shuffleButton = null;
            }
        }
    }
}