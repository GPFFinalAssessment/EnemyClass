using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace GPF_Final_Assessment
{
    public class Player
    {
        public Texture2D texture;
		public Vector2 position;
        public Game1 game;
        public float movementSpeed;
        
        public Player(Texture2D texture, Vector2 pos)
        {
            this.texture = texture;
            this.position = pos;
            this.movementSpeed = 2f;
        }

        public void Update(GameTime gametime, GraphicsDeviceManager gdManager)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && position.Y > 120) position.Y -= movementSpeed;
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && (position.Y < (gdManager.PreferredBackBufferHeight - movementSpeed - this.texture.Height))) position.Y += movementSpeed;
            //if (Keyboard.GetState().IsKeyDown(Keys.A)) position.X -= 0.5f;
            //if (Keyboard.GetState().IsKeyDown(Keys.D)) position.X += 0.5f;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Vector2 offset = new Vector2(texture.Width / 2, 0);
            spritebatch.Draw(texture, position - offset, Color.White);
        }
    }
}