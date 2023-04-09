using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text;

namespace GameEngine
{
    public class AnimatedObject : GameObject
    {
        protected int mCurrentAnimationFrame;

        protected float mAnimationTimer;

        protected int mCurrentAnimationX = -1;
        protected int mCurrentAnimationY = -1;

        protected AnimationSet mAnimationSet = new AnimationSet();

        protected Animation mCurrentAnimation;

        protected bool mFlipRightFrames = true;
        protected bool mFlipLeftFrames = false;

        protected SpriteEffects mSpriteEffect = SpriteEffects.None;

        protected enum Animations
        {
            RunLeft,
            RunRight,
            IdleLeft,
            IdleRight
        }

        protected void LoadAnimation( string path, ContentManager content )
        {
            AnimationData animationData = AnimationLoader.Load(path);
            mAnimationSet = animationData.animation;

            mCenter.X = mAnimationSet.width / 2;
            mCenter.Y = mAnimationSet.height / 2;

            if( mAnimationSet.animationList.Count > 0 )
            {
                mCurrentAnimation = mAnimationSet.animationList[0];
                mCurrentAnimationFrame = 0;
                mAnimationTimer = 0.0f;

                CalculateFramePoisition();
            }
        }

        public override void Update(List<GameObject> objects, Map currentMap)
        {
            base.Update(objects, currentMap);
            if( mCurrentAnimation != null )
            {
                UpdateAnimations();
            }
        }

        protected void CalculateFramePoisition()
        {
            int coordinate = mCurrentAnimation.animationOrder[ mCurrentAnimationFrame ];

            mCurrentAnimationX = (coordinate % mAnimationSet.gridX * mAnimationSet.width);
            mCurrentAnimationY = (coordinate / mAnimationSet.gridX * mAnimationSet.height);
        }

        protected virtual void UpdateAnimations()
        {
            if( mCurrentAnimation.animationOrder.Count < 1 )
            {
                return;
            }

            mAnimationTimer -= 1;

            if( mAnimationTimer <= 0 )
            {
                mAnimationTimer = mCurrentAnimation.speed;

                if (IsAnimationComplete() )
                {
                    mCurrentAnimationFrame = 0;
                }
                else
                {
                    mCurrentAnimationFrame++;
                }

                CalculateFramePoisition();
                     
            }
        }

        protected virtual void ChangeAnimation( Animations newAnimation )
        {
            mCurrentAnimation = GetAnimation(newAnimation);

            if( mCurrentAnimation == null )
            {
                return;
            }

            mCurrentAnimationFrame = 0;
            mAnimationTimer = mCurrentAnimation.speed;

            CalculateFramePoisition();

            if( mFlipRightFrames == true && mCurrentAnimation.name.Contains("Right") ||
                mFlipLeftFrames == true && mCurrentAnimation.name.Contains("Left") )
            {
                mSpriteEffect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                mSpriteEffect = SpriteEffects.None;
            }
        }

        private Animation GetAnimation( Animations inAnimation )
        {
            string name = GetAnimationName( inAnimation );

            for (int i = 0; i < mAnimationSet.animationList.Count; i++)
            {
                if( mAnimationSet.animationList[ i ].name == name )
                {
                    return mAnimationSet.animationList[i];
                }
            }
            return null;
        }

        public bool IsAnimationComplete()
        {
            return mCurrentAnimationFrame >= mCurrentAnimation.animationOrder.Count - 1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if( mActive == false )
            {
                return;
            }

            if( mCurrentAnimationX == -1 ||mCurrentAnimationY == -1 )
            {
                base.Draw(spriteBatch); 
            }
            else
            {
                spriteBatch.Draw(mImage, position, new Rectangle(mCurrentAnimationX, mCurrentAnimationY, mAnimationSet.width, mAnimationSet.height), mDrawColor, mRotation, Vector2.Zero, mScale, mSpriteEffect, layerDepth);
            }
        }

        protected string GetAnimationName(Animations animation)
        {
            //Make an accurately spaced string. Example: "RunLeft" will now be "Run Left":
            return AddSpacesToSentence(animation.ToString(), false);
        }

        protected bool AnimationIsNot(Animations input)
        {
            //Used to check if our currentAnimation isn't set to the one passed in:
            return mCurrentAnimation != null && GetAnimationName(input) != mCurrentAnimation.name;
        }

        public string AddSpacesToSentence(string text, bool preserveAcronyms) //IfThisWasPassedIn... "If This Was Passed In" would be returned
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                        (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                         i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                        newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }
}
