using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FlappyF
{
    class Bullet
    {
        public Texture2D Texture;
        public Vector2 Position;
        public bool isActive;
        public int Damage;
        private int W;
        public float Speed;

        public void Intialize(Texture2D texture, Vector2 position, int width)
        {
            Texture = texture;
            Position = position;
            isActive = true;
            Damage = 2;
            Speed = 20.0f;
            W = width;
        }

        public void Update()
        {
            Position.X += Speed;

            if(Position.X > W)
            {
                isActive = false;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, Position, Color.White);
        }

    }
}
