using System;

namespace M04_Encapsulation_Inheritance_Polymorphism
{
    public class Circle : Shape
    {
        const double Pi = 3.14159265;
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public override double Perimeter() => 2 * Pi * Radius;

        public override double Area() => Pi * Math.Pow(Radius, 2);
    }
    
}
