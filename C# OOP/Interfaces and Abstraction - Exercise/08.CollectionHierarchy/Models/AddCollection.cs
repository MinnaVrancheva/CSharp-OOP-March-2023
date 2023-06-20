using CollectionHierarchy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy.Models
{
    public class AddCollection : IAddCollection
    {
        private readonly List<string> items;

        public AddCollection()
        {
            items = new List<string>();
        }
        public int Add(string item)
        {
            items.Add(item);
            return items.Count - 1;
        }
    }
}
