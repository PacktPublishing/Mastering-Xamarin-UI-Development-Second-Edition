//
//  GameTile.cs
//  Creates each of our tile images for our Tile Slider Game.
//
//  Created by Steven F. Daniel on 24/04/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using UIKit;
using SlidingTiles.Interfaces;
using CoreGraphics;
using System;

namespace SlidingTiles.Classes
{
	public class GameTile : UIImageView, IGameTile
	{
		// GameTile Class Constructor
		public GameTile()
		{
		}

		// Overload GameTile Class Constructor
		public GameTile(int row, int col)
		{
			this.Row = row;
			this.Col = col;
		}

		// Define the properties that will be used by our class
		public int Row { private set; get; }
		public int Col { private set; get; }

		// Instance method to draw our tile with additional text
		public UIImage DrawTileText(UIImage uiImage, string sText, UIColor textColor, int iFontSize)
		{
			nfloat fWidth = uiImage.Size.Width;
			nfloat fHeight = uiImage.Size.Height;

			CGColorSpace colorSpace = CGColorSpace.CreateDeviceRGB();

			using (CGBitmapContext ctx = new CGBitmapContext(IntPtr.Zero, (nint)fWidth, (nint)fHeight, 8, 4 * (nint)fWidth, CGColorSpace.CreateDeviceRGB(), CGImageAlphaInfo.PremultipliedFirst))
			{
				ctx.DrawImage(new CGRect(0, 0, (double)fWidth, (double)fHeight), uiImage.CGImage);
				ctx.SelectFont("HelveticaNeue-Bold", iFontSize, CGTextEncoding.MacRoman);

				// Measure the text's width - This involves drawing an invisible string to calculate the X position difference
				float start, end, textWidth;

				// Get the texts current position
				start = (float)ctx.TextPosition.X;

				// Set the drawing mode to invisible
				ctx.SetTextDrawingMode(CGTextDrawingMode.Invisible);

				// Draw the text at the current position
				ctx.ShowText(sText);

				// Get the end position
				end = (float)ctx.TextPosition.X;

				// Subtract start from end to get the text's width
				textWidth = end - start;

				nfloat fRed, fGreen, fBlue, fAlpha;

				// Set the fill color to black. This is the text color.
				textColor.GetRGBA(out fRed, out fGreen, out fBlue, out fAlpha);
				ctx.SetFillColor(fRed, fGreen, fBlue, fAlpha);

				// Set the drawing mode back to something that will actually draw Fill for example
				ctx.SetTextDrawingMode(CGTextDrawingMode.Fill);

				// Draw the text at given coords.
				ctx.ShowTextAtPoint(50, 50, sText);

				return UIImage.FromImage(ctx.ToImage());
			}
		}
	}
}