using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silent.GameSystem;
using Silent.Graphics;
using static Silent.Graphics.DisplayManager;

namespace TESTING
{
    class TestGame
    {
        
        public static void Main()
        {
            Game game = new Game();
            Display display;
            DisplayManager displayManager = new DisplayManager();

            displayManager.setBorder(DisplayBorder.Hidden);

            display = displayManager.CreateDisplay();

            game.addDisplay(display);
            game.MainGameLoop();

        }

    }
}
