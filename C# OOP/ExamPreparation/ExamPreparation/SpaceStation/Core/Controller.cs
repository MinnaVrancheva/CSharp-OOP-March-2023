using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using SpaceStation.Utilities.Messages;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Mission;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private AstronautRepository astronauts;
        private PlanetRepository planets;
        private int exploredPlanets = 0;
        public Controller()
        {
            this.astronauts = new AstronautRepository();
            this.planets = new PlanetRepository();
        }
        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronaut;
            if(type == nameof(Biologist))
            {
                astronaut = new Biologist(astronautName);
            }
            else if (type == nameof(Geodesist))
            {
                astronaut = new Geodesist(astronautName);
            }
            else if (type == nameof(Meteorologist))
            {
                astronaut = new Meteorologist(astronautName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }

            astronauts.Add(astronaut);
            return string.Format(OutputMessages.AstronautAdded, type, astronautName);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);
            
            foreach(var item in items)
            {
                planet.Items.Add(item);
            }

            planets.Add(planet);
            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string ExplorePlanet(string planetName)
        {
            var suitableAstronauts = astronauts.Models.Where(x => x.Oxygen > 60).ToList();

            if(!suitableAstronauts.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }

            IMission mission = new Mission();
            IPlanet planet = planets.FindByName(planetName);

            mission.Explore(planet, suitableAstronauts);
            exploredPlanets ++;

            return string.Format(OutputMessages.PlanetExplored, planetName, suitableAstronauts.Count(x => x.Oxygen <= 0));
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{exploredPlanets} planets were explored!");
            sb.AppendLine("Astronauts info:");

            foreach (var astronaut in astronauts.Models)
            {
                string bags = astronaut.Bag.Items.Any() ? string.Join(", ", astronaut.Bag.Items) : "none";

                sb.AppendLine($"Name: {astronaut.Name}");
                sb.AppendLine($"Oxygen: {astronaut.Oxygen}");
                sb.AppendLine($"Bag Items: {bags}");
            }

            return sb.ToString().TrimEnd();
        }

        public string RetireAstronaut(string astronautName)
        {
            IAstronaut astronaut = astronauts.FindByName(astronautName);
            if(astronaut == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRetiredAstronaut, astronautName));
            }
            astronauts.Remove(astronaut);
            return string.Format(OutputMessages.AstronautRetired, astronautName);
        }
    }
}
