using System;

namespace ComputerGame
{
    internal class SettingProportionalObjectsCount
    {
        internal static int CountOfAddingObjects { get; private set; } 
       
        internal static void SetPropCount(int height, int width)
        {
            int numOfValues = height * width;

            if (numOfValues < 40 )
            {
                CountOfAddingObjects = 1;
            }
            else if (numOfValues >= 40 && numOfValues <70)
            {
                CountOfAddingObjects = 2;
            }
            else if (numOfValues >= 70 && numOfValues<=100)
            {
                CountOfAddingObjects = 3;
            }
            else 
            {
                throw new ArgumentException();
            }            
        }
    }
}