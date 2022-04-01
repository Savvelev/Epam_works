
namespace ComputerGame
{
    public class Player : Character
    {
        public int Health { get; set; }
        public Player() 
        {
            Health = 100;          
        }     
        public void PickBonus()
        {           
        }
        public override void Move()
        {

        }
        public override string ToString()
        {
            return "\x263B";
        }     
    }
}
