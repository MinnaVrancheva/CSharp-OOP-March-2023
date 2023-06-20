using CollectionHierarchy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy.Models
{
    public class MyList : IMyList
    {
        private readonly List<string> items;

        public MyList()
        {
            items = new List<string>();
        }

        public int Add(string item)
        {
            items.Insert(0, item);
            return 0;
        }

        public string Remove()
        {
            string item = items[0];
            items.RemoveAt(0);
            return item;
        }
        public int Used => items.Count;
    }
}
