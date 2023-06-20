using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models.Vessels;

public class Battleship : Vessel
{
    private const double BattleshipArmorThickness = 300;
    public Battleship(string name, double mainWeaponCaliber, double speed) 
        : base(name, BattleshipArmorThickness, mainWeaponCaliber, speed)
    {
        SonarMode = false;
    }

    public bool SonarMode { get; private set; }
    public void ToggleSonarMode()
    {
        if (SonarMode == false)
        {
            MainWeaponCaliber += 40;
            Speed -= 5;
        }
        else if (SonarMode == true)
        {
            MainWeaponCaliber -= 40;
            Speed += 5;
        }
        SonarMode = !SonarMode;
    }

    public override void RepairVessel()
    {
        ArmorThickness = BattleshipArmorThickness;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        string sonarMode = SonarMode == true ? "ON" : "OFF";

        sb.AppendLine(base.ToString());
        sb.AppendLine($" *Sonar mode: {sonarMode}");

        return sb.ToString().TrimEnd();
    }
}
