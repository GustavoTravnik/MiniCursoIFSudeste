using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluffy_Quest
{
    public class Grama : ClasseBase
    {

        public Grama(Vector2 posicao, Texture2D textura)
        {
            this.posicao = posicao;
            this.textura = textura;
        }

        public override void Draw(SpriteBatch render)
        {
            base.Draw(render);
        }
    }
}
