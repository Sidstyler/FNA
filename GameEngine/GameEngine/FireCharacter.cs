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
	public class FireCharacter : Character
	{
		List<Bullet> mBullets = new List<Bullet>();

		const int mNumBullets = 20;

		public FireCharacter()
		{

		}

		public override void Initialize()
		{
			if( mBullets.Count == 0 )
			{
				for( int i = 0 ; i < mNumBullets ; i++ )
				{
					mBullets.Add( new Bullet() );
				}
			}
			base.Initialize();
		}

		public override void Load( ContentManager content )
		{
			for( int i = 0 ; i < mBullets.Count ; i++ )
			{
				mBullets[ i ].Load( content );
			}
			base.Load( content );
		}

		public void Fire()
		{
			for( int i = 0 ; i < mBullets.Count ; i++ )
			{
				if( mBullets[ i ].mActive == false )
				{
					mBullets[ i ].Fire( this, mPosition, mDirection );
					break;
				}
			}
		}

		public override void Update( List<GameObject> objects, Map currentMap )
		{
			for( int i = 0 ; i < mBullets.Count ; i++ )
			{
				mBullets[ i ].Update( objects, currentMap );
			}
			base.Update( objects, currentMap );
		}

		public override void Draw( SpriteBatch spriteBatch )
		{
			for( int i = 0 ; i < mBullets.Count ; i++ )
			{
				mBullets[ i ].Draw( spriteBatch );
			}
			base.Draw( spriteBatch );
		}
	}
}
