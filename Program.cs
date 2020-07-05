using System;
using System.Collections.Generic;
using System.Linq;

namespace SD_29_BusDriversGossips
{
    class Program
    {
        static void Main(string[] args)
        {
            int nbOfDrivers;
            do
            {
                Console.WriteLine("How many bus drivers hit the road this time?");
                int.TryParse(Console.ReadLine(), out int _temp);
                nbOfDrivers = _temp;
            } while (nbOfDrivers <= 0);

            List<int> allStops = new List<int>();
            List<List<int>> AllBusDrivers = new List<List<int>>();
            for (int i = 0; i < nbOfDrivers; i++)
            {
                AllBusDrivers.Add(GetRoute(ref allStops));
            }

            List<List<int>> beenWhere = new List<List<int>>(420);
            for (int i = 0; i < 420; i++)
            {
                beenWhere.Add(new List<int>());
            }
            AllBusDrivers.ForEach(driver => Simulate_8hours(driver, beenWhere));

            //
            // Dobra masz generowanie tras
            // i miejsca gdzie sa ktorzy o jakiej porze
            // no to teraz 
            // odpierdol wykreslanke kto co wie i 
            // skreslanie jak sie spotykaja
            //

            int[,] tableOfKnowledge = new int[AllBusDrivers.Count, AllBusDrivers.Count];
            for (int i = 0; i < tableOfKnowledge.GetLength(0); i++)
            {
                for (int j = 0; j < tableOfKnowledge.GetLength(1); j++)
                {
                    if (i == j)
                        tableOfKnowledge[i, j] = 1;
                }
            }

            for (int i = 0; i < tableOfKnowledge.GetLength(0); i++)
            {
                for (int j = 0; j < tableOfKnowledge.GetLength(1); j++)
                {
                    Console.Write("[" + tableOfKnowledge[i, j] + "]");
                }
                Console.WriteLine();
            }

            Console.ReadKey();

            Console.WriteLine(
            AllBusDrivers.Count()
                );
            for (int i = 0; i < AllBusDrivers.Count(); i++)
            {
                AllBusDrivers[i].ForEach(x => Console.Write(x));
                Console.WriteLine();
            }


            Console.WriteLine("\n\n\t================================theEnd");
        }

        private static List<int> GetRoute(ref List<int> allStops)
        {
            Console.WriteLine("\n\tPut stops numbers describing route of this particullar driver (one full circle)");
            List<int> stops;
            do
            {
                var _temp = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                stops = _temp.ToList().ConvertAll(stop => Convert.ToInt32(stop));

            } while (stops.Count <= 0);
            allStops = allStops.Union(stops).ToList();
            Console.WriteLine();

            return stops;
        }

        private static void Simulate_8hours(List<int> stops, List<List<int>> data)
        {
            Console.WriteLine("\tThis one goes (minute by minute):");

            List<List<int>>.Enumerator dataEnumerator = data.GetEnumerator();
            List<int>.Enumerator stopEnumer = stops.GetEnumerator();
            for (int i = 0; i < 420; i++)
            {
                if (stopEnumer.MoveNext())
                {
                    dataEnumerator.MoveNext();
                    int _temp = stopEnumer.Current;
                    dataEnumerator.Current.Add(_temp);
                    Console.Write(_temp);
                    Console.Write(" ");
                }
                else
                {
                    stopEnumer = stops.GetEnumerator();
                    i--;
                }
            }
            Console.WriteLine();
        }

    } // end of class
} // end of namespace
