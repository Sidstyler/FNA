using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameEngine
{
	public class Map
	{
		public List<Wall> mWalls = new List<Wall>();
		Texture2D mWallImage;

		public int mMapWidth = 15;
		public int mMapHeight = 9;
		public int mTileSize = 128;

		public void Load( ContentManager content )
		{
			//mWallImage = TextureLoader.Load( "pixel", content );
		}
	}

	public class Wall
	{
		public Rectangle mWall;
		public bool mActive;

		public Wall()
		{
		}

		public Wall( Rectangle inputRectangle )
		{
			mWall = inputRectangle;
		}
	}
}
