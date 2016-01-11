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
        public Texture2D texturaGrama, texturaPedra, textureFluffy;

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

        private void CriarFluffy()
        {
            fluffy = new Fluffy(Vector2.Zero, textureFluffy, pedras);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texturaGrama = Content.Load<Texture2D>("grama");
            texturaPedra = Content.Load<Texture2D>("pedra");
            textureFluffy = Content.Load<Texture2D>("fluffy");
            CriarCenario();
            CriarFluffy();
        }

        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            fluffy.Update(gameTime);
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

            fluffy.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
