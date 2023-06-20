using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models.Vessels;

public class Submarine : Vessel
{
    private const double SubmarineArmorThickness = 200;
    public Submarine(string name, double mainWeaponCaliber, double speed) 
        : base(name, SubmarineArmorThickness, mainWeaponCaliber, speed)
    {
        SubmergeMode = false;
    }

    public bool SubmergeMode { get; private set; }

    public void ToggleSubmergeMode()
    {
        if (SubmergeMode == false)
        {
            MainWeaponCaliber += 40;
            Speed -= 4;
        }
        else if (SubmergeMode == true)
        {
            MainWeaponCaliber -= 40;
            Speed += 4;
        }
        SubmergeMode = !SubmergeMode;
    }
    public override void RepairVessel()
    {
        ArmorThickness = SubmarineArmorThickness;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        string submergeMode = SubmergeMode == true ? "ON" : "OFF";

        sb.AppendLine(base.ToString());
        sb.AppendLine($" *Submerge mode: {submergeMode}");

        return sb.ToString().TrimEnd();
    }
}
