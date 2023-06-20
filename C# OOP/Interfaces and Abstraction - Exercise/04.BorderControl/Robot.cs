using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl
{
    public class Robot : IIdentifier
    {
        public Robot(string id, string model)
        {
            Id = id;
            Model = model;
        }

        public string Model { get; private set; }
        public string Id {get; private set;}
    }
}
