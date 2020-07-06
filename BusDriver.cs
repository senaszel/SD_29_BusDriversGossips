using System;
using System.Collections.Generic;
using System.Linq;

namespace SD_29_BusDriversGossips
{
    class BusDriver
    {
        private List<int> route;

        public bool[] KnownGossips { get; set; }
        public List<int> Route { get; set; }


        public BusDriver(List<int> _route)
        {
            Route = new List<int>();
            do
            {
                Route.AddRange(_route);

            } while (Route.Count < 420);
            Route = Route.Take(420).ToList();
        }


        public override string ToString()
        {
            string route = String.Join(',', Route);
            string gossips = String.Join(',', KnownGossips);


            return $"This bus goes:\n{route}\nand knows:\n{gossips}";
        }


    }
}
