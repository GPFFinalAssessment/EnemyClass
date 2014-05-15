using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GPF_Final_Assessment
{
    class Background
    {
        public Texture2D backgroundTexture;
        public Vector2 backgroundPosition;
        public double backgroundPixelsToFinishScreen;
        public float backgroundScrollSpeed;
        public float pixelsPassed;
        
        public Background(Texture2D texture, Vector2 pos, float secondsToFinish)
        {
            backgroundTexture = texture;
            backgroundPosition = pos;
            backgroundScrollSpeed = 50;
            pixelsPassed = 0;

            //calculate how many screens we need to display by the number of seconds at regular speed
            backgroundPixelsToFinishScreen = Math.Ceiling((secondsToFinish / backgroundScrollSpeed) * backgroundTexture.Width);
        }

        public void Update(GameTime gameTime)
        {
            float intMovement;
 
            //update the positioning of the scrolling background
            //use the number of pixels we have passed to figure out if we are done yet
            if (!IsEnd())
            {
                intMovement = (float)(gameTime.ElapsedGameTime.TotalSeconds * backgroundScrollSpeed);
                pixelsPassed += intMovement;
                backgroundPosition.X -= intMovement;

                backgroundPosition.X = backgroundPosition.X % backgroundTexture.Width;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the texture if still on screen.
            if (backgroundPosition.X > -backgroundTexture.Width)
                spriteBatch.Draw(backgroundTexture, backgroundPosition, null, Color.White);

            // Draw the texture a second time, behind the first,
            // to create the scrolling illusion.
            spriteBatch.Draw(backgroundTexture, (backgroundPosition + new Vector2(backgroundTexture.Width, 0)), null, Color.White);
        }

        public bool IsEnd()
        {
            if (pixelsPassed >= backgroundPixelsToFinishScreen)
                return true;
            else
                return false;
        }
    }
}
