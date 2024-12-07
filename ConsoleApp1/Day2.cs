using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Day2 : DayTemplate
    {
        public Day2():base(2)
        {
        }

        public override void Run_Part1()
        {
            SantaFileReader day2 = new SantaFileReader(Path.Combine("Inputs", "Day2.txt"), ' ');
            for (int i = 0; i < day2.LinesCount; i++)
            {
                var line = day2.GetIntsLine(i);

                bool safe = CheckLevel(line);
                if (safe)
                {
                    Res_Part1++;
                }
            }
        }

        public override void Run_Part2()
        {
            SantaFileReader day2 = new SantaFileReader(Path.Combine("Inputs", "Day2.txt"), ' ');

            for (int i = 0; i < day2.LinesCount; i++)
            {
                var line = day2.GetIntsLine(i).ToList();

                bool safe = CheckLevel(line);
                int retries = 0;
                while (!safe && retries < line.Count())
                {
                    var line2 = line.ToList();
                    line2.RemoveAt(retries);
                    safe = CheckLevel(line2);
                    retries++;
                }

                if (safe)
                {
                    Res_Part2++;
                }
            }
        }

        private static bool CheckLevel(IEnumerable<int?> line)
        {
            int? previousValue = null;
            bool? direction = null;
            foreach (var item in line)
            {
                if (previousValue is null)
                {
                    previousValue = item;
                }
                else
                {
                    if (item is not null)
                    {
                        var diff = (item - previousValue) ?? 0;

                        if (Math.Abs(diff) > 3 || diff == 0)
                        {
                            return false;
                        }

                        if (direction is null)
                        {
                            direction = diff > 0;
                        }
                        else
                        {
                            if ((diff > 0) != direction)
                            {
                                return false;
                            }
                        }

                        previousValue = item;
                    }
                }
            }
            return true;
        }
    }
}
