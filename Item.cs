using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FlappyF
{
    class Item
    {
        public Texture2D ItemTexture;
        public Vector2 Position;
        public int Health;
        public bool isActive;
        public float Speed;
        public int Impact;

        public void Intialize(Texture2D texture, Vector2 position)
        {
            ItemTexture = texture;
            Position = position;
            isActive = true;
            Impact = 1;
            Speed = 5.0f;
        }


        public void Update(GameTime gameTime)
        {
            Position.X -= Speed;

            //if (Position.X < -W)
            //{
                //isActive = false;
            //}

        }


        public void Draw(SpriteBatch sb)
        {
            if (isActive == true)
            {
                sb.Draw(ItemTexture, Position, Color.White);
            }
        }
    }
}
