using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeamGenerator.Models
{
    public class Team
    {
        private string name;
        private List<Player> players;

        public Team()
        {
            Name = name;
            players = new List<Player>();
        }

        public string Name { get { return name; } set { name = value; } }

        public int Count { get { return players.Count; } }

        public double Rating
        {
            get
            {
                if (players.Any())
                {
                    return players.Average(p => p.SkillLevel);
                }

                return 0;
            }
        }

        public void AddPlayer(string teamName, Player player, Team team)
        {
            if (team.Name == teamName)
            {
                players.Add(player);
            }
            else
            {
                throw new ArgumentException($"Team {teamName} does not exist.");
            }
        }

        public void RemovePlayer(string playerName, Player player, string teamName)
        {
            if (player.Equals(playerName))
            {
                players.RemoveAll(p => p.Name == playerName);
            }
            else
            {
                throw new ArgumentException($"Player {playerName} is not in {teamName} team.");
            }
        }

        public void PrintRating(string teamName, Team team)
        {
            if (team.Name == teamName)
            {
                Console.WriteLine($"{teamName} - {team.Rating}");
            }
        }
    }
}
