using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite.Interfaces
{
    public interface ISoldier
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }   
        
    }
}
