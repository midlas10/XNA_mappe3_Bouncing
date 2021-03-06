using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Bouncing
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ObjectManager
    {

        protected List<GameObject> objects;

        protected Queue<GameObject> objectsToRemove;


        protected SpriteBatch spriteBatch;
        private PlayScreen playScreen;

        public ObjectManager(Game game)
        {
            objects = new List<GameObject>();
            objectsToRemove = new Queue<GameObject>();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            //First remove all the objects queued for removal.
            while (objectsToRemove.Count > 0)
            {
                objects.Remove(objectsToRemove.Dequeue());
            }
            //Then update all the objects.
            for (int i = 0; i < objects.Count; i++)
            {
                GameObject go = objects[i];
                go.Update(gameTime);
            }

        }

        public void Draw(GameTime gameTime)
        {
            
            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            //Draw all the objects in the objectlist);
            foreach (GameObject go in objects)
            {
                go.Draw(gameTime);
            }
            
           // spriteBatch.End();
        }

        /// <summary>
        /// Registers an object in the objectmanager so that it will be 
        /// updated and drawn.
        /// </summary>
        /// <param name="obj">The object you want to register.</param>
        public void RegisterObject(GameObject obj)
        {
            objects.Add(obj);
        }

        /// <summary>
        /// Queue an object for removal during the next update phase.
        /// </summary>
        /// <param name="obj">The object you want removed.</param>
        public void UnregisterObject(GameObject obj)
        {
            objectsToRemove.Enqueue(obj);
        }

        public void RemoveAllObjects()
        {
            objectsToRemove.Clear();
            objects.Clear();
        }

        public void SetSpritebatch(SpriteBatch spriteBatcToUse)
        {
            spriteBatch = spriteBatcToUse;
        }
    }
}