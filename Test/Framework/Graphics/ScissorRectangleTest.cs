using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NUnit.Framework;

namespace MonoGame.Tests.Graphics
{
    [TestFixture]
    internal class ScissorRectangleTest : GraphicsDeviceTestFixtureBase
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        private RenderTarget2D _extraRenderTarget;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _spriteBatch = new SpriteBatch(gd);
            _texture = content.Load<Texture2D>(Paths.Texture("Surge"));
            _extraRenderTarget = new RenderTarget2D(gd, 256, 256);
        }

        [Test]
        public void Draw_with_render_target_change()
        {
            PrepareFrameCapture();

            gd.Clear(new Color(68, 34, 136, 255));

            var renderTargets = gd.GetRenderTargets();
            gd.ScissorRectangle = new Rectangle(0, 0, 20, 20);
            gd.SetRenderTarget(_extraRenderTarget);
            gd.SetRenderTargets(renderTargets);

            DrawTexture();

            CheckFrames();
        }

        [Test]
        public void Draw_without_render_target_change()
        {
            PrepareFrameCapture();

            var renderTargets = gd.GetRenderTargets();
            gd.ScissorRectangle = new Rectangle(0, 0, 20, 20);
            gd.SetRenderTargets(renderTargets);

            DrawTexture();

            CheckFrames();
        }

        private void DrawTexture()
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            _spriteBatch.GraphicsDevice.RasterizerState = new RasterizerState { ScissorTestEnable = true };
            _spriteBatch.Draw(_texture, new Vector2(0, 0), Color.White);
            _spriteBatch.End();
        }
    }
}
