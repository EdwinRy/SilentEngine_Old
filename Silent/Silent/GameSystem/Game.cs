using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Silent.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.GameSystem
{
    public class Game
    {

        Display gameDisplay;

        private void OnLoadGame(object sender, EventArgs e)
        {
            GL.ClearColor(0.25f, 0f, 0.5f, 0f);
        }

        private void OnUpdateGame(object sender, FrameEventArgs e)
        {

        }

        private void OnRenderGame(object sender, FrameEventArgs e)
        {
            GL.Flush();
            GL.Clear(ClearBufferMask.ColorBufferBit);

            gameDisplay.SwapBuffers();
        }

        private void OnClosingGame(object sender, EventArgs e)
        {

        }

        private void OnResize()
        {

        }

        public void addDisplay(Display display)
        {

            gameDisplay = display;
            gameDisplay.Load += OnLoadGame;
            gameDisplay.UpdateFrame += OnUpdateGame;
            gameDisplay.RenderFrame += OnRenderGame;
            gameDisplay.Closing += OnClosingGame;

            if (!gameDisplay.getResizable())
                gameDisplay.WindowBorder = WindowBorder.Hidden;

        }

        public void MainGameLoop()
        {
            gameDisplay.EnterMainLoop();
        }

    }
}
