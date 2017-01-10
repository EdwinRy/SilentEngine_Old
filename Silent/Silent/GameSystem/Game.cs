using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Silent.Entities;
using Silent.Graphics.RenderEngine;
using Silent.Graphics.Shaders;
using Silent.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

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

        //Used to execute functions or methods when keyboard state matches bindings
        public Silent_Input inputManager = new Silent_Input();

        //Set the default window border type to be fixed so the window shows but can't be resized
        public DisplayBorder    windowBorder                    = DisplayBorder.Fixed;

        //Set the default window width to 600px
        public int              windowWidth                    = 600;
        //Set the default window height to 400px
        public int              windowHeight                   = 400;

        //Set the logic update frequency
        public int              windowUpdateFrameRate           = 60;
        //Set the render frequency
        public int              windowRenderFrameRate           = 60;
        //Set the default title of the window
        public string           windowTitle                     = "Silent Game Engine";
        //Try to update as many times a second as defined by windowUpdateFrameRate
        public bool             usePresetWindowUpdateFrequency  = false;
        //Try to update as many times a second as defined by windowUpdateFrameRate
        public bool             usePresetWindowRenderFrequency  = false;
        //Use OpenGL by default
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

        //Timer used to determine the framerate
        Stopwatch sw = new Stopwatch();
        
        //Frequency of rendering (how many frames are rendered per second)
        public static double RenderFramerate = 0;

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
            GLModelLoader loader = new GLModelLoader();

            //float[] SplashVertices = { -1, 0, 1, 1, 0, 1, -1, 0, -1, 1, 0, -1 };
            //float[] SplashTextureCoordinates = { 0.0001f, 0.9999f, 0.9999f, 0.9999f, 0.0001f, 0.0001000166f, 0.9999f, 0.0001000166f };
            //float[] SplashNormals = { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 };
            //int[] SplashIndices = { 1, 2, 0, 1, 3, 2 };


            
            Silent_Entity SplashQuad = new Silent_Entity();
            SplashQuad.EntityModel = new Silent_Model();
            //SplashQuad.EntityModel.Vertices = SplashVertices;
            //SplashQuad.EntityModel.TextureCoords = SplashTextureCoordinates;
            //SplashQuad.EntityModel.Normals = SplashNormals;
            //SplashQuad.EntityModel.Indices = SplashIndices;

            SplashQuad.EntityMaterial = new Silent_Material();
            SplashQuad.EntityMaterial.TexturePath = "EngineAssets/SplashScreen.png";

            SplashQuad.EntityModel.ModelTexture = loader.LoadTexture("EngineAssets/SplashScreen.png");
            //SplashQuad.EntityModel.ModelVertex = loader.Load(
              //   SplashQuad.EntityModel.Vertices,
              //   SplashQuad.EntityModel.Indices,
              //   SplashQuad.EntityModel.TextureCoords,
              //   SplashQuad.EntityModel.Normals
              //  );
            
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.Black);
            GL.BindTexture(TextureTarget.Texture2D, SplashQuad.EntityModel.ModelTexture.GetTextureID());
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex2(-1, -1); //Bottom Left
            GL.TexCoord2(0, 0);
            GL.Vertex2(-1, 1); //Top Left
            GL.TexCoord2(1, 0);
            GL.Vertex2(1, 1); //Top Right
            GL.TexCoord2(1, 1);
            GL.Vertex2(1, -1); //Bottom Right
            GL.End();
            m_gameDisplay.SwapBuffers();
            sw.Start();
            while (sw.Elapsed.TotalSeconds < 2)
            {
                
            }
            sw.Stop();

        }  

        //Runs whenever the Game's loop is started
        private void OnLoad(object sender, EventArgs e)
        {
            ShowSplashScreen();
            OnPreloadGame();
            
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

            RenderFramerate = 1000 / sw.Elapsed.TotalMilliseconds;
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