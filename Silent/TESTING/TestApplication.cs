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
            Game sampleGame = new SampleGame()
            {
                windowWidth = 600,
                windowHeight = 400
            };

            Level lvl1 = new SampleLevel();
            Entity sampleEntity = new SampleEntity();           
            lvl1.SetLevelName("lvl1");
            lvl1.AddEntity(sampleEntity);
            sampleGame.LoadLevel(lvl1);
            sampleGame.SetCurrentLevel("lvl1");
            sampleGame.windowBorder = Game.DisplayBorder.Resizable;

            sampleGame.MainGameLoop();          
        }
    }

    //Implement Cameras

    class SampleGame : Game
    {

    }

    class SampleLevel : Level
    {
        Camera camera;
        Light light;
        bool goR = true;
        bool goL = false;
        public override void OnLoad()
        {
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
            //light.Translate(0, 0, -0.25f);
            CycleLight(light);
            Console.WriteLine(light.position.X+","+ light.position.Y + "," + light.position.Z);
            //currentCamera.Rotate(0, 0, 0, 0);
            //currentCamera.Translate(0, 0, 0.001f);
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
        bool goR = true;
        bool goL = false;

        public override void OnLoad()
        {
            base.Translate(new Vector3f(0, 0, -20));
            base.Translate(new Vector3f(0, -10, 0));

        }

        

        public override void OnUpdate()
        {
            Cycle();
            
        }

        public void Cycle()
        {
            if (goR == true)
            {
                base.Translate(new Vector3f(0.1f, 0, 0));

                if (position.X >= 10)
                {
                    goR = false;
                    goL = true;
                }
            }
            if (goL == true)
            {
                base.Translate(new Vector3f(-0.1f, 0, 0));
                if (position.X <= -10)
                {
                    goL = false;
                    goR = true;
                }
            }
        }

        
    }

}
