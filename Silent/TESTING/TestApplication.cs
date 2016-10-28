using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silent.GameSystem;
using Silent.Graphics;
using static Silent.Graphics.DisplayManager;
using Silent.Entities;
using Silent.Tools;

namespace TESTING
{
    class TestApplication
    {
        
        public static void Main()
        {
            Game game = new TestGame();
            Level lvl1 = new TestLevel();

            lvl1.setLevelName("swagLVL");

            game.loadLevel(lvl1);

            game.setCurrentLevel("swagLVL");

            Display display;
            DisplayManager displayManager = new DisplayManager();

            displayManager.setBorder(DisplayBorder.Hidden); 

            display = displayManager.CreateDisplay();

            game.addDisplay(display);
            game.MainGameLoop();

        }

    }

    class TestGame : Game
    { }
        

    class TestLevel : Level
    {
        public override void OnUpdate()
        {
            Console.WriteLine("swag");
        }
    }
}
