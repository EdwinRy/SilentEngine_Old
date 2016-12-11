using OpenTK;
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
    public class Level
    {
        private string m_LevelName = null;

        public Level(string name = "SampleText") { m_LevelName = name; }

        public static float fov = 90;
        public static float nearPlane = 0.01f;
        public static float farPlane = 10000;

        List<Entity> m_entities = new List<Entity>();

        StaticShader shader;
        private GLRenderer renderer = new GLRenderer(); 


        public virtual void OnLoad() { }
        public virtual void OnUpdate() { }
        public virtual void OnRender() { }
        public virtual void OnClosing() { }
        public virtual void OnClosed() { }
        public virtual void OnUnload() { }


        public void OnLoadLevel()
        {

            shader = new StaticShader();
            OnLoad();
            foreach (Entity entity in m_entities)
            {
                if (entity.Active)
                {
                    Console.WriteLine("Loading Entity: ", entity.EntityName);
                    entity.OnLoadEntity();
                }
            }

            renderer.SetProjection(shader,fov,nearPlane,farPlane);
        }

        public void OnUpdateLevel()
        {
            OnUpdate();

            foreach (Entity entity in m_entities)
            {
                if (entity.Active)
                {
                    entity.OnUpdateEntity();
                }
            }
            
        }

        public void OnRenderLevel()
        {
            OnRender();

            //TODO: fix the way entities are added
            foreach (Entity entity in m_entities)
            {
                if (entity.Active)
                {
                    renderer.prepareToRender();
                    shader.startShader();                    
                    renderer.render(entity,shader);
                    entity.OnRenderEntity();
                    shader.stopShader();
                }
            }

        }

        public void OnClosingLevel()
        {
            OnClosing();

            foreach (Entity entity in m_entities)
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

            foreach (Entity entity in m_entities)
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

            foreach (Entity entity in m_entities)
            {
                if (entity.Active)
                {
                    entity.OnDeleteEntity();
                }
            }

        }

        public string getLevelName()
        {
            if (m_LevelName == null)
            {
                Console.WriteLine("You have to set the level name first");
            }
            return m_LevelName;
        }

        public void setLevelName(string newLevelName)
        {
            m_LevelName = newLevelName;
        }

        public void addEntity(Entity entity)
        {
            m_entities.Add(entity);
        }
    }
}
