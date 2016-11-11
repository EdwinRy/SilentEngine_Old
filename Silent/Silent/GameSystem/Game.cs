using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Silent.Graphics;
using Silent.Graphics.RenderEngine;
using Silent.Graphics.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.GameSystem
{

    public class Game 
    {
        //Use this class to extend your own classess with overriden virtual methods instead of calling this one directly

        //The display the Game is using
        Display gameDisplay;

        //List of levels that have been loaded to the game
        List<Level> levels = new List<Level>();

        //List of names Of the levels with index corresponding to the levels in levels list
        List<string> levelNames = new List<string>();

        //Current level in execution
        private string m_currentLevel = null;

        //For checks if the game is already running
        private bool m_gameRunning = false;

        private bool m_firstLevelLoaded = false;

        //Override functions enabling inserting additional code into the game loop
        public virtual void OnLoad() { }
        public virtual void OnUpdate() { }
        public virtual void OnRender() { }
        public virtual void OnClosing() { }
        public virtual void OnClosed() { }

        //Runs whenever the Game's loop is started
        private void OnLoadGame(object sender, EventArgs e)
        {

            foreach(Level level in levels)
            {
                level.OnPreLoad();
                level.OnPreLoadLevel();
                if (!m_firstLevelLoaded)
                {
                    level.OnLoadLevel();
                    m_firstLevelLoaded = true;
                }
            }
            GL.ClearColor(0.25f, 0f, 0.5f, 0f);

            OnLoad();
        }

        //Execution of operations such as physics and other non-graphics related calculations etc
        private void OnUpdateGame(object sender, FrameEventArgs e)
        {
            if (!(m_currentLevel == null))
            {
                levels[levelNames.IndexOf(m_currentLevel)].OnUpdateLevel();
            }else{
                Console.WriteLine("Current Level has to be declared");
            }
            OnUpdate();
        }

        //Drawing of the scene
        private void OnRenderGame(object sender, FrameEventArgs e)
        {           
            GL.Flush();
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
            if (!(m_currentLevel == null))
            {
                levels[levelNames.IndexOf(m_currentLevel)].OnRenderLevel();
            }
            else
            {
                Console.WriteLine("Current level has to be declared");
            }
            OnRender();
            gameDisplay.SwapBuffers();
        }

        //Execute when the game is about to close
        private void OnClosingGame(object sender, EventArgs e)
        {
            if (!(m_currentLevel == null))
            {
                levels[levelNames.IndexOf(m_currentLevel)].OnClosingLevel();
            }
            else
            {
                Console.WriteLine("Current level has to be declared");
            }
            OnClosing();
        }

        //Execute when the game has closed
        private void OnClosedGame(object sender, EventArgs e)
        {
            if (!(m_currentLevel == null))
            {
                levels[levelNames.IndexOf(m_currentLevel)].OnClosedLevel();
            }
            else
            {
                Console.WriteLine("Current level has to be declared");
            }
            OnClosed();
        }

        //Load a single level to the game
        public void loadLevel(Level levelToLoad)
        {
            if(levelToLoad.getLevelName() == null)
            {
                Console.WriteLine("Couldn't load level due to the lack of name of the level");
            }

            levels.Add(levelToLoad);
            levelNames.Add(levelToLoad.getLevelName());

        }

        //Load multiple levels to the game at once
        public void loadLevels(Level[] levelsToLoad)
        {

            foreach(Level level in levelsToLoad)
            {
                if (level.getLevelName() == null)
                {
                    Console.WriteLine("Couldn't load level due to the lack of name of the level");
                }

                levels.Add(level);
                levelNames.Add(level.getLevelName());
            }
        }

        //Set the level currently in execution
        public void setCurrentLevel(string newLevel)
        {
            m_currentLevel = newLevel;
            if (m_currentLevel != null) { levels[levelNames.IndexOf(m_currentLevel)].OnUnloadLevel(); }
            if (m_firstLevelLoaded) { levels[levelNames.IndexOf(m_currentLevel)].OnLoadLevel(); }
            
        }

        //Get the name of the current level running
        public string getCurrentLevel()
        {
            return m_currentLevel;
        }

        //Add Display for the game to use
        public void addDisplay(Display display)
        {

            gameDisplay                 = display;
            gameDisplay.Load            += OnLoadGame;
            gameDisplay.UpdateFrame     += OnUpdateGame;
            gameDisplay.RenderFrame     += OnRenderGame;
            gameDisplay.Closing         += OnClosingGame;
            gameDisplay.Closed          += OnClosedGame;

        }

        //The main loop
        public void MainGameLoop()
        {
            if (!m_gameRunning)
            {
                gameDisplay.EnterMainLoop();
            }else
            {
                Console.WriteLine("Game is already running");
            }
        }

    }
}
