using System;

namespace M04_Encapsulation_Inheritance_Polymorphism
{
    public class Square : Shape
    {
        public int Side { get; set; }       

        public Square(int side)
        {
            Side = side;          
        }

        public override double Area() => Math.Pow(Side, 2);

        public override double Perimeter() => 4 * Side;
    }
    
}
