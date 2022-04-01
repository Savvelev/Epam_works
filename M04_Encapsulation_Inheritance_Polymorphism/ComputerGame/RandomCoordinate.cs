using System;
using System.Linq;

namespace ComputerGame
{
    public class RandomCoordinate
    {
        static Random random = new Random();

        public static void RandomInitCooridnate(GameObject gameObject)
        {
            do
            {
                gameObject.X = random.Next(Field.Width);
                gameObject.Y = random.Next(Field.Height);

            } while (InitAllObjects.gameObjects.Any(n => gameObject.Equals(n)));
        }
    }
}
