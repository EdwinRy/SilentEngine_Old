using Silent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.GameSystem
{
    public class Level
    {
        public Level(string name = "SampleText") { m_LevelName = name; }
        private string m_LevelName = null;

        List<Entity> entities = new List<Entity>();

        public virtual void OnLoad() { }
        public virtual void OnUpdate() { }
        public virtual void OnRender() { }
        public virtual void OnClosing() { }
        public virtual void OnClosed() { }

        public void OnLoadLevel()
        {

        }

        public void OnUpdateLevel()
        {

        }

        public void OnRenderLevel()
        {

        }

        public void OnClosingLevel()
        {

        }

        public void OnClosedLevel()
        {

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
            entities.Add(Entity)
        }
    }
}
