using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Topic_4___Timers_and_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        bool exploded = false, timing = false;
        
        Texture2D bombTexture, snipsTexture, explosionTexture;
        SoundEffect explode;
        SoundEffectInstance explodeInstance;
        Rectangle bombRect, wireRect;

        SpriteFont timeFont;

        MouseState mouseState, prevMouseState;

        float seconds = 0;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here-----------

            base.Initialize();
            bombRect = new Rectangle(50, 50, 700, 400);
            wireRect = new Rectangle(490, 160, 150, 20);
           
            
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            bombTexture = Content.Load<Texture2D>("bomb");
            timeFont = Content.Load<SpriteFont>("TimeFont");
            snipsTexture = Content.Load<Texture2D>("snips");
            explosionTexture = Content.Load<Texture2D>("boom");
            explode = Content.Load<SoundEffect>("explosion");
            explodeInstance = explode.CreateInstance();

        }

        protected override void Update(GameTime gameTime)
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            
            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (timing == false)
            {
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (exploded)
            {
                if (explodeInstance.State == SoundState.Stopped)
                {
                    Exit();
                }
            }

            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                if (wireRect.Contains(mouseState.Position))
                {
                    timing = true;
                }
            }

            if (seconds > 15 && exploded == false)
            {
                timing = true;
                exploded = true;
                explodeInstance.Play();
                
            } 

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(bombTexture, bombRect, Color.White);
            _spriteBatch.DrawString(timeFont, (15 - seconds).ToString("00.0"), new Vector2(305, 210), Color.DarkRed);
            _spriteBatch.Draw(snipsTexture, mouseState.Position.ToVector2(), Color.White); 
            if (exploded == true)
            {
                _spriteBatch.Draw(explosionTexture, bombRect, Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
