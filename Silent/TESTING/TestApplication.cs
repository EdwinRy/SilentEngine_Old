using Silent.Entities;
using Silent.GameSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silent.Maths;

namespace TESTING
{
    class TestApplication
    {
        public static void Main()
        {
            Game sampleGame = new SampleGame();
            Level lvl1 = new SampleLevel();
            Entity sampleEntity = new SampleEntity();
            lvl1.setLevelName("lvl1");

            lvl1.addEntity(sampleEntity);

            


            sampleGame.loadLevel(lvl1);
            sampleGame.setCurrentLevel("lvl1");
            sampleGame.windowBorder = Game.DisplayBorder.Resizable;

            sampleGame.MainGameLoop();
           
        }
    }

    class SampleGame : Game
    {
        
    }

    class SampleLevel : Level
    {

    }

    class SampleEntity : Entity
    {
       
    }

}
