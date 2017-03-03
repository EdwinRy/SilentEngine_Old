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

    class SampleGame
    {
        public static void Main(string[] args)
        {
            Game game = new Game();
            Lvl level = new Lvl();
            game.LoadLevel(level);
            game.SetCurrentLevel(level);

            game.MainGameLoop();  
        }
    }

    class Game : Silent_Game
    {
        public float r = 1;
        public float g = 0;
        public float b = 0;

        public string currentFlow = "rg";

        public override void OnUpdateGame()
        {
            if(currentFlow == "rg")
            {
                r -= 0.005f;
                g += 0.005f;
                SetBackgroundColour(r, g, b, 1);
                if(g >= 1)
                {
                    g = 1;
                    currentFlow = ("gb");
                }
            }

            if(currentFlow == "gb")
            {
                g -= 0.005f;
                b += 0.005f;
                SetBackgroundColour(r, g, b, 1);
                if (b >= 1)
                {
                    b = 1;
                    currentFlow = ("br");
                }
            }

            if (currentFlow == "br")
            {
                b -= 0.005f;
                r += 0.005f;
                SetBackgroundColour(r, g, b, 1);
                if (r >= 1)
                {
                    r = 1;
                    currentFlow = ("rg");
                }
            }
        }
    }

    class Lvl : Silent_Level
    {

        Entity entity = new Entity();
        public override void OnLoad()
        {
            Silent_Camera camera = new Silent_Camera();
            currentCamera = camera;
            entity.EntityModelPath = "Assets/dragon.obj";
            entity.EntityTexturePath = "Assets/Dragon_Blue.png";
            entity.Translate(0, -2, -10);
            this.AddEntity(entity);
        }

    }

    class Entity : Silent_Entity
    {

    }

    /*
    class TestApplication
    {
        public static void Main()
        {
            Silent_Game sampleGame = new SampleGame()
            {
                windowWidth = 1280,
                windowHeight = 720
            };

            sampleGame.windowBorder = Silent_Game.DisplayBorder.Resizable;
            sampleGame.MainGameLoop();          
        }
    }


    class SampleLevel : Silent_Level
    {
        Entity sampleEntity;
        Camera camera;
        Light light;
        bool goR = true;
        bool goL = false;

        public void goRight()
        {
            sampleEntity.Translate(new Vector3f(0.1f, 0, 0));
        }
        public void goLeft()
        {
            sampleEntity.Translate(new Vector3f(-0.1f, 0, 0));
        }

        public void goUp()
        {
            sampleEntity.Translate(new Vector3f(0, 0.1f, 0));
        }

        public void goDown()
        {
            sampleEntity.Translate(new Vector3f(0, -0.1f, 0));
        }

        public void goForward()
        {
            sampleEntity.Translate(new Vector3f(0, 0, -0.1f));
        }

        public void goBackward()
        {
            sampleEntity.Translate(new Vector3f(0, 0, 0.1f));
        }

        public override void OnLoad()
        {
            List<Silent_Input.Keys> r = new List<Silent_Input.Keys>();
            List<Silent_Input.Keys> l = new List<Silent_Input.Keys>();
            List<Silent_Input.Keys> u = new List<Silent_Input.Keys>();
            List<Silent_Input.Keys> d = new List<Silent_Input.Keys>();
            List<Silent_Input.Keys> f = new List<Silent_Input.Keys>();
            List<Silent_Input.Keys> b = new List<Silent_Input.Keys>();
            r.Add(Silent_Input.Keys.Right);
            l.Add(Silent_Input.Keys.Left);
            u.Add(Silent_Input.Keys.Space);
            d.Add(Silent_Input.Keys.LControl);
            f.Add(Silent_Input.Keys.Up);
            b.Add(Silent_Input.Keys.Down);
            inputManager.Bind(r, goRight);
            inputManager.Bind(l, goLeft);
            inputManager.Bind(u, goUp);
            inputManager.Bind(d, goDown);
            inputManager.Bind(f, goForward);
            inputManager.Bind(b, goBackward);
            sampleEntity = new SampleEntity();

            sampleEntity.customFileType = false;

            this.AddEntity(sampleEntity);

            camera = new Camera();
            camera.SetCameraProjectionMatrix(shader, 1280, 720);
            camera.SetCameraViewMatrix(shader);
            currentCamera = camera;

            light = new Light();
            light.Translate(0, 0, -20);
            AddLight(light);
            
        }

        public override void OnUpdate()
        {
            //CycleLight(light);
            //Cycle();
        }

        public void Cycle()
        {
            if (goR == true)
            {
                sampleEntity.Translate(new Vector3f(0.1f, 0, 0));

                if (sampleEntity.position.X >= 10)
                {
                    goR = false;
                    goL = true;
                }
            }
            if (goL == true)
            {
                sampleEntity.Translate(new Vector3f(-0.1f, 0, 0));
                if (sampleEntity.position.X <= -10)
                {
                    goL = false;
                    goR = true;
                }
            }
        }

        public void CycleLight(Light light)
        {
            if (goR == true)
            {
                light.Translate(new Vector3f(0, 0, -0.25f));
                if (light.position.Z <= -200)
                {
                    
                    goR = false;
                    goL = true;
                }
            }
            if (goL == true)
            {
                light.Translate(new Vector3f(0, 0, 0.25f));
                if (light.position.Z >= 0)
                {
                    goL = false;
                    goR = true;
                }
            }
        }
    }

    class SampleEntity : Entity
    {
        public override void OnLoad()
        {
            customFileType = false;
            base.Translate(new Vector3f(0, 0, -20));
            base.Translate(new Vector3f(0, -10, 0));
        }

        public override void OnUpdate()
        {
            //Rotate(this.position, 0.001f);
        }
    } */

}
