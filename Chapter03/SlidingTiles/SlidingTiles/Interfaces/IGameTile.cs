//
//  IGameTile.cs
//  Interface class for the GameTile class
//
//  Created by Steven F. Daniel on 24/04/2018.
//  Copyright © 2018 GENIESOFT STUDIOS. All rights reserved.
//
using UIKit;

namespace SlidingTiles.Interfaces
{
    public interface IGameTile
    {
        UIImage DrawTileText(UIImage uiImage, string sText, UIColor textColor, int iFontSize);
    }
}
