using NavalVessels.Core.Contracts;
using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Models.Vessels;
using NavalVessels.Repositories;
using NavalVessels.Repositories.Contracts;
using NavalVessels.Utilities.Messages;

namespace NavalVessels.Core;

public class Controller : IController
{
    private readonly IRepository <IVessel> vessels;
    private readonly List<ICaptain> captains;

    public Controller()
    {
        this.vessels = new VesselRepository();
        this.captains = new List<ICaptain>();
    }
    public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
    {
        
        if (captains.FirstOrDefault(x => x.FullName == selectedCaptainName) == default)
        {
            return string.Format(OutputMessages.CaptainNotFound, selectedCaptainName);
        }

        IVessel vessel = vessels.FindByName(selectedVesselName);
        if(vessel == null)
        {
            return string.Format(OutputMessages.VesselNotFound, selectedVesselName);
        }

        if (vessel.Captain != null)
        {
            return string.Format(OutputMessages.VesselOccupied, selectedVesselName);
        }

        ICaptain captain = captains.First(x => x.FullName == selectedCaptainName); //FirstOrDefault(x => x.FullName == selectedCaptainName);

        captain.AddVessel(vessel);
        vessel.Captain = captain;
        return string.Format(OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName);
    }

    public string AttackVessels(string attackingVesselName, string defendingVesselName)
    {
        IVessel attackingVessel = vessels.FindByName(attackingVesselName);
        IVessel defendingVessel = vessels.FindByName(defendingVesselName);

        if (attackingVessel == null || defendingVessel == null)
        {
            string outputName = attackingVessel == null ? attackingVesselName : defendingVesselName;
            return string.Format(OutputMessages.VesselNotFound, outputName);
        }

        if (attackingVessel.ArmorThickness == 0 || defendingVessel.ArmorThickness == 0)
        {
            string outputThickness = attackingVessel.ArmorThickness == 0 ? attackingVesselName : defendingVesselName;
            return string.Format(OutputMessages.AttackVesselArmorThicknessZero, outputThickness);
        }

        attackingVessel.Attack(defendingVessel);
        attackingVessel.Captain.IncreaseCombatExperience();
        defendingVessel.Captain.IncreaseCombatExperience();

        return string.Format(OutputMessages.SuccessfullyAttackVessel, defendingVesselName, attackingVesselName, defendingVessel.ArmorThickness);
    }

    public string CaptainReport(string captainFullName)
    {
        ICaptain captain = captains.First(x => x.FullName == captainFullName);
        return captain.Report();
    }

    public string HireCaptain(string fullName)
    {
        ICaptain captain = new Captain(fullName);

        if (captains.FirstOrDefault(x => x.FullName == fullName) != null)
        {
            return string.Format(OutputMessages.CaptainIsAlreadyHired, fullName);
        }

        captains.Add(captain);

        return string.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
    }

    public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
    {
        IVessel vessel = vessels.FindByName(name);

        if (vessel != null)
        {
            return string.Format(OutputMessages.VesselIsAlreadyManufactured, vesselType, name);
        }

        if (vesselType == nameof(Battleship))
        {
            vessel = new Battleship(name, mainWeaponCaliber, speed);
        }
        else if (vesselType == nameof(Submarine))
        {
            vessel = new Submarine(name, mainWeaponCaliber, speed);
        }
        else
        {
            return OutputMessages.InvalidVesselType; 
        }

        vessels.Add(vessel);
        return string.Format(OutputMessages.SuccessfullyCreateVessel, vesselType, name, mainWeaponCaliber, speed);
    }

    public string ServiceVessel(string vesselName)
    {
        IVessel vessel = vessels.FindByName(vesselName);
        if(vessel == null)
        {
            return String.Format(OutputMessages.VesselNotFound, vesselName);
        }
        else
        {
            return String.Format(OutputMessages.SuccessfullyRepairVessel, vesselName);
        }
    }

    public string ToggleSpecialMode(string vesselName)
    {
        IVessel vessel = vessels.FindByName(vesselName);
        if (vessel == null)
        {
            return String.Format(OutputMessages.VesselNotFound, vesselName);
        }

        if (vessel.GetType().Name == nameof(Battleship))
        {
            Battleship battleship = (Battleship)vessel;
            battleship.ToggleSonarMode();

            return String.Format(OutputMessages.ToggleBattleshipSonarMode, vesselName);
        }
        else
        {
            Submarine submarine = (Submarine)vessel;
            submarine.ToggleSubmergeMode();

            return String.Format(OutputMessages.ToggleSubmarineSubmergeMode, vesselName);
        }
    }

    public string VesselReport(string vesselName)
    {
        IVessel vessel = vessels.FindByName(vesselName);
        return vessel.ToString().TrimEnd();
    }
}
