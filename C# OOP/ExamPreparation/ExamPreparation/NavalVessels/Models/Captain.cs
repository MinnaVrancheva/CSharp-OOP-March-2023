using NavalVessels.Models.Contracts;
using NavalVessels.Models.Vessels;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models;

public class Captain : ICaptain
{
    private string fullName;
    private int combatExperience;
    private List<IVessel> vessels;

    public Captain(string fullName)
    {
        FullName = fullName;
        combatExperience = 0;
        vessels = new List<IVessel>();
    }

    public string FullName
    {
        get { return fullName; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(ExceptionMessages.InvalidCaptainName);
            }
            fullName = value;
        }
    }

    public int CombatExperience => combatExperience;

    public ICollection<IVessel> Vessels => this.vessels.AsReadOnly();

    public void AddVessel(IVessel vessel)
    {
        if (vessel == null)
        {
            throw new NullReferenceException(ExceptionMessages.InvalidVesselForCaptain);
        }

        vessels.Add(vessel);
    }

    public void IncreaseCombatExperience()
    {
        combatExperience += 10;
    }

    public string Report()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"{FullName} has {CombatExperience} combat experience and commands {Vessels.Count} vessels.");

        foreach (var vessel in Vessels)
        {
            sb.AppendLine(vessel.ToString());
        }

        return sb.ToString().TrimEnd();
    }
}
