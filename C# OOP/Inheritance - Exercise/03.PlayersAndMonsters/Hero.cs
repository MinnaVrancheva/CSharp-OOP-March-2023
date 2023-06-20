using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayersAndMonsters
{
    public class Hero
    {

        public Hero(string userName, int level)
        {
            UserName = userName;
            Level = level;
        }

        public string UserName;
        public int Level;

        public override string ToString()
        {
            return $"Type: {this.GetType().Name} Username: {this.UserName} Level: {this.Level}";
        }
    }
}