using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models.Vessels;

public abstract class Vessel : IVessel
{
    private string name;
    private ICaptain captain;
    //private double armorThickness;
    //private double mainWeaponCaliber;
    //private double speed;
    private List<string> targets;

    public Vessel(string name, double mainWeaponCaliber, double speed, double armorThickness)
    {
        Name = name;
        MainWeaponCaliber = mainWeaponCaliber;
        Speed = speed;
        ArmorThickness = armorThickness;
        targets = new List<string>();
    }

    public string Name
    {
        get { return name; }
        private set
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(ExceptionMessages.InvalidVesselName);
            }
            name = value;
        }
    }

    public ICaptain Captain
    {
        get { return captain; }
        set
        {
            if(value == null)
            {
                throw new NullReferenceException(ExceptionMessages.InvalidCaptainToVessel);
            }
            captain = value;
        }
    }
    public double ArmorThickness { get; set; }

    public double MainWeaponCaliber { get; protected set; }

    public double Speed { get; protected set; }

    public ICollection<string> Targets => this.targets.AsReadOnly();

    public void Attack(IVessel target)
    {
        if (target == null)
        {
            throw new NullReferenceException(ExceptionMessages.InvalidTarget);
        }

        target.ArmorThickness -= MainWeaponCaliber;

        if(target.ArmorThickness < 0)
        {
            target.ArmorThickness = 0;
        }

        targets.Add(target.Name);
    }

    public abstract void RepairVessel();

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        string availableTargets = Targets.Any() ? string.Join(" ", targets) : "None";

        sb.AppendLine($"- {Name}");
        sb.AppendLine($" *Type: {GetType().Name}");
        sb.AppendLine($" *Armor thickness: {ArmorThickness}");
        sb.AppendLine($" *Main weapon caliber: {MainWeaponCaliber}");
        sb.AppendLine($" *Speed: {Speed} knots");
        sb.AppendLine($" *Targets: {availableTargets}");

        return sb.ToString().TrimEnd();
    }
}
