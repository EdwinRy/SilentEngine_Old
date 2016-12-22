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
                windowWidth = 600,
                windowHeight = 400
            };
           
            sampleGame.windowBorder = Silent_Game.DisplayBorder.Resizable;
            sampleGame.MainGameLoop();          
        }
    }

    class SampleGame : Silent_Game
    {
        public void getbinded()
        {
            Console.WriteLine("swag");
        }

        Silent_Input inputman = new Silent_Input();
        List<Silent_Input.Keys> inputs = new List<Silent_Input.Keys>();

        public override void OnPreloadGame()
        {
            Silent_Level lvl2 = new SampleLevel();
            this.LoadLevel(lvl2);
            this.SetCurrentLevel(lvl2);
        }

        public override void OnLoadGame()
        {
            inputs.Add(Silent_Input.Keys.A);
            inputman.Bind(inputs, getbinded);
            this.inputManager = inputman;
            
        }

    }

    class SampleLevel : Silent_Level
    {
        Entity sampleEntity;
        Camera camera;
        Light light;
        bool goR = true;
        bool goL = false;

        public override void OnLoad()
        {
            sampleEntity = new SampleEntity();
            this.AddEntity(sampleEntity);

            camera = new Camera();
            camera.SetCameraProjectionMatrix(shader, 600, 400);
            camera.SetCameraViewMatrix(shader);
            currentCamera = camera;

            light = new Light();
            light.Translate(0, 0, -20);
            AddLight(light);
            
        }

        public override void OnUpdate()
        {
            CycleLight(light);
            Cycle();
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
    }
}
