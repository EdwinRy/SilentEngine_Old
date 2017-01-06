using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Silent.Graphics;
using Silent.Graphics.RenderEngine;
using Silent.Graphics.Shaders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Silent.GameSystem
{

    public class Silent_Game //Use this class to extend your own classess with overriden virtual methods instead of calling this one directly
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
            //Fully supported
            OpenGL,
            //Not implemented
            Vulkan,
            //Not tested
            OpenGLES,
            //Not implemented
            WebGL
            
        }

        public Silent_Input inputManager = new Silent_Input();

        public DisplayBorder    windowBorder                    = DisplayBorder.Fixed;

        public int              windowWidth                    = 600;
        public int              windowHeight                   = 400;

        public int              windowUpdateFrameRate           = 60;
        public int              windowRenderFrameRate           = 60;
        public string           windowTitle                     = "Silent Game Engine";
        public bool             usePresetWindowUpdateFrequency  = false;
        public bool             usePresetWindowRenderFrequency  = false;
        public GameAPI          UseGameAPI                      = GameAPI.OpenGL;


        //The display the Game is using
        private GameWindow      m_gameDisplay;

        //List of levels that have been loaded to the game
        private List<Silent_Level>     levels = new List<Silent_Level>();

        //List of names Of the levels with index corresponding to the levels in levels list
        private List<string>    levelNames = new List<string>();

        //Current level in execution
        private Silent_Level m_currentLevel = null;

        //For checks if the game is already running
        public bool gameRunning = true;

        Stopwatch sw = new Stopwatch();

        public static double Framerate = 0;

        //Check if the first level was loaded
        private bool m_firstLevelLoaded = false;

        //Override functions enabling inserting additional code into the game loop
        public virtual void OnPreloadGame() { }
        public virtual void OnLoadGame() { }
        public virtual void OnUpdateGame() { }
        public virtual void OnRenderGame() { }
        public virtual void OnClosingGame() { }
        public virtual void OnClosedGame() { }

        public Silent_Game()
        {

        }

        private void ShowSplashScreen()
        {
            Bitmap bmp = new Bitmap(@"EngineAssets/Splash.png");

            GL.Enable(EnableCap.Texture2D);
            int textureID = GL.GenTexture();

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.BindTexture(TextureTarget.Texture2D, textureID);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            while (true)
            {
                GL.Begin(PrimitiveType.Quads);

                GL.TexCoord2(0.0f, 0.0f);
                GL.Vertex2(0.0f, 0.0f);
                GL.TexCoord2(1.0f, 0.0f);
                glVertex2f(m_pos.x + m_size.x, m_pos.y);
                GL.Vertex2(1.0f, 0.0f);
                glTexCoord2f(1.0f, 1.0f);
                glVertex2f(m_pos.x + m_size.x, m_pos.y + m_size.y);
                glTexCoord2f(0.0f, 1.0f);
                glVertex2f(m_pos.x, m_pos.y + m_size.y);

                glEnd();
            }

        }

        //Runs whenever the Game's loop is started
        private void OnLoad(object sender, EventArgs e)
        {
            OnPreloadGame();
            ShowSplashScreen();
            foreach(Silent_Level level in levels)
            {
                if (!m_firstLevelLoaded)
                {
                    level.OnLoadLevel();
                    m_firstLevelLoaded = true;
                }
            }
            GL.ClearColor(0.25f, 0f, 0.5f, 0f);
            GL.Viewport(new System.Drawing.Rectangle(0, 0, m_gameDisplay.Width, m_gameDisplay.Height));
            OnLoadGame();
        }

        //Execution of operations such as physics and other non-graphics related calculations etc
        private void OnUpdate(object sender, FrameEventArgs e)
        {
            if (!(m_currentLevel == null))
            {
                levels[levels.IndexOf(m_currentLevel)].OnUpdateLevel();
            }else{
                Console.WriteLine("Current Level has to be declared");
            }

            if (!gameRunning)
                m_gameDisplay.Close();

            OnUpdateGame();
            inputManager.Update();
        }

        //Drawing of the scene
        private void OnRender(object sender, FrameEventArgs e)
        {
            sw.Start();
            //GL.Flush();
            if (!(m_currentLevel == null))
            {
                levels[levels.IndexOf(m_currentLevel)].OnRenderLevel();
            }
            else
            {
                Console.WriteLine("Current level has to be declared");
            }
            OnRenderGame();
            m_gameDisplay.SwapBuffers();

            sw.Stop();

            Framerate = 1000 / sw.Elapsed.TotalMilliseconds;
            sw.Reset();
        }

        //Execute when the game is about to close
        private void OnClosing(object sender, EventArgs e)
        {
            if (!(m_currentLevel == null))
            {
                levels[levels.IndexOf(m_currentLevel)].OnClosingLevel();
            }
            else
            {
                Console.WriteLine("Current level has to be declared");
            }
            OnClosingGame();
        }

        //Execute when the game has closed
        private void OnClosed(object sender, EventArgs e)
        {
            if (!(m_currentLevel == null))
            {
                levels[levels.IndexOf(m_currentLevel)].OnClosedLevel();
            }
            else
            {
                Console.WriteLine("Current level has to be declared");
            }
            OnClosedGame();
        }

        private void KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            inputManager.KeyDown(e.Key);
        }

        private void KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            inputManager.KeyUp(e.Key);
        }

        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            inputManager.KeyPress(e.KeyChar);
        }

        private void OnResize(EventArgs e)
        {

        }

        //Load a single level to the game
        public void LoadLevel(Silent_Level levelToLoad)
        {
            if(levelToLoad.GetLevelName() == null)
            {
                Console.WriteLine("Couldn't load level due to the lack of name of the level");
            }

            levels.Add(levelToLoad);
            levelNames.Add(levelToLoad.GetLevelName());

        }

        //Load multiple levels to the game at once
        public void LoadLevels(Silent_Level[] levelsToLoad)
        {

            foreach(Silent_Level level in levelsToLoad)
            {
                if (level.GetLevelName() == null)
                {
                    Console.WriteLine("Couldn't load level due to the lack of name of the level");
                }

                levels.Add(level);
                levelNames.Add(level.GetLevelName());
            }
        }

        //Set the level currently in execution
        public void SetCurrentLevel(Silent_Level newLevel)
        {
            m_currentLevel = newLevel;
            if (m_currentLevel != null) { levels[levels.IndexOf(m_currentLevel)].OnUnloadLevel(); }
            if (m_firstLevelLoaded) { levels[levels.IndexOf(m_currentLevel)].OnLoadLevel(); m_currentLevel.inputManager = this.inputManager; }
            
        }

        //Get the name of the current level running
        public string GetCurrentLevelName()
        {
            return m_currentLevel.GetLevelName();
        }

        //The main loop
        public void MainGameLoop()
        {
            if (UseGameAPI == GameAPI.OpenGL)
            {
                m_gameDisplay = new GameWindow();
                m_gameDisplay.Load += OnLoad;
                m_gameDisplay.UpdateFrame += OnUpdate;
                m_gameDisplay.RenderFrame += OnRender;
                m_gameDisplay.Closing += OnClosing;
                m_gameDisplay.Closed += OnClosed;
                m_gameDisplay.VSync = OpenTK.VSyncMode.Adaptive;
                if (inputManager != null) {
                    m_gameDisplay.KeyDown += KeyDown;
                    m_gameDisplay.KeyUp += KeyUp;
                    m_gameDisplay.KeyPress += KeyPress;
                }
                
                if (gameRunning)
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