using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class User
    {
        private int health = 100;

        public int Health
        {
             get { return health; }
            private set { health = value; }
        }
        public void hit(int damage)
        {
            if (damage > health)
            {
                damage = health;
            }
            health -= damage;

        }
    }
}
 