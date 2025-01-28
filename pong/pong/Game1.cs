using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Paddle_Player PaddlePlayer;
        private PaddleAI PlayerAI;
        private Texture2D PaddleTexture, BallTexture, PaddleAITexture;
        private Ball _ball;
        private Microsoft.Xna.Framework.Rectangle screen;
        private Random rand;
        private SpriteFont ScorePlayer, ScoreAI;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 600;
            screen = new Microsoft.Xna.Framework.Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            _graphics.ApplyChanges();
            rand = new Random();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            PaddleTexture = Content.Load<Texture2D>("Paddle");
            PaddlePlayer = new Paddle_Player(PaddleTexture, new Vector2(0, screen.Height / 2 - PaddleTexture.Height / 2), Vector2.Zero, 5f, screen);
            BallTexture = Content.Load<Texture2D>("Ball");
            PaddleAITexture = Content.Load<Texture2D>("Paddle");
            PlayerAI = new PaddleAI(PaddleTexture, new Vector2(screen.Width - PaddleAITexture.Width, screen.Height / 2 - PaddleTexture.Height / 2), Vector2.Zero, 5f, screen);
            ScorePlayer = Content.Load<SpriteFont>("File");
            ScoreAI = Content.Load<SpriteFont>("File");
            _ball = new Ball(BallTexture, new Vector2(screen.Width / 2 - BallTexture.Width / 2, screen.Height / 2 - BallTexture.Height / 2), new Vector2(0, -1), 1f, screen);
            _ball.Restart();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();

            PaddlePlayer.Update(gameTime);
            PlayerAI.Update(gameTime);
            _ball.BoundsPaddle(PaddlePlayer, PlayerAI);
            _ball.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.DrawString(ScorePlayer,_ball.player1Score.ToString(),new Vector2(screen.Width/2-50,50),Microsoft.Xna.Framework.Color.White);
            _spriteBatch.DrawString(ScoreAI, _ball.player2Score.ToString(), new Vector2(screen.Width / 2 + 50, 50), Microsoft.Xna.Framework.Color.White);
            PaddlePlayer.Draw(_spriteBatch);
            PlayerAI.Draw(_spriteBatch);
            _ball.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        
    }
}
