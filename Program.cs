using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace SD_29_BusDriversGossips
{
    class Program
    {
        static void Main(string[] args)
        {
            int nbOfDrivers = AskHowManyDrivers();
            List<BusDriver> drivers = SetRoutesForEm(nbOfDrivers);
            Introduction();
            IntroduceDriversToUser(drivers);
            (int minutes, List<BusDriver> drivers) answer = SimulateShift(drivers);
            GiveAnswer(answer.minutes);
            IntroduceDriversToUser(answer.drivers);
        }

        private static void Introduction()
        {
            Console.Clear(); Console.WriteLine("\tAt the beginning all drivers know only one gossip: ");
            Console.WriteLine("And pick ups more while encountering other drivers at the same bus stops. They share gossips until...");
        }

        private static void IntroduceDriversToUser(List<BusDriver> drivers)
        {
            drivers.ForEach(x => { Console.WriteLine(); Console.WriteLine(x.ToString()); });
        }

        private static void GiveAnswer(int answer)
        {
            if (answer > -1)
                Console.WriteLine($"\n\n\tEvery driver already know every gossip just after passing {answer+1} stops.\n");
            else
                Console.WriteLine("\n\n\tNEVER");
        }


        private static (int, List<BusDriver>) SimulateShift(List<BusDriver> drivers)
        {
            List<int> stops = new List<int>();

            for (int i = 0; i < 420; i++)
            {
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!" + i + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                stops.Clear();

                drivers.ForEach(x =>
                {
                    stops.Add(x.Route[i]);
                });

                if (i == 0)
                {
                    drivers.ForEach(x =>
                    {
                        Console.WriteLine("count="+x.Route.Count);

                    });
                }

                for (int oneDriver = 0; oneDriver < stops.Count; oneDriver++)
                {
                    for (int anotherDriver = 0; anotherDriver < stops.Count; anotherDriver++)
                    {
                        if (oneDriver != anotherDriver)
                        {
                            if (stops[oneDriver] == stops[anotherDriver])
                            {
                                ShareGossip(drivers, oneDriver, anotherDriver);

                                //IntroduceDriversToUser(drivers);

                                bool allKnowEverything = true;
                                for (int o = 0; o < drivers.Count; o++)
                                {
                                    for (int p = 0; p < drivers[o].KnownGossips.Length; p++)
                                    {
                                        if (drivers[o].KnownGossips[p] == false)
                                        {
                                            allKnowEverything = false;
                                        }
                                    }

                                }

                                if (allKnowEverything)
                                {
                                    return (i, drivers);
                                }
                                else
                                    Console.WriteLine($"driver {oneDriver} meets driver {anotherDriver} on stops nb:{stops[oneDriver]}");
                            }

                        }
                    }
                }
            }
            return (-1, drivers);
        }

        private static void ShareGossip(List<BusDriver> drivers, int one, int another)
        {
            for (int m = 0; m < drivers[one].KnownGossips.Length; m++)
            {
                if (drivers[one].KnownGossips[m] == false && drivers[another].KnownGossips[m] == true)
                {
                    drivers[one].KnownGossips[m] = true;
                }

                if (drivers[another].KnownGossips[m] == false && drivers[one].KnownGossips[m] == true)
                {
                    drivers[another].KnownGossips[m] = true;
                }
            }
        }

        private static List<BusDriver> SetRoutesForEm(int nbOfDrivers)
        {
            List<BusDriver> drivers = new List<BusDriver>();
            for (int i = 0; i < nbOfDrivers; i++)
            {
                List<int> intted = new List<int>();
                string[] splitted;
                do
                {
                    Console.Clear();
                    Console.WriteLine(i + "Put stops numbers, divided by space, in a sequence the bus will be passing:");

                    intted.Clear();
                    string _userInput = Console.ReadLine();
                    splitted = _userInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < splitted.Length; j++)
                    {
                        if (int.TryParse(splitted[j], out int _temp))
                            intted.Add(_temp);
                    }
                } while (intted.Count != splitted.Length);

                BusDriver busDriver = new BusDriver(intted);
                drivers.Add(busDriver);
            }

            for (int i = 0; i < drivers.Count; i++)
            {
                drivers[i].KnownGossips = Enumerable.Repeat(false, drivers.Count).ToArray();
                drivers[i].KnownGossips[i] = true;
            }

            return drivers;
        }


        private static int AskHowManyDrivers()
        {
            int nbOfDrivers;
            do
            {
                Console.Clear();
                Console.WriteLine("How many drivers hit the road?");

                bool parsed = int.TryParse(Console.ReadLine(), out int _temp);
                nbOfDrivers = parsed == true ? _temp : 0;

            } while (nbOfDrivers <= 1);


            return nbOfDrivers;
        }


    } // end of class
} // end of namespace
