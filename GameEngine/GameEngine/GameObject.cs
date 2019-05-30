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
    public class GameObject
    {
        protected Texture2D mImage;
        protected Vector2 mPosition;

		public Color mDrawColor = Color.White;
        public float mScale = 1f;
        public float mRotation = 0f;
        public float mLayerDepth = 0.5f;
        public bool mActive = true;

        protected Vector2 mCenter;

		public bool mCollidable = true;
		protected int mBoundingBoxWidth;
		protected int mBoundingBoxHeight;
		protected Vector2 mBoundingBoxOffset;

		Texture2D mBoundingBoxImage;

		const bool mDrawBouningBoxes = true;

		protected Vector2 mDirection = new Vector2( 1, 0 );

		public Rectangle mBoundingBox
		{
			get
			{
				return new Rectangle( ( int )( mPosition.X + mBoundingBoxOffset.X ), ( int )( mPosition.Y + mBoundingBoxOffset.Y ), mBoundingBoxWidth, mBoundingBoxHeight );
			}
		}

        public GameObject()
        {
        }

        public virtual void Initialize()
        {
        }

        public virtual void Load( ContentManager content )
        {
			mBoundingBoxImage = TextureLoader.Load( "pixel", content );

			CalculateCenter();

			if( mImage != null )
			{
				mBoundingBoxWidth = mImage.Width;
				mBoundingBoxHeight = mImage.Height;
			}
        }

        public virtual void Update( List<GameObject> objects, Map currentMap )
        {
        }

		public virtual bool CheckCollision( Rectangle inputRectangle )
		{
			return mBoundingBox.Intersects( inputRectangle );
		}

		public virtual void Draw( SpriteBatch spriteBatch )
		{
			if( mBoundingBoxImage != null && mDrawBouningBoxes == true && mActive == true )
			{
				spriteBatch.Draw( mBoundingBoxImage, new Vector2( mBoundingBox.X, mBoundingBox.Y ), mBoundingBox, new Color( 120, 120, 120, 120 ), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f );
			}

			if( mImage != null && mActive == true )
			{
				spriteBatch.Draw( mImage, mPosition, null, mDrawColor, mRotation, Vector2.Zero, mScale, SpriteEffects.None, mLayerDepth );
			}
		}

        private void CalculateCenter()
        {
            if( mImage == null )
            {
                return;
            }
            mCenter.X = mImage.Width / 2;
            mCenter.Y = mImage.Height / 2;
        }

    }
}
