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
	public class Player : FireCharacter
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

		public override void Update( List<GameObject> objects, Map currentMap )
		{
			CheckInput( objects, currentMap);
			base.Update( objects, currentMap );
		}

		private void CheckInput( List<GameObject> objects, Map currentMap )
		{
			if( Character.mApplyGravity == false )
			{
				if( Input.IsKeyDown( Keys.D ) )
				{
					MoveRight();
				}
				else if( Input.IsKeyDown( Keys.A ) )
				{
					MoveLeft();
				}
				if( Input.IsKeyDown( Keys.W ) )
				{
					MoveUp();
				}
				else if( Input.IsKeyDown( Keys.S ) )
				{
					MoveDown();
				}
			}
			else
			{
				if( Input.IsKeyDown( Keys.D ) )
				{
					MoveRight();
				}
				else if( Input.IsKeyDown( Keys.A ) )
				{
					MoveLeft();
				}
				if( Input.IsKeyDown( Keys.W ) )
				{
					Jump( currentMap );
				}
				
			}

			if( Input.KeyPressed( Keys.Space ) )
			{
				Fire();
			}
		}
	}
}
