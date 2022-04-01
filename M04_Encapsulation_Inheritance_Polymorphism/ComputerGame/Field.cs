using System;

namespace ComputerGame
{
    public class Field
    {
        public static int Height { get; private set; }
        public static int Width { get; private set; }

        public Field(int height, int width)
        {
            SettingProportionalObjectsCount.SetPropCount(height, width);  
            
            Height = height;
            Width = width;                                   
        }

        public static string[,] CreateField()
        {
            var field = new string[Width,Height];

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    field[i, j] = ".";
                }
            }
            return field;
        }
        public static void DisplayField(Field field, string[,] createdField)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Console.Write(createdField[i, j] + "  ");
                }
                Console.WriteLine();
            }
        }
    }
}
