using Silent.GameSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TESTING
{
    class TestApplication
    {
        public static void Main()
        {
            Game sampleGame = new SampleGame();
            Level lvl1 = new SampleLevel();
            lvl1.setLevelName("lvl1");

            sampleGame.loadLevel(lvl1);
            sampleGame.setCurrentLevel("lvl1");

            sampleGame.MainGameLoop();
           
        }
    }

    class SampleGame : Game
    {
        
    }

    class SampleLevel : Level
    {

    }

}
