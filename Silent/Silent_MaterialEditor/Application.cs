using Silent.GameSystem;
using Silent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Silent_MaterialEditor
{
    public class EditorApplication  : Silent_Game
    {
        Level level;

        public override void OnPreloadGame()
        {
            level = new Level();
            LoadLevel(level);
            SetCurrentLevel(level);
            
        }

        public override void OnUpdateGame()
        {
            if (!level.console.ConsoleRunning)
                this.gameRunning = false;
        }

    }                     

    class Level : Silent_Level
    {

        public EditorManager console;

        public override void OnLoad()
        {
            console = new EditorManager();
            Thread consoleThread = new Thread(new ThreadStart(console.MainLoop));

            console.level = this;
            consoleThread.Name = "Console";
            consoleThread.Start();
        }

        public override void OnUpdate()
        {

            Console.WriteLine("r");

            if (!console.ConsoleRunning)
            {
                System.Environment.Exit(0);  
                
            }
        }
    }
    
    class EditorManager
    {

        public Silent_Level level;
        public UniversalEntity entity;
        public bool ConsoleRunning = true;

        public void MainLoop()
        {

            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form editorWindow = new Form1();
            Application.Run(editorWindow); 

            /*
            while (true)
            {
                Console.WriteLine("1.Import OBJ model");
                Console.WriteLine("2.Edit current model");
                Console.WriteLine("3.Export current model");
                Console.WriteLine("4.Import existing silent model");
                Console.WriteLine("5.Exit");
                Console.Write(":"); string input = Console.ReadLine();

                if (input == "1")
                    ImportIntoScene();

                else if (input == "2")
                    EditCurrentModel();

                else if (input == "3")
                    ExportFromScene();

                else if (input == "4")
                    ImportSilentModel();

                else if (input == "5")
                    Exit();
                else
                {
                    Console.WriteLine("Your input is invalid");
                    continue;
                }

            }  */
        }


        public void ImportIntoScene()
        {
            //string model = 

            //entity = new UniversalEntity();
        }

        public void EditCurrentModel()
        {

        }

        public void ExportFromScene()
        {

        }

        public void ImportSilentModel()
        {

        }

        public void Exit()
        {
            ConsoleRunning = false;
        }

    } 

    class UniversalEntity :  Entity
    {

    }
}
