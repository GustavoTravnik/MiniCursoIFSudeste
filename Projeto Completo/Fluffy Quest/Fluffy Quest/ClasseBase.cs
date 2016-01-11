using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluffy_Quest
{
    public abstract class ClasseBase
    {
        public Vector2 posicao;
        public Texture2D textura;

        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch render)
        {
            render.Draw(textura, posicao, Color.White);
        }
    }
}
