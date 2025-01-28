using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Dynamic;

namespace pong
{
    internal class PaddleAI:Sprite
    {
        private int score;
        public PaddleAI(Texture2D texture, Vector2 position, Vector2 direction, float speed, Rectangle screen):base(texture, position, direction, speed,screen)
        {
            score = 0;
        }

        public override void Update(GameTime gametime)
        {
            direction = Vector2.Zero;
            InputKeyboard();
            BoundsRestrictions();
            base.Update(gametime);
        }
        private void InputKeyboard()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                direction.Y = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                direction.Y = 1;
        }
        public void BoundsRestrictions()
        {
            if (position.Y < 0) position.Y = 0;
            if (position.Y > screen.Height - texture.Height) position.Y = screen.Height - texture.Height;

        }
    }
    
}
