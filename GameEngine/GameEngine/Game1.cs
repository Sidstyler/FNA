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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager mGraphics;
        SpriteBatch mSpriteBatch;

		public List<GameObject> mGameObjects = new List<GameObject>();
		public Map mMap = new Map();


        GameHUD mGameHud = new GameHUD();

        public Game1() //This is the constructor, this function is called whenever the game class is created.
        {
            mGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Resolution.Init(ref mGraphics);
            Resolution.SetVirtualResolution(1280, 720); // Resolution our assets are based on

            Resolution.SetResolution(1280, 720, false);
        }

        /// <summary>
        /// This function is automatically called when the game launches to initialize any non-graphic variables.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            Camera.Initialize();
        }

        /// <summary>
        /// Automatically called when your game launches to load any game assets (graphics, audio etc.)
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            mSpriteBatch = new SpriteBatch(GraphicsDevice);
			mMap.Load( Content );

            mGameHud.Load(Content);

			LoadLevel();
        }

        /// <summary>
        /// Called each frame to update the game. Games usually runs 60 frames per second.
        /// Each frame the Update function will run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            Input.Update();

			UpdateObjects();

            mMap.Update(mGameObjects);

            UpdateCamera();

            //Update the things FNA handles for us underneath the hood:
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game is ready to draw to the screen, it's also called each frame.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            //This will clear what's on the screen each frame, if we don't clear the screen will look like a mess:
            GraphicsDevice.Clear( Color.CornflowerBlue );

            Resolution.BeginDraw();

            mSpriteBatch.Begin( SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Camera.GetTransformMatrix() );
			DrawObjects();
			mMap.DrawWalls( mSpriteBatch );
            mSpriteBatch.End();

            mGameHud.Draw(mSpriteBatch);

            //Draw the things FNA handles for us underneath the hood:
            base.Draw(gameTime);
        }

		public void LoadLevel()
		{
			mGameObjects.Add( new Player( new Vector2( 640, 360 ) ) );

			mGameObjects.Add( new Enemy( new Vector2( 300, 522 ) ) );

			mMap.mWalls.Add( new Wall( new Rectangle( 256, 256, 256, 256 ) ) );
			mMap.mWalls.Add( new Wall( new Rectangle( 0, 650, 1280, 128 ) ) );

            mMap.mDecor.Add(new Decor(Vector2.Zero, "background", 1f) );

            mMap.LoadMap(Content);


			LoadObjects();
		}

		public void LoadObjects()
		{
			for( int i = 0 ; i < mGameObjects.Count ; i++ )
			{
				mGameObjects[ i ].Initialize();
				mGameObjects[ i ].Load( Content );
			}
		}

		public void UpdateObjects()
		{
			for( int i = 0 ; i < mGameObjects.Count ; i++ )
			{
				mGameObjects[ i ].Update( mGameObjects, mMap );
				
			}
		}

		public void DrawObjects()
		{
			for( int i = 0 ; i < mGameObjects.Count ; i++ )
			{
				mGameObjects[ i ].Draw( mSpriteBatch );

			}

            for (int i = 0; i < mMap.mDecor.Count; i++)
            {
                mMap.mDecor[ i ].Draw(mSpriteBatch);

            }
        }

        private void UpdateCamera()
        {
            if( mGameObjects.Count == 0 )
            {
                return;
            }
            Camera.Update(mGameObjects[0].mPosition);
        }
	}
}
