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
	public class Bullet : GameObject
	{
		const float mSpeed = 12f;
		Character mOwner;

		int mLifeTime;

		const int mMaxTimer = 180;

		public Bullet()
		{
			mActive = false;
		}

		public override void Load( ContentManager content )
		{
			mImage = TextureLoader.Load( "bullet", content );
			base.Load( content );
		}

		public override void Update( List<GameObject> objects, Map currentMap )
		{
			if( mActive == false )
			{
				return;
			}

			mPosition += mDirection * mSpeed;

			CheckCollisions( objects, currentMap );

			mLifeTime--;
			if( mLifeTime <= 0 && mActive == true )
			{
				Destroy();
			}
			base.Update( objects, currentMap );
		}

		private void CheckCollisions( List<GameObject> objects, Map currentMap )
		{
			for ( int i	= 0 ; i < objects.Count ; i++ )
			{
				if( objects[ i ].mActive == true && objects[ i ] != mOwner && objects[ i ].CheckCollision( mBoundingBox ) == true )
				{
					Destroy();

					objects[ i ].BulletResponse();

					return;
				}
			}

			if( currentMap.CheckCollision( mBoundingBox ) != Rectangle.Empty )
			{
				Destroy();
			}
		}

		public void Destroy()
		{
			mActive = false;

		}

		public void Fire( Character owner, Vector2 inputPosition, Vector2 inputDirection )
		{
			mOwner = owner;
			mPosition = inputPosition;
			mDirection = inputDirection;
			mActive = true;
			mLifeTime = mMaxTimer;
		}
	}
}
