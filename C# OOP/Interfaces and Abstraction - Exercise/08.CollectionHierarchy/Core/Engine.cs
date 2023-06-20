using CollectionHierarchy.Core.Interfaces;
using CollectionHierarchy.Models;
using CollectionHierarchy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionHierarchy.Core
{
    public class Engine : IEngine
    {
        //Dictionary<string, List<int>> addedItems = new()
        //{
        //    {"AddCollection", new List<int>()},
        //    {"AddRemoveCollection", new List<int>()},
        //    {"MyList", new List<int>()},
        //};

        //Dictionary<string, List<int>> removedItems = new()
        //{
        //    {"AddCollection", new List<int>()},
        //    {"AddRemoveCollection", new List<int>()},
        //    {"MyList", new List<int>()},
        //};

        public void Run()
        {
            string[] input = Console.ReadLine()
                .Split();

            IAddCollection addCollection = new AddCollection();
            IAddRemoveCollection addRemoveCollection = new AddRemoveCollection();
            IMyList myList = new MyList();

            foreach (string inputItem in input)
            {
                addCollection.Add(inputItem);
                addRemoveCollection.Add(inputItem);
                myList.Add(inputItem);
            }

            
            Console.WriteLine(addCollection.ToString());
            Console.WriteLine(addRemoveCollection.ToString());
            Console.WriteLine(myList.ToString());
        }
    }
}
