using System;
using System.Text;
using ComputerGame;
namespace ExecutionProject
{

    class Program
    {
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;         

            Field field = new Field(6, 7);
            var createdField = Field.FieldCreator();
            Field.FieldDisplay(field, createdField);
            // Field is ready

            InitAllObjects.NessasaryObject(Field.Width, Field.Height, createdField);
            // Ready to start play


            Console.ReadLine();
            Console.Clear();
            Field.FieldDisplay(field, createdField);
        }
    }    
}