

namespace ComputerGame.Monsters
{
    public abstract class Monster: Character
    {
        public int Damage { get; set; }
        protected Monster()
        {
            Damage = 50;
        }
       
        public void Hit(ref int playerHealth, int damage)
        {
            if (playerHealth>0)
            {
                playerHealth -= damage;
            }
           
        }

        public abstract override void Move();
    }
}
