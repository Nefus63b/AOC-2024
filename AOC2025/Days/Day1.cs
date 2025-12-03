using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Quic;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace AOC2025.Days
{
    internal class Day1 : DayTemplate
    {
        public Day1() : base(1)
        {
           
        }

        public override void Run_Part1()
        {
            SantaFileReader day1 = new SantaFileReader(Path.Combine("Inputs", "Day1.txt"), ' ');

            var data = day1.GetAllLines().Select(l => new { Value = int.Parse(l[0].Substring(1, l[0].Length - 1)) * (l[0][0] == 'L' ? -1 : 1) }).ToList();

            var current = 50;
            var code = 0;
            foreach (var item in data)
            {
                var start = current;
                current += item.Value;
                while(current < 0)
                {
                    current += 100;
                }
                while(current > 99)
                {
                    current -= 100;
                }

                if (current == 0)
                {
                    code++;
                }
                //Console.WriteLine($"{start} {item.Value}\t = {current} \tTotal:{code}");
            }
            Res_Part1 = code;
            // 31
        }

        public override void Run_Part2()
        {
            SantaFileReader day1 = new SantaFileReader(Path.Combine("Inputs", "Day1.txt"), ' ');

            var data = day1.GetAllLines().Select(l => new { Value = int.Parse(l[0].Substring(1, l[0].Length - 1)) * (l[0][0] == 'L' ? -1 : 1) }).ToList();

            var current = 50;
            var code = 0;
            foreach (var item in data)
            {
                var start = current;
                var amount = Math.Abs(item.Value);
                var direction = item.Value >= 0 ? 1 : -1;

                var ticks = 0;
                while(amount >= 100)
                {
                    amount -= 100;
                    ticks++;
                }
                var ticks2 = amount / 100;
                var amount2 = amount % 100;

                for (var i = 0; i < amount; i++)
                {
                    current += direction;
                    if (current < 0)
                    {
                        current += 100;
                    }
                    if (current > 99)
                    {
                        current -= 100;
                    }

                    if (current == 0) ticks++;
                }

                code += ticks;
                //Console.WriteLine($"{start} {item.Value}\t = {current} \t({ticks} turns) \tTotal:{code}");
            }

            Res_Part2 = code;
            //! 5542
            //! 5326
            //! 6188
            //! 7321
            //  6295
        }
    }
}
