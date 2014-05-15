using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace GPF_Final_Assessment
{
	public class EnemyClass
	{

		public Texture2D EnemyTexture;
		public Vector2 enemyposition;
		public Game1 game;
	

		
		public EnemyClass (Texture2D texture, Vector2 position)
			{

				this.EnemyTexture = texture;
			this.enemyposition = position;
			}

			public void Update (GameTime gameTime)
			{
			enemyposition.X -= 5.5f;
			}

			public void Draw (GameTime gameTime, SpriteBatch spritBatch)
			{
				Vector2 offset = new Vector2(EnemyTexture.Width,EnemyTexture.Height) / 2.0f; 
			spritBatch.Draw (EnemyTexture, enemyposition - offset, Color.White);
			}



		}
	}



