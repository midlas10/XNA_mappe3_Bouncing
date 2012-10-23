using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace Bouncing
{
    /// <summary>
    /// An instance of this gamecomponent can be used to manage collisions between 2D objects in a gameworld. 
    /// The collisionDetectionService implements IManageCollisionsService interface to make it possible to 
    /// create other collisionsystems and still have the game function as intended, because the interface function
    /// is still the same, meaning that all objects making use of the interface will notice no change in the system.
    /// </summary>
    public class CollisionDetectionCircleService : IManageCollisionsService
    {

        protected List<GameObjectCollidable> collidables;

        public CollisionDetectionCircleService(Game game)
        {
            collidables = new List<GameObjectCollidable>();
        }


        /// <summary>
        /// Performs maintenance of the collision system as well as a collision check on all objects
        /// currently registered.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {

            //Maintenance pass
            for (int i = 0; i < collidables.Count; i++)
            {
                if (collidables[i].DeleteFlag)
                {
                    collidables.Remove(collidables[i]);
                    i--;
                    continue;
                }
            }
            //Match all objects against each other and check for collisions
            for (int i = 0; i < collidables.Count; i++)
            {
                for (int j = 0; j < collidables.Count; j++)
                {   //Don't match an object with itself
                    if (i == j)
                    {
                        continue;
                    }
                    if (BoundingCircle(collidables[i].CollisionBox.X, collidables[i].CollisionBox.Y, collidables[i].CollisionBox.Width / 2, collidables[j].CollisionBox.X, collidables[j].CollisionBox.Y, collidables[j].CollisionBox.Width / 2))
                    {
                        collidables[i].Collision(collidables[j]);
                    }
                }
            }
        }

        /// <summary>
        /// Register an object for collision detection.
        /// </summary>
        /// <param name="goc">The object to register..</param>
        public void RegisterObject(GameObjectCollidable goc)
        {
            collidables.Add(goc);
        }

        public void RemoveAllObjects()
        {
            collidables.Clear();
        }

        public bool BoundingCircle(int x1, int y1, int radius1, int x2, int y2, int radius2)
        {
            Vector2 V1 = new Vector2(x1, y1); // reference point 1
            Vector2 V2 = new Vector2(x2, y2); // reference point 2
            Vector2 Distance = V1 - V2; // get the distance between the two reference points
            if (Distance.Length() < radius1 + radius2) // if the distance is less than the diameter
                return true;

            return false;
        }
    }
}