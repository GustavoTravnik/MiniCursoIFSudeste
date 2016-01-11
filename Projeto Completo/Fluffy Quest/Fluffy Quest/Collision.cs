using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluffy_Quest
{
    public static class Collision
    {
        public static Fluffy fluffy;

        public static Boolean EncostouEmFluffy(ClasseBase objeto)
        {
            return CreateSourceRectangle(fluffy.posicao).Intersects(CreateSourceRectangle(objeto.posicao));
        }

        private static Boolean IsCrash(List<Pedra> target, float speedX, float speedY, Rectangle sourceRectangle)
        {
            Boolean bump = false;
            foreach (Pedra pedra in target)
            {
                Rectangle retanguloTarget = new Rectangle((int)pedra.posicao.X, (int)pedra.posicao.Y, 50, 50);
                Rectangle retanguloPosicaoFrente = GetFutureRectangle(sourceRectangle, speedX, speedY);
                if (IsCollide(retanguloPosicaoFrente, retanguloTarget))
                {
                    bump = true;
                    break;
                }
            }
            return bump;
        }

        private static Boolean IsSourceBash(Vector2 source, List<Pedra> target, float speedX, float speedY)
        {
            Rectangle retanguloSource = CreateSourceRectangle(source);
            return IsCrash(target, speedX, speedY, retanguloSource);
        }

        public static Boolean WillCollideRight(Vector2 source, List<Pedra> target, float speed)
        {
            return IsSourceBash(source, target, speed, 0);
        }

        public static Boolean WillCollideLeft(Vector2 source, List<Pedra> target, float speed)
        {
            return IsSourceBash(source, target, -speed, 0);
        }

        public static Boolean WillCollideDown(Vector2 source, List<Pedra> target, float speed)
        {
            return IsSourceBash(source, target, 0, speed);
        }

        public static Boolean WillCollideUp(Vector2 source, List<Pedra> target, float speed)
        {
            return IsSourceBash(source, target, 0, -speed);
        }

        private static Boolean IsCollide(Rectangle source, Rectangle target)
        {
            return source.Intersects(target);
        }

        private static Rectangle GetFutureRectangle(Rectangle source, float x, float y)
        {
            return new Rectangle(source.X + (int)x, source.Y + (int)y, source.Width, source.Height);
        }

        private static Rectangle CreateSourceRectangle(Vector2 source)
        {
            return new Rectangle((int)source.X + 10, (int)source.Y + 10, 40, 40);
        }
    }
}
