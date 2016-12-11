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

    public class Game //Use this class to extend your own classess with overriden virtual methods instead of calling this one directly
    {

        //WINDOW PROPERTIES:

        public enum DisplayBorder
        {
            Resizable,
            Fixed,
            Hidden
        }

        public enum GameAPI
        {
            OpenGL,
            OpenGLES,
            WebGL,
            Vulkan
        }

        public DisplayBorder    windowBorder                    = DisplayBorder.Fixed;
        public static int       windowWidth                     = 600;
        public static int       windowHeight                    = 400;
        public int              windowUpdateFrameRate           = 60;
        public int              windowRenderFrameRate           = 60;
        public string           windowTitle                     = "Silent Game Engine";
        public bool             usePresetWindowUpdateFrequency  = false;
        public bool             usePresetWindowRenderFrequency  = false;
        public GameAPI          UseGameAPI                      = GameAPI.OpenGL;


        //The display the Game is using
        private GameWindow      m_gameDisplay;

        //List of levels that have been loaded to the game
        private List<Level>     levels = new List<Level>();

        //List of names Of the levels with index corresponding to the levels in levels list
        private List<string>    levelNames = new List<string>();

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

        public Game()
        {

        }

        //Runs whenever the Game's loop is started
        private void OnLoadGame(object sender, EventArgs e)
        {

            foreach(Level level in levels)
            {
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
            //GL.Flush();
            if (!(m_currentLevel == null))
            {
                levels[levelNames.IndexOf(m_currentLevel)].OnRenderLevel();
            }
            else
            {
                Console.WriteLine("Current level has to be declared");
            }
            OnRender();
            m_gameDisplay.SwapBuffers();
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

        //The main loop
        public void MainGameLoop()
        {
            if (UseGameAPI == GameAPI.OpenGL)
            {
                m_gameDisplay = new GameWindow();
                m_gameDisplay.Load += OnLoadGame;
                m_gameDisplay.UpdateFrame += OnUpdateGame;
                m_gameDisplay.RenderFrame += OnRenderGame;
                m_gameDisplay.Closing += OnClosingGame;
                m_gameDisplay.Closed += OnClosedGame;

                if (!m_gameRunning)
                {
                    if (windowBorder == DisplayBorder.Resizable)
                        m_gameDisplay.WindowBorder = WindowBorder.Resizable;

                    if (windowBorder == DisplayBorder.Fixed)
                        m_gameDisplay.WindowBorder = WindowBorder.Fixed;

                    if (windowBorder == DisplayBorder.Hidden)
                        m_gameDisplay.WindowBorder = WindowBorder.Hidden;

                    m_gameDisplay.Width = windowWidth;
                    m_gameDisplay.Height = windowHeight;
                    m_gameDisplay.Title = windowTitle;



                    if (usePresetWindowUpdateFrequency)
                    {

                        if (usePresetWindowRenderFrequency)
                        {
                            m_gameDisplay.Run(windowUpdateFrameRate, windowRenderFrameRate);
                        }

                        else
                        {
                            m_gameDisplay.Run(windowUpdateFrameRate);
                        }
                    }
                    else
                        m_gameDisplay.Run();

                    if (!usePresetWindowUpdateFrequency && usePresetWindowRenderFrequency)
                    {
                        Console.WriteLine("Update frequency required in order to change render frequency");
                        m_gameDisplay.Run();
                    }
                }
            }


        }

    }
}