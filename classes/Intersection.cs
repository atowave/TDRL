using System;
using System.Numerics;

namespace KWEngine.Hitbox
{
    /// <summary>
    /// Helper class that holds information about the intersection.
    /// </summary>
    public class Intersection
    {
        /// <summary>
        /// The object the calling Hitbox did collide with.
        /// </summary>
        public Object Object { get; private set; } = null;
        private Vector2 mMTV = new Vector2(0, 0);
        /// <summary>
        /// The MTV (minimum-translation-vector) that will undo the collision.
        /// Example:
        /// If the MTV is X=2 and Y=1, you would need to move the calling object
        /// 2 units along the x axis and one unit along the y axis in order to 
        /// "not collide anymore".
        /// </summary>
        public Vector2 MTV
        {
            get
            {
                return mMTV;
            }
        }

        /// <summary>
        /// Generates an Intersection instance.
        /// </summary>
        /// <param name="collider">Colliding object</param>
        /// <param name="mtv">Minimum-translation-vector</param>
        public Intersection(Object collider, ref Vector2 mtv)
        {
            Object = collider;
            mMTV.X = mtv.X;
            mMTV.Y = mtv.Y;
        }
    }
}
