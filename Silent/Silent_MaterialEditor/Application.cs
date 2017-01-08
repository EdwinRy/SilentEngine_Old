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
using Silent.Tools;

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
            if (!EditorManager.ConsoleRunning)
                this.gameRunning = false;
        }

    }                     

    public class Level : Silent_Level
    {
        OpenFileDialog fileChoose;
        public EditorManager console;
        Thread consoleThread;

        public void AddUniversalEntity()
        {
            
            fileChoose = new OpenFileDialog();

            //consoleThread.Suspend();
            fileChoose.ShowDialog();
            //consoleThread.Resume();

            UniversalEntity tempEntity = new UniversalEntity();

            OBJModelLoader.Load(
                fileChoose.FileName,
                out tempEntity.EntityModel
                );

            EditorManager.entity = tempEntity;
        }

        public override void OnLoad()
        {
            console = new EditorManager();
            consoleThread = new Thread(new ThreadStart(console.MainLoop));

            EditorManager.level = this;
            consoleThread.Name = "Console";
            consoleThread.Start();
        }

        public override void OnUpdate()
        {

            if (!EditorManager.ConsoleRunning)
            {
                System.Environment.Exit(0);  
                
            }

            if(EditorManager.entity != null)
            {
                Console.WriteLine("not null");
            }
        }
    }
    
    public class EditorManager
    {

        public static Level level;
        public static UniversalEntity entity;
        public static bool ConsoleRunning = true;

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


        public void ImportIntoScene(UniversalEntity entity)
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

    public class UniversalEntity :  Entity
    {

    }
}
