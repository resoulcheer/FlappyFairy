using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FlappyF
{
    class Wall
    {
        public Texture2D WallTexture;
        public Vector2 Position;
        public int Health;
        public bool isActive;
        public float Speed;
        public int Damage;
        private int W;

        public void Intialize(Texture2D texture, Vector2 position, int width, float speed)
        {
            WallTexture = texture;
            Position = position;
            isActive = true;
            Damage = 100;
            Speed = speed;
            W = width;
        }


        public void Update(GameTime gameTime)
        {
            Position.X -= Speed;

            if (Position.X < -W)
            {
                isActive = false;
            }

        }


        public void Draw(SpriteBatch sb)
        {
            sb.Draw(WallTexture, Position, Color.White);
        }
    }
}
