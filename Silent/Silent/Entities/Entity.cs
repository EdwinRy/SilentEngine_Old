using Silent.Graphics.RenderEngine;
using Silent.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Entities
{
    public class Entity
    {
        //Is Entity active
        private bool m_active;

        //Name of the entity
        private string m_EntityName;

        private string m_modelPath;

        private string m_texturePath;

        //Every entity has to have a model consisting of Vertex and Texture
        private Model m_model;

        private OBJLoader m_loader = new OBJLoader();

        //The constructor takes in the model
        public Entity(string name = "SampleText", string modelPath = "EngineAssets/SampleCube.obj", string texturePath = "EngineAssets/SampleTexture.png", bool active = true)
        {
            m_EntityName = name;
            m_modelPath = modelPath;
            m_texturePath = texturePath;
            m_active = active;
        }

        public virtual void OnLoad() { }
        public virtual void OnUpdate() { }
        public virtual void OnRender() { }
        public virtual void OnClosing() { }
        public virtual void OnClosed() { }
        public virtual void OnDelete() { }

        public void OnLoadEntity()
        {
            //TODO: add a model on load

            m_model = m_loader.loadObjModel(m_modelPath, m_texturePath);

            OnLoad();
        }

        public void OnUpdateEntity()
        {
           
            OnUpdate();
        }

        public void OnRenderEntity()
        {
            OnRender();
        }

        public void OnClosingEntity()
        {

            OnClosing();
        }

        public void OnClosedEntity()
        {

            OnClosed();
        }

        public void OnDeleteEntity()
        {
            //TODO: Implement deletion of Entities
            OnDelete();
        }

        //The entity returns the model to enable rendering the model
        public Model getModel()
        {
            return m_model;
        }

        //Return name of the entity
        public string getEntityName()
        {
            return m_EntityName;
        }

        //Return whether the entity is active or not
        public bool getActive()
        {
            return m_active;
        }

        //Set the activity of the entity
        public void setActive(bool active)
        {
            m_active = active;

            if (active) { OnLoadEntity(); }

        }

        public void DeleteEntity(Entity entity)
        {
            entity.OnDeleteEntity();
        }

    }
}
