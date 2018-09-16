using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FlappyF
{
    class Enemy
    {
        public Animation EnemyAnimation;
        public Vector2 Position;
        public int Health;
        public bool isActive;
        public float Speed;
        public int Damage;
        public int Score;

        public int Width
        {
            get
            {
                return EnemyAnimation.FrameWidth;
            }
        }

        public int Height
        {
            get { return EnemyAnimation.FrameHeight; }
        }


        public void Initialize(Animation animation, Vector2 position)
        {
            EnemyAnimation = animation;
            Position = position;
            Health = 10;
            Damage = 10;
            isActive = true;
            Speed = 4.0f;
            Score = 100;
        }


        public void Update(GameTime gameTime)
        {
            Position.X = Position.X - Speed;
            EnemyAnimation.Position = Position;
            EnemyAnimation.Update(gameTime);

            if (Health <= 0 || Position.X < -Width)
            {
                isActive = false;
            }

        }


        public void Draw(SpriteBatch sb)
        {
            if (isActive == true)
            {
                EnemyAnimation.Draw(sb);
            }
        }
    }
}
