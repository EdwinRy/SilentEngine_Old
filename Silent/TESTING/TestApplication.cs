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
            Silent_Game sampleGame = new SampleGame()
            {
                windowWidth = 1280,
                windowHeight = 720
            };

            sampleGame.windowBorder = Silent_Game.DisplayBorder.Resizable;
            sampleGame.MainGameLoop();          
        }
    }

    class SampleGame : Silent_Game
    {

        Silent_Input inputman = new Silent_Input(); 

        public override void OnPreloadGame()
        {
            Silent_Level lvl2 = new SampleLevel();
            this.LoadLevel(lvl2);
            this.SetCurrentLevel(lvl2);
            this.inputManager = inputman;
            lvl2.inputManager = this.inputManager;
        }

        public override void OnLoadGame()
        {
            
            
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
            base.Translate(new Vector3f(0, 0, -20));
            base.Translate(new Vector3f(0, -10, 0));
        }

        public override void OnUpdate()
        {
            //Rotate(this.position, 0.001f);
        }
    }
}
