using System;

namespace M04_Encapsulation_Inheritance_Polymorphism
{
    public class Triangle : Shape
    {
        public double ASide { get; set; }
        public double BSide { get; set; }
        public double CSide { get; set; }

        public Triangle(double aSide, double bSide , double cSide)
        {
            ASide = aSide;
            BSide = bSide;
            CSide = cSide;

        }
        public override double Perimeter() => ASide + BSide + CSide;
        public override double Area() => Math.Sqrt(Perimeter() * (Perimeter() - ASide) * (Perimeter() - BSide) * (Perimeter() - CSide));
    }
    
}
