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

namespace CGs
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameRaytrace : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D texture;
        Rectangle rect;
        Color[] data;
        Renderer renderer;
        int sizeX, sizeY;

        public GameRaytrace()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            // ウインドウサイズを変更
            graphics.PreferredBackBufferHeight = graphics.PreferredBackBufferWidth;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // レンダラの初期化
            renderer = new Raytrace2.Raytrace(); 
            renderer.Initialize(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferWidth);

            sizeX = sizeY = graphics.PreferredBackBufferWidth;

            // 描画する画像の初期化
            texture = new Texture2D(graphics.GraphicsDevice, sizeX, sizeY);
            data = new Color[sizeX * sizeY];
            rect = new Rectangle(0, 0, sizeX, sizeY);
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    Color c = Color.White;
                    data[x + y * sizeX] = c;
                }
            }
            texture.SetData(data);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            // ウインドウタイトルの更新
            if (0 < gameTime.ElapsedGameTime.TotalSeconds)
            {
                float fps = (float)(1 / gameTime.ElapsedGameTime.TotalSeconds);
                Window.Title = "CGs(" + fps + ")";
            }

            // 描画画像の更新
            if (renderer.Draw(data, sizeX, sizeY) == false) Exit();
            GraphicsDevice.Textures[0] = null;  // これをしないでtexture.SetDataを呼ぶと例外が発生する
            texture.SetData(data);
            
            // 描画
            spriteBatch.Begin();
            spriteBatch.Draw(texture, rect, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
