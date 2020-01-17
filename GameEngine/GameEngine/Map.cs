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
        public List<Decor> mDecor = new List<Decor>();
		public List<Wall> mWalls = new List<Wall>();
		Texture2D mWallImage;

		public int mMapWidth = 15;
		public int mMapHeight = 9;
		public int mTileSize = 128;

        public void LoadMap( ContentManager content )
        {
            for( int i = 0; i < mDecor.Count; i++ )
            {
                mDecor[i].Load( content, mDecor[i].mImagePath );
            }
        }

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

        public void Update( List< GameObject > objects )
        {
            for (int i = 0; i < mDecor.Count; i++)
            {
                mDecor[i].Update( objects, this );
            }
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

    public class Decor : GameObject
    {
        public string mImagePath;
        public Rectangle mSourceRectangle;

        public string Name {  get { return mImagePath; } }

        public Decor()
        {
            mCollidable = false;
        }

        public Decor( Vector2 inPos, string imgPath, float inputDepth )
        {
            mPosition = inPos;
            mImagePath = imgPath;
            mLayerDepth = inputDepth;
            mActive = true;
            mCollidable = false;
        }

        public virtual void Load( ContentManager content, string asset )
        {
            mImage = TextureLoader.Load(asset, content );
            mImage.Name = asset;

            mBoundingBoxWidth = mImage.Width;
            mBoundingBoxHeight = mImage.Height;

            if( mSourceRectangle == Rectangle.Empty )
            {
                mSourceRectangle = new Rectangle(0, 0, mImage.Width, mImage.Height );
            }
        }

        public void SetImage( Texture2D input, string newPath )
        {
            mImage = input;
            mImagePath = newPath;

            mBoundingBoxWidth = mSourceRectangle.Width = mImage.Width;
            mBoundingBoxHeight = mSourceRectangle.Height = mImage.Height;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if( mImage != null && mActive )
            {
                spriteBatch.Draw( mImage, mPosition, mSourceRectangle, mDrawColor, mRotation, Vector2.Zero, mScale, SpriteEffects.None, mLayerDepth );
            }
        }
    }
}
