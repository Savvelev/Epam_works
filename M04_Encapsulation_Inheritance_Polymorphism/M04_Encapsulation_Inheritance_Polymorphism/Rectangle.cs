namespace M04_Encapsulation_Inheritance_Polymorphism
{
    public class Rectangle : Shape
    {
        public int ASide { get; set; }
        public int BSide { get; set; }
        public Rectangle(int aSide, int bSide)
        {
            ASide = aSide;
            BSide = bSide;
        }

        public override double Area() => ASide * BSide;

        public override double Perimeter() => 2 * (ASide + BSide);
    }
    
}
