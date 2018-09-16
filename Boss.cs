using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FlappyF
{
    class Boss
    {
        public Animation BossAnimation = new Animation();
        public Vector2 Position;
        public int Health;
        public bool isActive;
        public float Speed;
        public int Damage;

        public int Width
        {
            get
            {
                return BossAnimation.FrameWidth;
            }
        }

        public int Height
        {
            get { return BossAnimation.FrameHeight; }
        }


        public void Initialize(Animation animation, Vector2 position, int health, int damage, float speed)
        {
            BossAnimation = animation;
            Position = position;
            Health = health;
            Damage = damage;
            Speed = speed;
        }


        public void Update(GameTime gameTime)
        {
            //Position.X = Position.X - Speed;
            BossAnimation.Position = Position;
            BossAnimation.Update(gameTime);

            if (Health <= 0 || Position.X < -Height)
            {
                isActive = false;
            }

        }


        public void Draw(SpriteBatch sb)
        {
                BossAnimation.Draw(sb);
        }
    }
}
