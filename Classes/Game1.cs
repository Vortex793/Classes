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
        Random generator;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        List<Texture2D> booTextures;
        Texture2D hauntedBackgroundTexture;
        Texture2D titleTexture;
        Texture2D endTexture;
        Texture2D marioTexture;
        Rectangle marioRect;

        Screen screen;
        Rectangle window;

        MouseState mouseState;
        KeyboardState keyboardState;

        List<Ghost> ghosts;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screen = Screen.Title;
            generator = new Random();
            booTextures = new List<Texture2D>();
            ghosts = new List<Ghost>();
            marioRect = new Rectangle(0, 0, 30, 30);
            base.Initialize();
            this.IsMouseVisible = false;
            for (int i = 0; i < 20; i++)
                ghosts.Add(new Ghost(booTextures, new Rectangle(generator.Next(500), generator.Next(500), 40, 40)));
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            hauntedBackgroundTexture = Content.Load<Texture2D>("haunted-background");
            titleTexture = Content.Load<Texture2D>("haunted-title");
            endTexture = Content.Load<Texture2D>("haunted-end-screen");
            booTextures.Add(Content.Load<Texture2D>("boo-stopped"));
            marioTexture = Content.Load<Texture2D>("mario");


            for (int i = 1; i <= 8; i++)
                booTextures.Add(Content.Load<Texture2D>("Images/boo-move-" + i));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            mouseState = Mouse.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();


            if (screen == Screen.Title)
            {
                _spriteBatch.Draw(titleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            }
            else if (screen == Screen.Game)
            {

            }
            else if (screen == Screen.End)
            {

            }
                _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
