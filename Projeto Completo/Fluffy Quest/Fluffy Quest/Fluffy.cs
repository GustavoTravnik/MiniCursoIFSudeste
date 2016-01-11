using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluffy_Quest
{
    public class Fluffy : ClasseBase
    {

        float speed = 4;
        List<Pedra> pedras = new List<Pedra>();

        public Fluffy(Vector2 posicao, Texture2D textura, List<Pedra> pedras)
        {
            this.posicao = posicao;
            this.textura = textura;
            this.pedras = pedras;
        }

        private void LimitarBordas()
        {
            if (posicao.X < 0)
            {
                posicao.X = 0;
            }
            if (posicao.X > 1300 - textura.Width)
            {
                posicao.X = 1300 - textura.Width;
            }
            if (posicao.Y < 0)
            {
                posicao.Y = 0;
            }
            if (posicao.Y > 750 - textura.Height)
            {
                posicao.Y = 750 - textura.Height;
            }
        }

        private void Controle()
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Left))
            {
                if (!Collision.WillCollideLeft(posicao, pedras, speed))
                {
                    posicao.X -= speed;
                }
            }

            if (keyboard.IsKeyDown(Keys.Right))
            {
                if (!Collision.WillCollideRight(posicao, pedras, speed))
                {
                    posicao.X += speed;
                }
            }

            if (keyboard.IsKeyDown(Keys.Down))
            {
                if (!Collision.WillCollideDown(posicao, pedras, speed))
                {
                    posicao.Y += speed;
                }
            }

            if (keyboard.IsKeyDown(Keys.Up))
            {
                if (!Collision.WillCollideUp(posicao, pedras, speed))
                {
                    posicao.Y -= speed;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            Controle();
            LimitarBordas();
        }

        public override void Draw(SpriteBatch render)
        {
            base.Draw(render);
        }
    }
}
