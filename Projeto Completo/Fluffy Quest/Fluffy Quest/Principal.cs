using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Fluffy_Quest
{

    public class Principal : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Fluffy fluffy;
        List<Grama> gramas = new List<Grama>();
        List<Pedra> pedras = new List<Pedra>();
        List<Biscoito> biscoitos = new List<Biscoito>();
        public Texture2D texturaGrama, texturaPedra, texturaFluffy, texturaBiscoito;
        int biscoitosColetados = 0;
        SpriteFont fonte;

        public Principal()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1300;
            graphics.PreferredBackBufferHeight = 750;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        private readonly Object lockObj = new Object();
        private readonly Random random = new Random();

        private int GetRandomNumber(int min, int max)
        {
            lock (lockObj)
            {
                return random.Next(min-1, max+1);
            }
        }

        private void CriarCenario()
        {
            for (int i = 0; i < graphics.PreferredBackBufferWidth; i += 50)
            {
                for (int j = 0; j < graphics.PreferredBackBufferHeight; j += 50)
                {
                    CriarGramas(i, j);
                    CriarPedras(i, j);
                    CriarBiscoitos(i, j);
                }
            }
        }

        private void CriarGramas(int i, int j)
        {
            Vector2 posicao = new Vector2(i, j);
            Grama grama = new Grama(posicao, texturaGrama);
            gramas.Add(grama);
        }

        private void CriarPedras(int i, int j)
        {
            if (GetRandomNumber(0, 5) == 5 && i != 0 && j != 0)
            {
                Vector2 posicao = new Vector2(i, j);
                Pedra pedra = new Pedra(posicao, texturaPedra);
                pedras.Add(pedra);
            }
        }

        private void CriarBiscoitos(int i, int j)
        {
            if (GetRandomNumber(0, 5) == 2 && i != 0 && j != 0)
            {
                Vector2 posicao = new Vector2(i, j);
                if (!JaExisteUmaPedraEm(posicao))
                {
                    Biscoito biscoito = new Biscoito(posicao, texturaBiscoito);
                    biscoitos.Add(biscoito);
                }
            }
        }

        private Boolean JaExisteUmaPedraEm(Vector2 posicao)
        {
            foreach (Pedra pedra in pedras)
            {
                if (pedra.posicao.Equals(posicao))
                {
                    return true;
                }
            }
            return false;
        }

        private void CriarFluffy()
        {
            fluffy = new Fluffy(Vector2.Zero, texturaFluffy, pedras);
            Collision.fluffy = fluffy;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texturaGrama = Content.Load<Texture2D>("grama");
            texturaPedra = Content.Load<Texture2D>("pedra");
            texturaFluffy = Content.Load<Texture2D>("fluffy");
            texturaBiscoito = Content.Load<Texture2D>("biscoito");
            fonte = Content.Load<SpriteFont>("fonte");
            CriarCenario();
            CriarFluffy();
        }

        private void DesenharPlacar(SpriteBatch render)
        {
            render.DrawString(fonte, "Biscoitos Coletados: " + biscoitosColetados.ToString(), Vector2.Zero, Color.White);
        }

        private void AtualizarBiscoitos(GameTime gameTime)
        {
            for (int i = 0; i < biscoitos.Count; i++)
            {
                biscoitos[i].Update(gameTime);
                if (!biscoitos[i].vivo)
                {
                    biscoitosColetados++;
                    biscoitos.Remove(biscoitos[i]);
                    i--;
                }
            }
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            fluffy.Update(gameTime);
            AtualizarBiscoitos(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();

            foreach (Grama grama in gramas)
            {
                grama.Draw(spriteBatch);
            }

            foreach (Pedra pedra in pedras)
            {
                pedra.Draw(spriteBatch);
            }

            foreach (Biscoito biscoito in biscoitos)
            {
                biscoito.Draw(spriteBatch);
            }

            fluffy.Draw(spriteBatch);

            DesenharPlacar(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
