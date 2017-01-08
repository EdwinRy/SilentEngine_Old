using Silent.GameSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent_MaterialEditor
{
    class Program
    {
        [STAThread]

        static void Main(string[] args)
        {
            
            Silent_Game app = new EditorApplication()
            {
                windowWidth = 600,
                windowHeight = 400
            };
            app.windowBorder = Silent_Game.DisplayBorder.Resizable;
            app.MainGameLoop();
        }
    }
}
