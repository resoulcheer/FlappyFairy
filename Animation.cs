using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FlappyF
{
    class Animation
    {
        Texture2D _spriteStrip;
        int _elapedTime;
        int _frameTime;
        int _frameCount;
        int _currentFrame;
        Rectangle _sourceRect;
        Rectangle _targetRect;

        public bool isActive;
        public bool isLoop;

        public Vector2 Position
        {
            get { return (new Vector2(_targetRect.X, _targetRect.Y)); }
            set
            {
                _targetRect.X = (int)value.X;
                _targetRect.Y = (int)value.Y;
            }
        }

        public int FrameWidth
        {
            get { return _sourceRect.Width; }
            set { _sourceRect.Width = value; }
        }

        public int FrameHeight
        {
            get { return _sourceRect.Height; }
            set { _sourceRect.Height = value; }
        }

        // Methods

        public void Initialize(Texture2D texture, Vector2 position,
            int frame_width, int frame_height, int frame_count,
            int frame_time, bool loop)
        {
            _frameCount = frame_count;
            _frameTime = frame_time;
            _spriteStrip = texture;
            Position = position;
            FrameWidth = frame_width;
            FrameHeight = frame_height;
            isLoop = loop;

            isActive = true;
            _currentFrame = 0;
            _elapedTime = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (!isActive)
            {
                return;
            }

            _elapedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_elapedTime > _frameTime)
            {
                _currentFrame++;
                if (_currentFrame == _frameCount) //เมื่อเลื่อนไปสุดขอบ
                {
                    _currentFrame = 0;
                    if (!isLoop)
                    { isActive = false; }
                }
                _elapedTime = 0;
            }

            //update source rectangle
            _sourceRect = new Rectangle(_currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
            _targetRect = new Rectangle((int)Position.X, (int)Position.Y, FrameWidth, FrameHeight);
        }

        public void Draw(SpriteBatch sb)
        {
            if (isActive)
            {
                sb.Draw(_spriteStrip, _targetRect, _sourceRect, Color.White);
            }
        }


    }
}
