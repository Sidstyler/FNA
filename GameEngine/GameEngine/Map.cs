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
			mWallImage = TextureLoader.Load( "pixel", content );
		}

		public Rectangle CheckCollision( Rectangle inRectangle )
		{
			for ( int i = 0 ; i < mWalls.Count ; i++ )
			{
				if( mWalls[ i ] != null && mWalls[ i ].mWall.Intersects( inRectangle ) == true )
				{
					return mWalls[ i ].mWall;
				}
			}

			return Rectangle.Empty;
		}

		public void DrawWalls( SpriteBatch spriteBatch )
		{
			for ( int i = 0 ; i < mWalls.Count ; i++ )
			{
				if( mWalls[ i ] != null && mWalls[ i ].mActive == true )
				{
					Vector2 position = new Vector2( mWalls[ i ].mWall.X, mWalls[ i ].mWall.Y );
					spriteBatch.Draw( mWallImage, position, mWalls[ i ].mWall, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.7f );
				}

			}
		}
	}

	public class Wall
	{
		public Rectangle mWall;
		public bool mActive = true;

		public Wall()
		{
		}

		public Wall( Rectangle inputRectangle )
		{
			mWall = inputRectangle;
		}
	}
}
