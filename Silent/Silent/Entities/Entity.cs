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
    public class Silent_Entity
    {
        //Matrix defining the object's position in 3D space
        public Matrix4 EntityTransformationMatrix = Matrix4.Identity;

        //May be set to false if the data will be changed etc
        public bool CanBePlacedInRenderingBatch = true;

        //Number of entities in the game
        public static int NumberOfEntities = 0;

        //Whether the entity is loaded using custom .silent file
        public bool EntityUsesCustomFileType = false;

        //Is Entity active in terms of logic updates
        public bool Active = true;

        //Is entity visible
        public bool Visible = true;

        //Name of the entity
        public string EntityName = "SampleEntity"+NumberOfEntities.ToString();

        //Defines the path to the texture the model is using
        public string EntityTexturePath;

        //Path to the model
        public string EntityModelPath;

        //Position in 3D space
        public Vector3f EntityPosition = new Vector3f(0,0,0);

        //Axis of rotation (mostly uses its position)
        public Vector3f EntityRotationAxis = new Vector3f(0,0,0);

        //Angle at which the entity is rotated
        public float EntityRotationAngle = 0;

        //The scale of the entity
        public float EntityScale = 1;

        //Every entity has to have a model consisting of Vertex and Texture
        public Silent_Model EntityModel;

        //Entity's material - included in the custom model file type (has to be defined otherwise)
        public Silent_Material EntityMaterial;

        public bool CopyParentLocation = true;
        public bool CopyParentRotation = true;

        //private OBJLoader m_loader = new OBJLoader();

        private List<Silent_Entity> EntityParentOf = new List<Silent_Entity>();

        //The constructor takes in the model
        public Silent_Entity()
        {
            NumberOfEntities += 1;
                      
        }

        //To be overriden by the entity instance
        public virtual void OnLoadEntity() { } //Called once to load an entity
        public virtual void OnUpdateEntity() { } //Called whenever it's time to update logic
        public virtual void OnRenderEntity() { } //Called whenever it's time to render the entity
        public virtual void OnClosingEntity() { } //Called when it's time to close the level
        public virtual void OnClosedEntity() { } //Called after the game closed
        public virtual void OnDeleteEntity() { } //Called when entity is being deleted
        //-------------------------------------


        //Call when the level is loading the entity
        public void OnLoad()
        {
            //Call developer defined method
            OnLoadEntity();
            //Define entity position in 3D space
            EntityTransformationMatrix *= MatrixMaths.CreateTransformationMatrix(
                EntityPosition,
                EntityRotationAxis.X,
                EntityRotationAxis.Y,
                EntityRotationAxis.Z,
                EntityScale
                );
        }

        //Call when it's time to update entities' logic
        public void OnUpdate()
        {
           if(EntityParentOf.Any())
            {
                foreach(Silent_Entity entity in EntityParentOf)
                {

                }
            }
            OnUpdateEntity();
        }

        //Call when it's time to render entities
        public void OnRender()
        {
            OnRenderEntity();
        }

        //Call as the application is closing
        public void OnClosing()
        {

            OnClosingEntity();
        }

        //Call after the application has closed
        public void OnClosed()
        {

            OnClosedEntity();
        }

        //Call when it's time to delete the entity
        public void OnDelete()
        {
            //TODO: Implement deletion of Entities
            OnDeleteEntity();
        }

        //Delete the entity by choice
        public void DeleteEntity(Silent_Entity entity)
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

            EntityPosition += applyTranslation;

            //Apply translation to the translation matrix of the entity
            EntityTransformationMatrix *= Matrix4.CreateTranslation(applyTranslation.X, applyTranslation.Y, applyTranslation.Z);

            if (EntityParentOf.Any())
            {
                foreach(Silent_Entity entity in EntityParentOf)
                {
                    if(entity.CopyParentLocation)
                        entity.Translate(applyTranslation);
                }
            }
        }

        //Translate the entity in 3D space
        public void Translate(float positionX, float positionY, float positionZ)
        {
            EntityPosition.X += positionX;
            EntityPosition.Y += positionY;
            EntityPosition.Z += positionZ;
            EntityTransformationMatrix *= Matrix4.CreateTranslation(positionX, positionY, positionZ);

            if (EntityParentOf.Any())
            {
                foreach (Silent_Entity entity in EntityParentOf)
                {
                    if(entity.CopyParentLocation)
                        entity.Translate(positionX,positionY,positionZ);
                }
            }
        }

        //Rotate the entity in 3D space
        public void Rotate(Vector3f rotationAxis, float rotation)
        {
            /*
            this.EntityRotationAxis.X += rotationAxis.X;
            this.EntityRotationAxis.Y += rotationAxis.Y;
            this.EntityRotationAxis.Z += rotationAxis.Z;
            */

            EntityRotationAxis += rotationAxis;

            this.EntityRotationAngle += rotation;

            EntityTransformationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(rotationAxis.X, rotationAxis.Y, rotationAxis.Z), rotation);

            if (EntityParentOf.Any())
            {
                foreach (Silent_Entity entity in EntityParentOf)
                {
                    if (entity.CopyParentRotation)
                        entity.Rotate(rotationAxis.X, rotationAxis.Y, rotationAxis.Z, rotation);
                }
            }
        }

        //Rotate the entity in 3D space
        public void Rotate(float rotationAxisX, float rotationAxisY, float rotationAxisZ, float rotation)
        {
            this.EntityRotationAxis.X += rotationAxisX;
            this.EntityRotationAxis.Y += rotationAxisY;
            this.EntityRotationAxis.Z += rotationAxisZ;

            this.EntityRotationAngle += rotation;

            EntityTransformationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(rotationAxisX, rotationAxisY, rotationAxisZ), rotation);

            if (EntityParentOf.Any())
            {
                foreach (Silent_Entity entity in EntityParentOf)
                {
                    if(entity.CopyParentRotation)
                        entity.Rotate(rotationAxisX,rotationAxisY,rotationAxisZ, rotation);
                }
            }
        }

        //Set an entity to be the child of this entity
        public void SetParentTo(Silent_Entity ChildEntity)
        {
            EntityParentOf.Add(ChildEntity);
        }

        //Set this entity to be the child of the parameter
        public void SetChildTo(Silent_Entity ParentEntity)
        {
            ParentEntity.SetParentTo(this);
        }
        

    }
}
