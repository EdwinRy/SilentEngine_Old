using Silent.GameSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTING
{
    class GameManager : Silent_Game
    {
        Silent_Input input_manager = new Silent_Input();
        Silent_Level lvl1;

        public override void OnPreloadGame()
        {
            lvl1 = new Level1();

            this.LoadLevel(lvl1);
            this.SetCurrentLevel(lvl1);

            this.inputManager = input_manager;
            lvl1.inputManager = this.inputManager;

        }

    }
}
