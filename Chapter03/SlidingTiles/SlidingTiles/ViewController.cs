//
//  ViewController.cs
//  Main game logic for the Letter Tiles Sliding game
//
//  Created by Steven F. Daniel on 24/04/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using SlidingTiles.Classes;
using UIKit;

namespace SlidingTiles
{
    public partial class ViewController : UIViewController
    {
        #region 1 - Declare our game variables for our game
        float gameViewWidth;
        float gameViewHeight;
        float tileWidth;
        float tileHeight;

        // Declare size of each of our grid cells
        int gridCellSize = 5;

        // Declare and set up our tiles array
        GameTile[,] tiles = new GameTile[5, 5];

        // Declare an array for our game tile images and game tile indexes
        List<UIImageView> gameTileImagesArray = new List<UIImageView>();
        List<CGPoint> GameTileCoords = new List<CGPoint>();

        // Declare our empty tile position
        CGPoint emptyTilePos;
        #endregion

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        #region 2 - Layout our Game Board
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            // Obtain the Width and Height for our GameBoard
            gameViewWidth = (float)gameBoardView.Frame.Size.Width;
            gameViewHeight = (float)gameBoardView.Frame.Size.Height;

            // call our method to start a new game
            startNewGame();
        }
        #endregion

        #region 3 - Instance method to create our Game Board and Game Tiles
        public void CreateGameBoard()
        {
            // Specify the Width and Heights for each of our tiles
            tileWidth = this.gameViewWidth / this.gridCellSize;
            tileHeight = this.gameViewHeight / this.gridCellSize;

            // Specify our tile width and tile centre values
            float tileCenterX = tileWidth / 2;
            float tileCenterY = tileHeight / 2;

            // Initialise our tile counter value
            int counter = 65;

            // Build our game board with images from our array
            for (int row = 0; row < gridCellSize; row++)
            {
                for (int column = 0; column < gridCellSize; column++)
                {
                    // Create a new tile by instantiating a new instance of our GameTile class
                    GameTile tile = new GameTile(row, column);
                    tile.Frame = new CGRect(0, 0, tileWidth, tileHeight);
                    tile.Image = tile.DrawTileText(UIImage.FromFile("game_tile.png"), Convert.ToChar(counter).ToString(), UIColor.White, 65);
                    tile.Center = new CGPoint(tileCenterX, tileCenterY);
                    tile.UserInteractionEnabled = true;

                    // Store our Tile Coordinates within our ArrayList object
                    GameTileCoords.Add(new CGPoint(tileCenterX, tileCenterY));

                    // Add the tile to our Tile Images
                    gameTileImagesArray.Add(tile);
                    gameBoardView.AddSubview(tile);

                    // Increment to the next tile position and image within array.
                    tileCenterX = tileCenterX + tileWidth;
                    counter = counter + 1;
                }

                tileCenterX = tileWidth / 2;
                tileCenterY = tileCenterY + tileHeight;
            }
            // Remove the last tile from the gameBoard and our gameTileImagesArray
            var emptyTile = gameTileImagesArray[gameTileImagesArray.Count - 1];
            emptyTile.RemoveFromSuperview();
            gameTileImagesArray.RemoveAt(gameTileImagesArray.Count - 1);
        }
        #endregion

        #region 4 - Instance method that will reset the current game in progress
        partial void ResetGame_Clicked(UIButton sender)
        {
            // Set up our UIAlertController as well as the Action methods
            UIApplication.SharedApplication.InvokeOnMainThread(new Action(() =>
            {
                var alert = UIAlertController.Create("Reset Game", "Are you sure you want to start again?",
                                                     UIAlertControllerStyle.Alert);
                // set up button event handlers
                alert.AddAction(UIAlertAction.Create("OK",
                                                     UIAlertActionStyle.Default, a =>
                                                     {
                                                         startNewGame();
                                                     }));
                alert.AddAction(UIAlertAction.Create("Cancel",
                                                     UIAlertActionStyle.Default,
                                                     null));

                // Display the UIAlertController to the current view
                this.ShowViewController(alert, sender);
            }));
        }
        #endregion

        #region 5 - Instance method to randomly shuffle our game tiles
        partial void ShuffleBoardTiles_Clicked(UIButton sender)
        {
            var tempGameTileCoords = new List<CGPoint>(GameTileCoords);

            foreach (UIImageView any in gameTileImagesArray)
            {
                var randGen = new Random();
                int randomIndex = randGen.Next(0, tempGameTileCoords.Count);
                any.Center = (CGPoint)tempGameTileCoords[randomIndex];
                tempGameTileCoords.RemoveAt(randomIndex);
            }
            emptyTilePos = (CGPoint)tempGameTileCoords[0];
            tempGameTileCoords.Clear();
        }
        #endregion

        #region 6 - Instance method to end the current game and start a new game
        void startNewGame()
        {
            // Remove reminants of our ImageViews from our GameBoard
            foreach (UIImageView any in gameBoardView.Subviews)
            {
                any.RemoveFromSuperview();
            }

            // Clear out our game tile arrays
            gameTileImagesArray.Clear();
            GameTileCoords.Clear();

            // Initialise our grid cell size
            gridCellSize = 5;
            CreateGameBoard();
            ShuffleBoardTiles_Clicked(shuffleButton);
        }
        #endregion

        #region 7 - Handling touch events within the Game Board
        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            if (touches.Count == 1)
            {
                try
                {
                    // Get the touch that was activated within the view
                    var myTouch = (UITouch)touches.AnyObject;
                    var touchedView = (UIImageView)myTouch.View;

                    if (gameTileImagesArray.Contains(touchedView))
                    {
                        var thisCenter = touchedView.Center;
                        UIView.Animate(.15f,
                                        () => // animation
                                        {
                                            touchedView.Center = emptyTilePos;
                                        },
                                        () => // completion
                                        {
                                            emptyTilePos = thisCenter;
                                        });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("touchedView is not a UIImageView: " + e.Message);
                }
            }
        }
        #endregion

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}