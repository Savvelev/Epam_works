using System;
using System.Text;

namespace ComputerGame
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;         

            Field field = new Field(10, 10);
            var createdField = Field.CreateField();
            Field.DisplayField(field, createdField);
            // Field is ready

            InitAllObjects.NessasaryObject(createdField);
            // Ready to start play


            Console.ReadLine();
            Console.Clear();
            Field.DisplayField(field, createdField);
        }
    }    
}