//-------------------------------------------------------------------
// 
//-------------------------------------------------------------------

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
    public class GameHLSL2 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Matrix
        Matrix projection, view, world;
        Matrix projection2, view2, world2;
        // Camera
        Vector3 eye = new Vector3(2, 2, 2);
        Vector3 target = Vector3.Zero;
        Vector3 up = Vector3.Up;
        // Effect
        Effect effect, effect2;
        // Vertex/Index buffer
        VertexPositionTexture[] texVertices = new VertexPositionTexture[] {
                new VertexPositionTexture(new Vector3(-1, -1, 0), new Vector2(0, 0)),
                new VertexPositionTexture(new Vector3(1, -1, 0), new Vector2(1, 0)),
                new VertexPositionTexture(new Vector3(-1, 1, 0), new Vector2(0, 1)),
                new VertexPositionTexture(new Vector3(1, 1, 0), new Vector2(1, 1))
            };
        int[] texVertexIndices = new int[] {
                0, 1, 3,
                0, 3, 2,
                0, 3, 1,
                0, 2, 3
            };
        VertexPositionColor[] vertices = new VertexPositionColor[] {
                new VertexPositionColor(new Vector3(-0.5f, 0.5f, -0.5f), Color.Red),
                new VertexPositionColor(new Vector3(0.5f, 0.5f, -0.5f), Color.Green),
                new VertexPositionColor(new Vector3(-0.5f, 0.5f, 0.5f), Color.Blue),
                new VertexPositionColor(new Vector3(0.5f, 0.5f, 0.5f), Color.Cyan),
                new VertexPositionColor(new Vector3(-0.5f, -0.5f, -0.5f), Color.Magenta),
                new VertexPositionColor(new Vector3(0.5f, -0.5f, -0.5f), Color.Yellow),
                new VertexPositionColor(new Vector3(-0.5f, -0.5f, 0.5f), Color.Black),
                new VertexPositionColor(new Vector3(0.5f, -0.5f, 0.5f), Color.White)
            };
        int[] vertexIndices = new int[] {
                2, 0, 1, 
                1, 3, 2, 
                4, 0, 2, 
                2, 6, 4, 
                5, 1, 0,
                0, 4, 5,
                7, 3, 1, 
                1, 5, 7,
                6, 2, 3, 
                3, 7, 6,
                4, 6, 7, 
                7, 5, 4,
            };
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;
        VertexBuffer texVertexBuffer;
        IndexBuffer texIndexBuffer;
        // angle
        float angle = 0;
        float angle2 = 0;
        // BG
        Color bgColor = new Color(125, 125, 255, 255);
        // RenderTarget
        RenderTarget2D renderTarget;

        public GameHLSL2()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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


            base.Initialize();
        }

        void InitializeCamera()
        {
            float aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.1f, 1000.0f);
            view = Matrix.CreateLookAt(eye, target, up);
            world = Matrix.Identity;
            projection2 = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.1f, 1000.0f);
            view2 = Matrix.CreateLookAt(eye, target, up);
            world2 = Matrix.Identity;
        }

        void InitializeEffect()
        {
            effect = Content.Load<Effect>("Shader/sample");
            effect.CurrentTechnique = effect.Techniques["Render"];
            effect2 = Content.Load<Effect>("Shader/sample2");
            effect2.CurrentTechnique = effect2.Techniques["Render"];
        }

        void InitializeCube()
        {
            // 頂点・インデックスバッファを初期化
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), vertices.Length, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionColor>(vertices);
            indexBuffer = new IndexBuffer(GraphicsDevice, IndexElementSize.ThirtyTwoBits, vertexIndices.Length, BufferUsage.None);
            indexBuffer.SetData<int>(vertexIndices);

            // 頂点・インデックスバッファを初期化
            texVertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionTexture), texVertices.Length, BufferUsage.None);
            texVertexBuffer.SetData<VertexPositionTexture>(texVertices);
            texIndexBuffer = new IndexBuffer(GraphicsDevice, IndexElementSize.ThirtyTwoBits, texVertexIndices.Length, BufferUsage.None);
            texIndexBuffer.SetData<int>(texVertexIndices);
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
            InitializeCamera();
            InitializeEffect();
            InitializeCube();

            renderTarget = new RenderTarget2D(GraphicsDevice, 400, 200);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            effect.Dispose();
            effect2.Dispose();
            vertexBuffer.Dispose();
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
            if ((angle += 2.5f) >= 360.0f) angle = 0.0f;
            Matrix.CreateRotationY(MathHelper.ToRadians(-angle), out world);
            Matrix.CreateRotationX(MathHelper.ToRadians(angle), out world2);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here

            // ウインドウタイトルの更新
            if (0 < gameTime.ElapsedGameTime.TotalSeconds)
            {
                float fps = (float)(1 / gameTime.ElapsedGameTime.TotalSeconds);
                Window.Title = "CGs(" + fps + ")";
            }

            // シェーダにパラメータをバインド
            effect.Parameters["Projection"].SetValue(projection);
            effect.Parameters["View"].SetValue(view);
            effect.Parameters["World"].SetValue(world);

            // シェーダを適用して描画
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                GraphicsDevice.SetVertexBuffer(vertexBuffer);
                GraphicsDevice.Indices = indexBuffer;
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertices.Length, 0, vertexIndices.Length / 3);
            }

            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.Clear(bgColor);

            effect2.Parameters["Projection"].SetValue(projection2);
            effect2.Parameters["View"].SetValue(view2);
            effect2.Parameters["World"].SetValue(world2);
            effect2.Parameters["tex0"].SetValue(renderTarget);

            // シェーダを適用して描画
            foreach (var pass in effect2.CurrentTechnique.Passes)
            {
                pass.Apply();

                GraphicsDevice.SetVertexBuffer(texVertexBuffer);
                GraphicsDevice.Indices = texIndexBuffer;
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, texVertices.Length, 0, texVertexIndices.Length / 3);
            }

            base.Draw(gameTime);
        }
    }
}
