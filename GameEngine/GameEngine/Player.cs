﻿using System;
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
	public class Player : GameObject
	{
		public Player()
		{
		}

		public Player( Vector2 inputPosition )
		{
			mPosition = inputPosition;
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void Load( ContentManager content )
		{
			mImage = TextureLoader.Load( "sprite", content );
			base.Load( content );
		}

		public override void Update( List<GameObject> objects )
		{
			CheckInput();
			base.Update( objects );
		}

		private void CheckInput()
		{
			if( Input.IsKeyDown( Keys.D ) )
			{
				mPosition.X += 5;
			}
			else if( Input.IsKeyDown( Keys.A ) )
			{
				mPosition.X -= 5;
			}
			if( Input.IsKeyDown( Keys.W ) )
			{
				mPosition.Y -= 5;
			}
			else if( Input.IsKeyDown( Keys.S ) )
			{
				mPosition.Y += 5;
			}
		}
	}
}
