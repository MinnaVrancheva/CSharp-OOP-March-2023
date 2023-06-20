using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsInfo
{
    public class Team
    {
        private string name;
        private List<Person> firstTeam;
        private List<Person> reserveTeam;

        public Team(string name)
        {
            Name = name;
            firstTeam = new List<Person>();
            reserveTeam = new List<Person>();
        }

        public string Name { get { return name; } private set { name = value; } }
        public IReadOnlyCollection<Person> FirstTeam
        { 
            get { return this.firstTeam.AsReadOnly(); }
        }
        public IReadOnlyCollection<Person> ReserveTeam 
        { 
            get { return this.reserveTeam.AsReadOnly(); }
        }

        public void AddPlayer(Person player)
        {
            if (player.Age < 40)
            {
                firstTeam.Add(player);
            }
            else
            {
                reserveTeam.Add(player);
            }
        }

        public override string ToString() => $"First team has {firstTeam.Count} players.{Environment.NewLine}Reserve team has {reserveTeam.Count} players.";
    }
}
