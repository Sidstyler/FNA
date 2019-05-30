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

        public GameObject()
        {
        }

        public virtual void Initialize()
        {
        }

        public virtual void Load( ContentManager content )
        {
        
        }

        public virtual void Update( List<GameObject> objects )
        {
        }

		public virtual void Draw( SpriteBatch spriteBatch )
		{
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
