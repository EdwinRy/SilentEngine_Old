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

        private GLRenderer renderer = new GLRenderer();

        List<Entity> m_entities = new List<Entity>();

        StaticShader shader;

        public virtual void OnPreLoad() { }
        public virtual void OnLoad() { }
        public virtual void OnUpdate() { }
        public virtual void OnRender() { }
        public virtual void OnClosing() { }
        public virtual void OnClosed() { }
        public virtual void OnUnload() { }

        public void OnPreLoadLevel()
        {

            Console.WriteLine("Preload");
            OnPreLoad();

        }
        public void OnLoadLevel()
        {
            shader = new StaticShader();
            Console.WriteLine("Load start");
            OnLoad();
            Console.WriteLine("Load continue");
            foreach (Entity entity in m_entities)
            {
                Console.WriteLine(entity.getActive());
                if (entity.getActive())
                {
                    Console.WriteLine("Loading Entity: ", entity.getEntityName());
                    entity.OnLoadEntity();
                }
            }
            

        }

        public void OnUpdateLevel()
        {
            OnUpdate();

            foreach (Entity entity in m_entities)
            {
                if (entity.getActive())
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
                if (entity.getActive())
                {
                    shader.startShader();
                    renderer.render(entity);
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
                if (entity.getActive())
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
                if (entity.getActive())
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
                if (entity.getActive())
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
