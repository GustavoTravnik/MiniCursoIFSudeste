using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluffy_Quest
{
    public class Biscoito : ClasseBase
    {

        public Boolean vivo = true;

        public Biscoito(Vector2 posicao, Texture2D textura)
        {
            this.posicao = posicao;
            this.textura = textura;
        }

        public override void Update(GameTime gameTime)
        {
            if (Collision.EncostouEmFluffy(this))
            {
                vivo = false;
            }
        }

        public override void Draw(SpriteBatch render)
        {
            base.Draw(render);
        }
    }
}
