using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FlappyF
{
    class MovingBackground
    {
        private Texture2D _texture;

        private float _speed;
        private Rectangle _bg1;
        private Rectangle _bg2;
        private int _bg_h, _bg_w;
        private int _W, _H;


        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public int Width
        {
            get { return _W; }
        }

        public int Height
        {
            get { return _H; }
        }

        public void Initialize(Texture2D texture, float speed, int screen_w, int screen_h)
        {
            _texture = texture;
            _speed = speed;
            _bg_h = _texture.Height;
            _bg_w = _texture.Width;
            _W = screen_w;
            _H = screen_h;
            _bg1 = new Rectangle(0, 0, _bg_w, _bg_h);
            _bg2 = new Rectangle(_bg_w, 0, _bg_w, _bg_h);
        }

        public void Update()
        {
            // update background
            if (_bg1.X <= 0 - _bg_w)
            {
                _bg1.X = _bg2.Width;
            }
            if (_bg2.X <= 0 - _bg_w)
            {
                _bg2.X = _bg1.Width;
            }
            _bg1.X = (int)(_bg1.X + _speed);
            _bg2.X = (int)(_bg2.X + _speed);

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_texture, _bg1, Color.White);
            sb.Draw(_texture, _bg2, Color.White);
        }


    }
}
