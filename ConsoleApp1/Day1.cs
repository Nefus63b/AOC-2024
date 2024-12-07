using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Day1 : DayTemplate
    {
        public Day1() : base(1)
        {
        }

        public override void Run_Part1()
        {
            SantaFileReader day1 = new SantaFileReader(Path.Combine("Inputs", "Day1.txt"), ' ');
            var list1 = day1.GetInt32NullColumn(0);
            var list2 = day1.GetInt32NullColumn(1);

            bool isFinished = false;
            while (!isFinished)
            {
                var smallest1 = list1.Where(i => i.BoolValue == false).MinBy(i => i.Value.HasValue ? i.Value : 0);
                var smallest2 = list2.Where(i => i.BoolValue == false).MinBy(i => i.Value.HasValue ? i.Value : 0);

                if (smallest1 is null || smallest2 is null)
                {
                    isFinished = true;
                }
                else
                {
                    var val1 = smallest1.Value.HasValue ? smallest1.Value.Value : 0;
                    var val2 = smallest2.Value.HasValue ? smallest2.Value.Value : 0;
                    Res_Part1 += Math.Abs(val1 - val2);
                    smallest1.BoolValue = true;
                    smallest2.BoolValue = true;
                }
            }
        }

        public override void Run_Part2()
        {
            SantaFileReader day1 = new SantaFileReader(Path.Combine("Inputs", "Day1.txt"), ' ');
            var list1 = day1.GetInt32NullColumn(0);
            var list2 = day1.GetInt32NullColumn(1);

            foreach (var item in list1)
            {
                var val = item.Value.HasValue ? item.Value.Value : 0;
                var count = list2.Where(i => (i.Value.HasValue ? i.Value.Value : 0) == val).Count();

                Res_Part2 += val * count;
            }
        }
    }
}
