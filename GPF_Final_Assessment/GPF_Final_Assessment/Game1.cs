#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
#endregion

namespace GPF_Final_Assessment
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        enum GameState { PAUSE, PLAY }
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Background background;
        Player player;
        Texture2D backTexture;
        Texture2D playerTexture;
		Texture2D EnemyTexture;
        float secondsToComplete = 10;
        GameState gameState = GameState.PAUSE;

		List<EnemyClass> enemies = new List<EnemyClass> ();

		float enemyspawntime = 3.0f;
		float enemyspawncooldown = 0.0f;
		Random rand = new Random ();


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 680;
            graphics.PreferredBackBufferWidth = 1100;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            backTexture = Content.Load<Texture2D>("ui_background");
            background = new Background(backTexture, new Vector2(0, 0), secondsToComplete);
            playerTexture = Content.Load<Texture2D>("player_blue");
            player = new Player(playerTexture, new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2));
			EnemyTexture = Content.Load<Texture2D> ("Enemy");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            switch (gameState)
            {
                case GameState.PAUSE:
                    PauseUpdate(gameTime);
                    break;
                default:
                    PlayUpdate(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        public void PauseUpdate(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                gameState = GameState.PLAY;
        }


        public void PlayUpdate(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                gameState = GameState.PAUSE;
            else
            {
                background.Update(gameTime);
                player.Update(gameTime, graphics);

            }
			enemyspawncooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (enemyspawncooldown < 0.0f) {
				SpawnEnemyClass ();
				enemyspawncooldown = enemyspawntime;
			}

			for (int i = enemies.Count - 1; i >= 0; i--) {
				enemies [i].Update (gameTime);

				if (enemies [i].enemyposition.X < -graphics.PreferredBackBufferWidth)
					enemies.RemoveAt (i);
			}
			collisionsWithEnemy ();
        }

		
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            // TODO: Add your drawing code here

            //draw background
            background.Draw(spriteBatch);
            player.Draw(spriteBatch);
            
			for (int i = 0; i < enemies.Count; i++) {
				enemies [i].Draw (gameTime, spriteBatch);
			}


            spriteBatch.End();
            base.Draw(gameTime);
        }

		void SpawnEnemyClass()

		{

			float randomX = rand.Next (1150, 1150);
			float randomY = rand.Next (120, 600);
			EnemyClass e = new EnemyClass (EnemyTexture, new Vector2 (randomX, randomY));
			enemies.Add (e);


		}
		void collisionsWithEnemy()
		{
			Rectangle playerrect = new Rectangle ((int)player.position.X, (int)player.position.Y, player.texture.Width, player.texture.Height);

			for (int i = enemies.Count - 1; i >= 0; i--) {
				Rectangle enemiesrect = new Rectangle ((int)enemies [i].enemyposition.X, (int)enemies [i].enemyposition.Y, enemies [i].EnemyTexture.Width, enemies [i].EnemyTexture.Height);


				if (Rectangle.Intersect (playerrect, enemiesrect) != Rectangle.Empty) {
					enemies.RemoveAt (i);
					break;
				}
			}
		}
    }
}
