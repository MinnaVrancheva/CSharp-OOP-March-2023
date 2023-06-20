using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return OutputMessages.RaceCannotBeCompleted;
            }
            else if (!racerOne.IsAvailable() && racerTwo.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerTwo.Username, racerOne.Username);
            }
            else if (racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerOne.Username, racerTwo.Username);
            }
            else
            {                
                double racerOneMultiplir = racerOne.RacingBehavior == "strict" ? 1.2 : 1.1;
                double racerTwoMultiplir = racerTwo.RacingBehavior == "strict" ? 1.2 : 1.1;

                double racerOneChanceOfWinning = racerOne.Car.HorsePower * racerOne.DrivingExperience * racerOneMultiplir;
                double racerTwoChanceOfWinning = racerTwo.Car.HorsePower * racerTwo.DrivingExperience * racerTwoMultiplir;

                string winner = racerOneChanceOfWinning > racerTwoChanceOfWinning ? racerOne.Username : racerTwo.Username;

                racerOne.Race();
                racerTwo.Race();

                return string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, winner);
            }
        }
    }
}
