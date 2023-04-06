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
        public static int mScore;

		public Player()
		{
		}

		public Player( Vector2 inputPosition )
		{
			mPosition = inputPosition;
		}

		public override void Initialize()
		{
            mScore = 0;
			base.Initialize();
		}

		public override void Load( ContentManager content )
		{
			mImage = TextureLoader.Load( "spritesheet", content );

			LoadAnimation("ShyBoy.anm", content );
			ChangeAnimation(Animations.IdleLeft);

			base.Load( content );

			mBoundingBoxOffset.X = 0;
			mBoundingBoxOffset.Y = 0;
			mBoundingBoxWidth = mAnimationSet.width;
			mBoundingBoxHeight = mAnimationSet.height;
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

        protected override void UpdateAnimations()
        {
			if( mCurrentAnimation == null )
            {
				return;
            }


            base.UpdateAnimations();

			if( mVelocity != Vector2.Zero || mIsJumping == true )
            {
				if( mDirection.X < 0 && AnimationIsNot( Animations.RunLeft ))
                {
					ChangeAnimation(Animations.RunLeft);
                }
				else if( mDirection.X > 0 && AnimationIsNot(Animations.RunRight))
				{
					ChangeAnimation(Animations.RunRight);
				}
            }
			else if (mVelocity == Vector2.Zero || mIsJumping == false)
			{
				if (mDirection.X < 0 && AnimationIsNot(Animations.IdleLeft))
				{
					ChangeAnimation(Animations.IdleLeft);
				}
				else if (mDirection.X > 0 && AnimationIsNot(Animations.IdleRight))
				{
					ChangeAnimation(Animations.IdleRight);
				}
			}
		}
    }
}
