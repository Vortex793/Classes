using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Ghost
    {
        private List<Texture2D> _textures;
        private int _textureIndex;
        private Vector2 _speed;
        private Rectangle _location;
        private SpriteEffects _direction;
        private float _animationSpeed;
        private float _seconds;
        private float _opacity;
        private float _speedMultiplier;

        public Ghost(List<Texture2D> textures, Rectangle location, float speedMultiplier = 1f)
        {
            _textures = textures;
            _textureIndex = 0;
            _speed = Vector2.Zero;
            _location = location;
            _direction = SpriteEffects.None;
            _opacity = 1f;
            _animationSpeed = 0.2f;
            _seconds = 0;
            _speedMultiplier = speedMultiplier;
        }
        public Rectangle Rect
        {
            get { return _location; }
        }
        public void Update(GameTime gameTime, MouseState mouseState)
        {

            // Calculates horizontal speed and direction sprite should face
            _speed = Vector2.Zero;
            if (mouseState.X < _location.X + _location.Width / 2)
            {
                _direction = SpriteEffects.FlipHorizontally;
                _speed.X = -_speedMultiplier;
            }
            else if (mouseState.X > _location.X + _location.Width)
            {
                _direction = SpriteEffects.None;
                _speed.X = _speedMultiplier;
            }
           
            // Calculates vertical speed
            if (mouseState.Y < _location.Y + _location.Height / 2)
                _speed.Y = -_speedMultiplier;
            else if (mouseState.Y > _location.Y + _location.Height)
                _speed.Y = _speedMultiplier;

            // Only move if the mouse button is down
            if (mouseState.LeftButton == ButtonState.Released)
            {
                _speed = Vector2.Zero; // Sets speed to zero if mouse not clicked
                _textureIndex = 0;
                _seconds = 0f;
                _opacity = 0.3f;
            }
            else
            {
                _opacity = 1f;

                if (_speed != Vector2.Zero)
                {
                    _seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (_seconds >= _animationSpeed)
                    {
                        _seconds = 0f;
                        _textureIndex++;

                        if (_textureIndex >= _textures.Count)
                            _textureIndex = 1;
                    }
                }
                else
                {
                    _textureIndex = 0;
                    _seconds = 0f;
                }
            }

            _location.Offset(_speed);

        }

        //public Rectangle Rect
        //{
        //    get { return _location; }
        //}

        public bool Contains(Point player)
        {
            return _location.Contains(player);
        }

        public bool Intersects(Rectangle player)
        {
            return _location.Intersects(player);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textures[_textureIndex], _location, null, Color.White * _opacity, 0f, Vector2.Zero, _direction, 1f);
        }

    }
}