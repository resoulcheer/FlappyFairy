using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FlappyF
{
    class Player
    {
        public Animation PlayerAnimation;
        public Vector2 Position;
        public int Health;
        public bool isActive;
        public float Speed;

        public int Width
        {
            get
            {
                return PlayerAnimation.FrameWidth;
            }
        }

        public int Height
        {
            get { return PlayerAnimation.FrameHeight; }
        }




        public void Initialize(Animation animation, Vector2 position)
        {
            PlayerAnimation = animation;
            Position = position;
            Health = 100;
            isActive = true;
            Speed = 5.0f;
        }




        public void Update(GameTime gameTime)
        {
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);

            if (Health <= 0)
            {
                isActive = false;
            }

        }



        public void Draw(SpriteBatch sb)
        {
            if (isActive == true)
            {
                PlayerAnimation.Draw(sb);
            }
        }



        public void MoveUp()
        {
            Position.Y = Position.Y - (Speed + 3);
        }
        public void MoveDown()
        {
            Position.Y = Position.Y + (Speed + 1);
        }
    }
}
