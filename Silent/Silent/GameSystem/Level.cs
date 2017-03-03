using OpenTK.Graphics.OpenGL;
using Silent.Entities;
using Silent.Graphics.RenderEngine;
using Silent.Graphics.Shaders;
using Silent.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.GameSystem
{
    public class Silent_Level
    {

        public int ScreenWidth;
        public int ScreenHeight;

        private static int levelCount = 0;
        public string LevelName = "SampleLevel"+levelCount.ToString();

        public Silent_Level() {
            levelCount += 1;
        }

        public Silent_Camera currentCamera = new Silent_Camera();

        public List<Silent_Entity> entities = new List<Silent_Entity>();
        public List<Silent_Light> lights = new List<Silent_Light>();

        public Shader environmentalShader;
        private GLRenderer renderer = new GLRenderer();

        public virtual void OnLoad() { }
        public virtual void OnUpdate() { }
        public virtual void OnRender() { }
        public virtual void OnClosing() { }
        public virtual void OnClosed() { }
        public virtual void OnUnload() { }


        public void OnLoadLevel()
        {

            OnLoad();
            environmentalShader = new Shader("Graphics/Shaders/VertexShader.txt", "Graphics/Shaders/FragmentShader.txt");
            currentCamera.SetCameraProjectionMatrix(environmentalShader, ScreenWidth, ScreenHeight);
            currentCamera.SetCameraViewMatrix(environmentalShader);
            if (entities.Any())
            {
                LoadEntities();
            }

        }

        public void LoadEntities()
        {

            foreach (Silent_Entity entity in entities)
            {
                entity.OnLoadEntity();

                ModelLoader.LoadModel(
                    entity.EntityModelPath,
                    entity.EntityTexturePath,
                    out entity.EntityModel,
                    out entity.EntityMaterial);

                

            }
        }

        public void LoadEntity(Silent_Entity entity)
        {
            entity.OnLoadEntity();
        }

        public void OnUpdateLevel()
        {
            OnUpdate();

            if(entities.Any())
            { 
                foreach (Silent_Entity entity in entities)
                {
                    if (entity.Active)
                    {
                        entity.OnUpdateEntity();
                    }
                }
            }
            
        }

        public void OnRenderLevel()
        {
            OnRender();

            //TODO: fix the way entities are added

            if (entities.Any())
            {
                foreach (Silent_Entity entity in entities)
                {
                    if (entity.Visible)
                    {
                        renderer.PrepareToRender();
                        environmentalShader.StartShader();
                        //environmentalShader.LoadLight(lights[0]);
                        environmentalShader.LoadToTransformationMatrix(entity.EntityTransformationMatrix);
                        environmentalShader.LoadToViewMatrix(currentCamera.view);
                        //shader.LoadEntityShiness(entity.EntityMaterial.MaterialShiness, entity.EntityMaterial.MaterialReflectivity);
                        renderer.Render(entity, environmentalShader);
                        entity.OnRenderEntity();
                        environmentalShader.StopShader();
                    }
                }
            }
            else
            {
                renderer.PrepareToRender();
            }             

        }

        public void OnClosingLevel()
        {
            OnClosing();

            foreach (Silent_Entity entity in entities)
            {
                if (entity.Active)
                {
                    entity.OnClosingEntity();
                }
            };

        }

        public void OnClosedLevel()
        {
            OnClosed();

            foreach (Silent_Entity entity in entities)
            {
                if (entity.Active)
                {
                    entity.OnClosedEntity();
                }
            }

        }

        public void OnUnloadLevel()
        {
            OnUnload();

            foreach (Silent_Entity entity in entities)
            {
                if (entity.Active)
                {
                    entity.OnDeleteEntity();
                }
            }

        }

        public string GetLevelName()
        {
            if (LevelName == null)
            {
                Console.WriteLine("You have to set the level name first");
            }
            return LevelName;
        }

        public void SetLevelName(string newLevelName)
        {
            LevelName = newLevelName;
        }

        public void AddEntity(Silent_Entity entity)
        {
            entities.Add(entity);
        }

        public void AddLight(Silent_Light light)
        {
            lights.Add(light);
        }

    }
}
