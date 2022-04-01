using System.Collections.Generic;
using ComputerGame.Bonuses;
using ComputerGame.Monsters;
using ComputerGame.Obstacles;

namespace ComputerGame
{
    public class InitAllObjects
    {

        public static List<GameObject> gameObjects = new List<GameObject>();
        private static readonly int countOfAddingObjects = SettingProportionalObjectsCount.CountOfAddingObjects;

        public static void NessasaryObject(string[,] createdField)           
        {           
            AddObject(new Player(), createdField);
            AddObject(new Bear(), createdField); 
            AddObject(new Wolve(), createdField);          

            for (int i = 0; i < countOfAddingObjects; i++)
            {
                AddObject(new Apple(), createdField); 
                AddObject(new Banana(), createdField);
                AddObject(new Cherry(), createdField); 
                AddObject(new Stone(), createdField); 
                AddObject(new Tree(), createdField); 
            }
        }
        public static void AddObject(GameObject gameObject, string[,] createdField)
        {
            gameObjects.Add(gameObject);
            createdField[gameObject.X, gameObject.Y] = gameObject.ToString();
        }
    }
}