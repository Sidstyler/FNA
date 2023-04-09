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

		public List<GameObject> objects = new List<GameObject>();
		public Map map = new Map();


        GameHUD mGameHud = new GameHUD();

        Editor mEditor;

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
#if DEBUG
            mEditor = new Editor( this );
            mEditor.Show();

#endif
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

#if DEBUG
            mEditor.LoadTextures( Content );

#endif
            map.Load( Content );

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

            map.Update(objects);

            UpdateCamera();

#if DEBUG
            mEditor.Update( objects, map );

#endif

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

#if DEBUG
            mEditor.Draw(mSpriteBatch);

#endif

            DrawObjects();
			map.DrawWalls( mSpriteBatch );
            mSpriteBatch.End();

            mGameHud.Draw(mSpriteBatch);

            //Draw the things FNA handles for us underneath the hood:
            base.Draw(gameTime);
        }

		public void LoadLevel()
		{
			objects.Add( new Player( new Vector2( 640, 360 ) ) );

			objects.Add( new Enemy( new Vector2( 300, 522 ) ) );

			map.walls.Add( new Wall( new Rectangle( 256, 256, 256, 256 ) ) );
			map.walls.Add( new Wall( new Rectangle( 0, 650, 1280, 128 ) ) );

            map.decor.Add(new Decor(Vector2.Zero, "background", 1f) );

            map.LoadMap(Content);


			LoadObjects();
		}

		public void LoadObjects()
		{
			for( int i = 0 ; i < objects.Count ; i++ )
			{
				objects[ i ].Initialize();
				objects[ i ].Load( Content );
			}
		}

		public void UpdateObjects()
		{
			for( int i = 0 ; i < objects.Count ; i++ )
			{
				objects[ i ].Update( objects, map );
				
			}
		}

		public void DrawObjects()
		{
			for( int i = 0 ; i < objects.Count ; i++ )
			{
				objects[ i ].Draw( mSpriteBatch );

			}

            for (int i = 0; i < map.decor.Count; i++)
            {
                map.decor[ i ].Draw(mSpriteBatch);

            }
        }

        private void UpdateCamera()
        {
            if( objects.Count == 0 )
            {
                return;
            }
            Camera.Update(objects[0].position);
        }
	}
}
