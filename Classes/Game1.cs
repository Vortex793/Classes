using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.InteropServices.JavaScript;

namespace Classes
{
    enum Screen
    {
        Title,
        Game,
        End
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Random generator;
        private List<Texture2D> booTextures;
        private List<Ghost> ghosts;

        private Texture2D hauntedBackgroundTexture;
        private Texture2D titleTexture;
        private Texture2D endTexture;
        private Texture2D marioTexture;

        private Rectangle marioRect;
        private Rectangle window;

        private MouseState mouseState;
        private KeyboardState keyboardState;

        private Screen screen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;

            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            generator = new Random();

            booTextures = new List<Texture2D>();
            ghosts = new List<Ghost>();

            window = new Rectangle(0, 0, 800, 600);
            marioRect = new Rectangle(0, 0, 30, 30);

            screen = Screen.Title;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load background textures.
            hauntedBackgroundTexture =
                Content.Load<Texture2D>("haunted-background");

            titleTexture =
                Content.Load<Texture2D>("haunted-title");

            endTexture =
                Content.Load<Texture2D>("haunted-end-screen");

            marioTexture = Content.Load<Texture2D>("mario");

            // Index 0 is the stopped ghost.
            booTextures.Add(
                Content.Load<Texture2D>("boo-stopped"));

            // Indexes 1-8 are the moving ghost textures.
            for (int i = 1; i <= 8; i++)
            {
                booTextures.Add(
                    Content.Load<Texture2D>("boo-move-" + i));
            }

            // Create ghosts after their textures have loaded.
            for (int i = 0; i < 20; i++)
            {
                int size = generator.Next(30, 61);

                int x = generator.Next(
                    0, _graphics.PreferredBackBufferWidth - size + 1);

                int y = generator.Next(
                    0, _graphics.PreferredBackBufferHeight - size + 1);

                // Random movement speed: 1, 2, or 3.
                float speed = generator.Next(1, 4);

                ghosts.Add(new Ghost(
                    booTextures,
                    new Rectangle(x, y, size, size),
                    speed));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back
                    == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // Update input states.
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            // Center Mario on the mouse cursor.
            marioRect = new Rectangle(
                mouseState.X - marioRect.Width / 2,
                mouseState.Y - marioRect.Height / 2,
                marioRect.Width,
                marioRect.Height);

            if (screen == Screen.Title)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screen.Game;
                }
            }
            else if (screen == Screen.Game)
            {
                foreach (Ghost ghost in ghosts)
                {
                    ghost.Update(gameTime, mouseState);

                    // End the game if the cursor touches a ghost.
                    if (ghost.Contains(mouseState.Position))
                    {
                        screen = Screen.End;
                        break;
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if (screen == Screen.Title)
            {
                _spriteBatch.Draw(
                    titleTexture, window, Color.White);
            }
            else if (screen == Screen.Game)
            {
                _spriteBatch.Draw(
                    hauntedBackgroundTexture, window, Color.White);

                // Draw all ghosts.
                foreach (Ghost ghost in ghosts)
                {
                    ghost.Draw(_spriteBatch);
                }

                // Draw Mario in place of the mouse cursor.
                _spriteBatch.Draw(
                    marioTexture, marioRect, Color.White);
            }
            else if (screen == Screen.End)
            {
                _spriteBatch.Draw(
                    endTexture, window, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}