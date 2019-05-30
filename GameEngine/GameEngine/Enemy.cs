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
	public class Enemy : FireCharacter
	{
		int mRespawnTimer;
		const int mMaxRespawnTimer = 60;

		Random mRandom = new Random();

		SoundEffect mExplosion;

		public Enemy()
		{
		}

		public Enemy( Vector2 inPosition )
		{
			mPosition = inPosition;
		}

		public override void Initialize()
		{
			mActive = true;
			mCollidable = false;

			mPosition.X = mRandom.Next( 0, 1100 );
			base.Initialize();
		}

		public override void Load( ContentManager content )
		{
			mImage = TextureLoader.Load( "enemy", content );
			mExplosion = content.Load<SoundEffect>( "Audio\\explosion" );
			base.Load( content );
		}

		public override void Update( List<GameObject> objects, Map currentMap )
		{

			if( mRespawnTimer > 0 )
			{
				mRespawnTimer--;
				if( mRespawnTimer <= 0 )
				{
					Initialize();
				}
			}
			base.Update( objects, currentMap );
		}

		public override void BulletResponse()
		{
			mActive = false;
			mRespawnTimer = mMaxRespawnTimer;

			mExplosion.Play( 0.1f, 0, 0 );
			base.BulletResponse();
		}
	}
}
