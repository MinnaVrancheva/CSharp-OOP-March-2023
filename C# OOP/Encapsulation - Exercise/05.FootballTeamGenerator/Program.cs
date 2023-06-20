
using FootballTeamGenerator.Models;

namespace FootballTeamGenerator
{
    public class StartUp
    {
        static void Main(string[] arg)
        {
            //List<Team> teamNames = new List<Team>();
            Team team = new Team();
            string command;

            while ((command = Console.ReadLine()) != "END")
            {
                try
                {
                    switch (command.Split(";", StringSplitOptions.RemoveEmptyEntries)[0])
                    {
                        case "Team":
                            team.Name = command.Split(";", StringSplitOptions.RemoveEmptyEntries)[1];
                            break;
                        case "Add":
                            Player player = new(command.Split(";")[2], int.Parse(command.Split(";")[3]), int.Parse(command.Split(";")[4]), int.Parse(command.Split(";")[5]), int.Parse(command.Split(";")[6]), int.Parse(command.Split(";")[7]));
                            team.AddPlayer(command.Split(";")[1], player, team);
                            break;
                        case "Remove":
                            string playerName = command.Split(";")[2];
                            string teamName = command.Split(";")[1];
                            team.RemovePlayer(playerName, player, teamName);
                            break;
                        case "Rating":
                            teamName = command.Split(";")[1];
                            team.PrintRating(teamName, team);
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

