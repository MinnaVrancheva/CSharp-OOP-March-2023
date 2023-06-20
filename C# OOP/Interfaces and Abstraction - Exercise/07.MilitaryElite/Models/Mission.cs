using MilitaryElite.Enums;
using MilitaryElite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite.Models
{
    public class Mission : IMission
    {
        public Mission(string codeName, State states)
        {
            CodeName = codeName;
            States = states;
        }
        public string CodeName { get; private set; }
        public State States { get; private set; }

        public void CompleteMission()
        {
            States = State.Finished;
        }

        public override string ToString()
        {
            return $"Code Name: {CodeName} State: {States}";
        }
    }
}
