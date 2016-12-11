using OpenTK;
using Silent.Graphics.RenderEngine;
using Silent.Maths;
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
        public Matrix4 transformationMatrix = Matrix4.Identity;   

        public static int NumberOfEntities = 0;

        //Is Entity active
        public bool Active = true;

        //Name of the entity
        public string EntityName = "SampleEntity"+NumberOfEntities.ToString();

        public string modelPath = "EngineAssets/SampleCube.obj";
        

        public string texturePath = "EngineAssets/SampleTexture.png";

        public Vector3f position = new Vector3f(5f,-25,-100f);

        public Vector3f rotation = new Vector3f();

        public float scale = 1;

        //Every entity has to have a model consisting of Vertex and Texture
        private Model m_model;

        private OBJLoader m_loader = new OBJLoader();

        //The constructor takes in the model
        public Entity()
        {
            NumberOfEntities += 1;
            
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
            transformationMatrix *= MatrixMaths.CreateTransformationMatrix(position, rotation.X, rotation.Y, rotation.Z, scale);

            m_model = m_loader.loadObjModel(modelPath, texturePath);

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

        public void DeleteEntity(Entity entity)
        {
            entity.OnDeleteEntity();
        }

        public void Translate(Vector3f applyTranslation)
        {
            transformationMatrix *= Matrix4.CreateTranslation(applyTranslation.X, applyTranslation.Y, applyTranslation.Z);
        }

        public void Rotate(Vector3f rotationAxis, float rotation)
        {
            transformationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(rotationAxis.X, rotationAxis.Y, rotationAxis.Z), rotation);
        }


    }
}
