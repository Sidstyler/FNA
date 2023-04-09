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
        public List<Decor> decor = new List<Decor>();
		public List<Wall> walls = new List<Wall>();
		Texture2D mWallImage;

		public int mapWidth = 15;
		public int mapHeight = 9;
		public int tileSize = 128;

        public void LoadMap( ContentManager content )
        {
            for( int i = 0; i < decor.Count; i++ )
            {
                decor[i].Load( content, decor[i].imagePath );
            }
        }

		public void Load( ContentManager content )
		{
			mWallImage = TextureLoader.Load( "pixel", content );
		}

		public Rectangle CheckCollision( Rectangle inRectangle )
		{
			for ( int i = 0 ; i < walls.Count ; i++ )
			{
				if( walls[ i ] != null && walls[ i ].wall.Intersects( inRectangle ) == true )
				{
					return walls[ i ].wall;
				}
			}

			return Rectangle.Empty;
		}

        public void Update( List< GameObject > objects )
        {
            for (int i = 0; i < decor.Count; i++)
            {
                decor[i].Update( objects, this );
            }
        }

		public void DrawWalls( SpriteBatch spriteBatch )
		{
			for ( int i = 0 ; i < walls.Count ; i++ )
			{
				if( walls[ i ] != null && walls[ i ].mActive == true )
				{
					Vector2 position = new Vector2( walls[ i ].wall.X, walls[ i ].wall.Y );
					spriteBatch.Draw( mWallImage, position, walls[ i ].wall, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.7f );
				}

			}
		}

        public Point GetTileIndex(Vector2 inPosition)
        {
            if (inPosition == new Vector2(-1, -1))
            {
                return new Point(-1, -1);
            }

            return new Point( ( int )inPosition.X / tileSize, ( int )inPosition.Y / tileSize );
        }
    }

	public class Wall
	{
		public Rectangle wall;
		public bool mActive = true;

		public Wall()
		{
		}

		public Wall( Rectangle inputRectangle )
		{
			wall = inputRectangle;
		}
	}

    public class Decor : GameObject
    {
        public string imagePath;
        public Rectangle sourceRect;

        public string Name {  get { return imagePath; } }

        public Decor()
        {
            mCollidable = false;
        }

        public Decor( Vector2 inPos, string imgPath, float inputDepth )
        {
            position = inPos;
            imagePath = imgPath;
            layerDepth = inputDepth;
            mActive = true;
            mCollidable = false;
        }

        public virtual void Load( ContentManager content, string asset )
        {
            mImage = TextureLoader.Load(asset, content );
            mImage.Name = asset;

            mBoundingBoxWidth = mImage.Width;
            mBoundingBoxHeight = mImage.Height;

            if( sourceRect == Rectangle.Empty )
            {
                sourceRect = new Rectangle(0, 0, mImage.Width, mImage.Height );
            }
        }

        public void SetImage( Texture2D input, string newPath )
        {
            mImage = input;
            imagePath = newPath;

            mBoundingBoxWidth = sourceRect.Width = mImage.Width;
            mBoundingBoxHeight = sourceRect.Height = mImage.Height;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if( mImage != null && mActive )
            {
                spriteBatch.Draw( mImage, position, sourceRect, mDrawColor, mRotation, Vector2.Zero, mScale, SpriteEffects.None, layerDepth );
            }
        }
    }
}
