using System;

namespace ComputerGame
{
    public abstract class GameObject: IEquatable<GameObject>
    {
        public int X { get; set; }
        public int Y { get; set; }
       

        protected GameObject()
        {            
            RandomCoordinate.RandomInitCooridnate(this);           
        }
       
        public bool Equals(GameObject other)
        {
            if(other == null)
            return false;

            return X.Equals(other.X) && Y.Equals(other.Y);
        }
    }  
}
