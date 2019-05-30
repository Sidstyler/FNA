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
	public class Character : GameObject
	{
		public Vector2 mVelocity;

		//Customize feel of our movement
		protected float mDeceleration = 1.2f; // The lower your decel is the slower you slow down
		protected float mAcceleration = .78f; //The lower your acceleration is, the slower you take off
		protected float mMaxSpeed = 5f;

		const float mGravity = 1f;
		const float mJumpVelocity = 16f;
		const float mMaxFallVelocity = 32f;

		protected bool mIsJumping;
		public static bool mApplyGravity = true;

		public override void Initialize()
		{
			mVelocity = Vector2.Zero;
			mIsJumping = false;

			base.Initialize();
		}

		public override void Update( List<GameObject> objects, Map currentMap )
		{
			UpdateMovement( objects, currentMap );
			base.Update( objects, currentMap );
		}

		private void UpdateMovement( List<GameObject> objects, Map currentMap )
		{
			if( mVelocity.X != 0 && CheckCollisions( currentMap, objects, true ) )
			{
				mVelocity.X = 0;
			}

			mPosition.X += mVelocity.X;

			if( mVelocity.Y != 0 && CheckCollisions( currentMap, objects, false ) )
			{
				mVelocity.Y = 0;
			}

			mPosition.Y += mVelocity.Y;

			if( mApplyGravity == true )
			{
				ApplyGravity( currentMap );
			}

			mVelocity.X = TendToZero( mVelocity.X, mDeceleration );

			if( mApplyGravity == false )
			{
				mVelocity.Y = TendToZero( mVelocity.Y, mDeceleration );
			}
		}

		private void ApplyGravity( Map map )
		{
			if( mIsJumping == true || OnGround( map ) == Rectangle.Empty )
			{
				mVelocity.Y += mGravity;
			}

			if( mVelocity.Y > mMaxFallVelocity )
			{
				mVelocity.Y = mMaxFallVelocity;
			}
		}

		protected void MoveRight()
		{
			mVelocity.X += mAcceleration + mDeceleration;

			if( mVelocity.X > mMaxSpeed )
			{
				mVelocity.X = mMaxSpeed;
			}

			mDirection.X = 1;
		}

		protected void MoveLeft()
		{
			mVelocity.X -= mAcceleration + mDeceleration;

			if( mVelocity.X < -mMaxSpeed )
			{
				mVelocity.X = -mMaxSpeed;
			}

			mDirection.X = -1;
		}

		protected void MoveDown()
		{
			mVelocity.Y += mAcceleration + mDeceleration;

			if( mVelocity.Y > mMaxSpeed )
			{
				mVelocity.Y = mMaxSpeed;
			}

			mDirection.Y = 1;
		}

		protected void MoveUp()
		{
			mVelocity.Y -= mAcceleration + mDeceleration;

			if( mVelocity.Y < -mMaxSpeed )
			{
				mVelocity.Y = -mMaxSpeed;
			}

			mDirection.Y = -1;
		}

		protected bool Jump( Map map )
		{
			if( mIsJumping == true )
			{
				return false;
			}

			if( mVelocity.Y == 0 && OnGround( map ) != Rectangle.Empty )
			{
				mVelocity.Y -= mJumpVelocity;
				mIsJumping = true;
				return true;
			}
			return false;
		}

		protected virtual bool CheckCollisions( Map map, List<GameObject> objects, bool xAxis )
		{
			Rectangle futureBox = mBoundingBox;

			int maxX = ( int )mMaxSpeed;
			int maxY = ( int )mMaxSpeed;

			if( mApplyGravity == true )
			{
				maxY = ( int )mJumpVelocity;
			}

			if( xAxis == true && mVelocity.X != 0 )
			{
				if( mVelocity.X > 0 )
					futureBox.X += maxX;
				else
					futureBox.X -= maxX;
			}
			else if( mApplyGravity == false && xAxis == false && mVelocity.Y != 0 )
			{
				if( mVelocity.Y > 0 )
					futureBox.Y += maxY;
				else
					futureBox.Y -= maxY;
			}
			else if( mApplyGravity == true && xAxis == false && mVelocity.Y != mGravity )
			{
				if( mVelocity.Y > 0 )
					futureBox.Y += maxY;
				else
					futureBox.Y -= maxY;
			}

			Rectangle wallCollision = map.CheckCollision( futureBox );

			if( wallCollision != Rectangle.Empty )
			{
				if( mApplyGravity == true && mVelocity.Y >= mGravity && ( futureBox.Bottom > wallCollision.Top - mMaxSpeed ) && ( futureBox.Bottom <= wallCollision.Top + mVelocity.Y ) )
				{
					//Do land response
					LandResponse( wallCollision );
					return true;
				}
				else
				{
					return true;
				}
			}

			//Check for object collision
			for( int i = 0 ; i < objects.Count ; i++ )
			{
				if( objects[ i ] != this && objects[ i ].mActive == true && objects[ i ].mCollidable && objects[ i ].CheckCollision( futureBox ) == true )
				{
					return true;
				}
			}

			return false;
		}

		public void LandResponse( Rectangle wallCollision )
		{
			mPosition.Y = wallCollision.Top - ( mBoundingBoxHeight + mBoundingBoxOffset.Y );
			mVelocity.Y = 0;
			mIsJumping = false;
		}

		protected Rectangle OnGround( Map map )
		{
			Rectangle futureBoundingBox = new Rectangle( ( int )( mPosition.X + mBoundingBoxOffset.X ), ( int )( mPosition.Y + mBoundingBoxOffset.Y + ( mVelocity.Y + mGravity ) ), mBoundingBoxWidth, mBoundingBoxHeight );

			return map.CheckCollision( futureBoundingBox );
		}

		protected float TendToZero( float val, float amount )
		{
			if( val > 0f && ( val -= amount ) < 0f )
				return 0f;
			if( val < 0f && ( val += amount ) > 0f )
				return 0f;
			return val;
		}
	}
}
