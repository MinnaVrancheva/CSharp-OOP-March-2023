using System;
using System.Collections.Generic;
using MilitaryElite.Models;
using MilitaryElite.Interfaces;
using MilitaryElite.Enums;
using MilitaryElite.Core.Interfaces;

namespace MilitaryElite.Core;

public class Engine : IEngine
{
    private Dictionary<int, ISoldier> soldiers;

    public Engine()
    {
        soldiers = new Dictionary<int, ISoldier>();
    }
    public void Run()
    {
        string input;

        while ((input = Console.ReadLine()) != "End")
        {
            try
            {
                string[] input2 = input.Split(' ');

                Console.WriteLine(ProcessInput(input2));
            }
            catch (Exception) { }
        }
    }

    private string ProcessInput(string[] input2)
    {
        string type = input2[0];
        int id = int.Parse(input2[1]);
        string firstName = input2[2];
        string lastName = input2[3];

        ISoldier soldier = null;

        soldier = GetSoldierTypes(input2, type, id, firstName, lastName, soldier);

        soldiers.Add(id, soldier);

        return soldier.ToString();
    }
    private ISoldier GetSoldierTypes(string[] input2, string type, int id, string firstName, string lastName, ISoldier soldier)
    {
        if (type == "Private")
        {
            soldier = GetPrivate(input2, id, firstName, lastName);
        }
        else if (type == "LieutenantGeneral")
        {
            soldier = GetLieutenantGeneral(input2, id, firstName, lastName);

        }
        else if (type == "Engineer")
        {
            soldier = GetEngineer(input2, id, firstName, lastName);
        }
        else if (type == "Commando")
        {
            soldier = GetCommando(input2, id, firstName, lastName, soldier);
        }
        else if (type == "Spy")
        {
            soldier = GetSpy(input2, id, firstName, lastName);
        }

        return soldier;
    }

    private static ISoldier GetSpy(string[] input2, int id, string firstName, string lastName)
    {
        ISoldier soldier;
        int codeNumber = int.Parse(input2[4]);

        soldier = new Spy(id, firstName, lastName, codeNumber);
        return soldier;
    }

    private static ISoldier GetCommando(string[] input2, int id, string firstName, string lastName, ISoldier soldier)
    {
        bool IsValidCorps = Enum.TryParse(input2[5], out Corps corps);

        if (!IsValidCorps)
        {
            throw new Exception();
        }

        List<Mission> missions = new List<Mission>();

        for (int i = 6; i < input2.Length; i += 2)
        {
            string missionName = input2[i];
            string missionState = input2[i + 1];

            bool IsValidMission = Enum.TryParse<State>(missionState, out State state);

            if (!IsValidMission)
            {
                continue;
            }

            IMission mission = new Mission(missionName, state);
            missions.Add((Mission)mission);
        }

        soldier = new Commando(id, firstName, lastName, decimal.Parse(input2[4]), corps, missions);
        return soldier;
    }

    private static ISoldier GetEngineer(string[] input2, int id, string firstName, string lastName)
    {
        ISoldier soldier;
        List<IRepair> repairs = new();

        bool IsValidCorps = Enum.TryParse(input2[5], out Corps corps);

        if (!IsValidCorps)
        {
            throw new Exception();
        }

        for (int i = 6; i < input2.Length; i += 2)
        {
            string partName = input2[i];
            int hoursWorked = int.Parse(input2[i + 1]);
            IRepair repair = new Repair(partName, hoursWorked);
            repairs.Add(repair);
        }

        soldier = new Engineer(id, firstName, lastName, decimal.Parse(input2[4]), corps, repairs);
        return soldier;
    }

    private ISoldier GetLieutenantGeneral(string[] input2, int id, string firstName, string lastName)
    {
        ISoldier soldier;
        List<IPrivate> privates = new();
        
        for (int i = 5; i < input2.Length; i++)
        {
            int soldierId = int.Parse(input2[i]);
            soldier = soldiers[soldierId];
            privates.Add((IPrivate)soldier);
        }

        soldier = new LieutenantGeneral(id, firstName, lastName, decimal.Parse(input2[4]), privates);
        return soldier;
    }

    private static ISoldier GetPrivate(string[] input2, int id, string firstName, string lastName)
    {
        return new Private(id, firstName, lastName, decimal.Parse(input2[4]));
    }
}

