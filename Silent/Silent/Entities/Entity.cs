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

        public float shiness = 10;

        public float reflectivity = 0;

        public bool customFileType = false;

        //Is Entity active
        public bool Active = true;

        //Is entity visible
        public bool Visible = true;

        //Name of the entity
        public string EntityName = "SampleEntity"+NumberOfEntities.ToString();

        public string modelPath = "EngineAssets/SampleCube2.obj";

        public Vector3f position = new Vector3f(0,0,0);

        public Vector3f rotationAxis = new Vector3f(0,0,0);

        public float rotationAngle = 0;

        public float scale = 1;

        //Every entity has to have a model consisting of Vertex and Texture
        public Model EntityModel;

        public Material EntityMaterial;

        //private OBJLoader m_loader = new OBJLoader();

        private Entity ChildOf = null;

        //The constructor takes in the model
        public Entity()
        {
            NumberOfEntities += 1;
                      
        }

        //To be overriden by the entity instance
        public virtual void OnLoad() { }
        public virtual void OnUpdate() { }
        public virtual void OnRender() { }
        public virtual void OnClosing() { }
        public virtual void OnClosed() { }
        public virtual void OnDelete() { }
        //-------------------------------------


        //Call when the level is loading the entity
        public void OnLoadEntity()
        {
            OnLoad();
            transformationMatrix *= MatrixMaths.CreateTransformationMatrix(position, rotationAxis.X, rotationAxis.Y, rotationAxis.Z, scale);

            if (customFileType)
            {
                SilentModelFile.LoadModel(modelPath, out EntityMaterial, out EntityModel);
            }
            

            //m_model = m_loader.loadObjModel(modelPath, texturePath);

            
        }


        public void OnUpdateEntity()
        {
           if(ChildOf != null)
            {
                //transformationMatrix = Matrix4.CreateTranslation(position.X,position.Y, position.Z);
            }
            OnUpdate();
        }

        public void OnRenderEntity()
        {
            OnRender();
        }

        //Call as teh application is closing
        public void OnClosingEntity()
        {

            OnClosing();
        }

        //Call after the application has closed
        public void OnClosedEntity()
        {

            OnClosed();
        }

        //Call when it's time to delete the entity
        public void OnDeleteEntity()
        {
            //TODO: Implement deletion of Entities
            OnDelete();
        }

        //Delete the entity by choice
        public void DeleteEntity(Entity entity)
        {
            entity.OnDeleteEntity();
        }

        //Translate the entity in 3D space
        public void Translate(Vector3f applyTranslation)
        {
            /*
            position.X += applyTranslation.X;
            position.Y += applyTranslation.Y;
            position.Z += applyTranslation.Z;*/

            position += applyTranslation;


            transformationMatrix *= Matrix4.CreateTranslation(applyTranslation.X, applyTranslation.Y, applyTranslation.Z);
        }

        //Translate the entity in 3D space
        public void Translate(float positionX, float positionY, float positionZ)
        {
            position.X += positionX;
            position.Y += positionY;
            position.Z += positionZ;
            transformationMatrix *= Matrix4.CreateTranslation(positionX, positionY, positionZ);
        }

        //Rotate the entity in 3D space
        public void Rotate(Vector3f rotationAxis, float rotation)
        {
            this.rotationAxis.X += rotationAxis.X;
            this.rotationAxis.Y += rotationAxis.Y;
            this.rotationAxis.Z += rotationAxis.Z;
            this.rotationAngle += rotation;
            transformationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(rotationAxis.X, rotationAxis.Y, rotationAxis.Z), rotation);
        }

        //Rotate the entity in 3D space
        public void Rotate(float rotationAxisX, float rotationAxisY, float rotationAxisZ, float rotation)
        {
            this.rotationAxis.X += rotationAxisX;
            this.rotationAxis.Y += rotationAxisY;
            this.rotationAxis.Z += rotationAxisZ;
            this.rotationAngle += rotation;
            transformationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(rotationAxisX, rotationAxisY, rotationAxisZ), rotation);
        }

        /*
        public void SetParent(Entity parentEntity)
        {
            this.ChildOf = parentEntity;
            this.position += parentEntity.position;
        }
        */

    }
}
