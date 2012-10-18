using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bouncing.CollisionSystem
{
    /// <summary>
    /// Defines functions that all collisionsystems developed for this
    /// game must implement.
    /// </summary>
    public interface IManageCollisionsService
    {
        /// <summary>
        /// Register an object that does not move. These objects are never checked
        /// against other static objects for collision, reducing the number of checks per frame.
        /// </summary>
        /// <param name="goc">The gameobject to check for collision</param>
        void RegisterObject(GameObjectCollidable goc);

        void RemoveAllObjects();
    }
}
