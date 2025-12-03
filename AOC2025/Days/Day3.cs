using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Quic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;

namespace AOC2025.Days
{
    internal class Day3 : DayTemplate
    {
        public Day3() : base(3)
        {
           
        }

        public override void Run_Part1()
        {
            SantaFileReader day2 = new SantaFileReader(Path.Combine("Inputs", "Day3.txt"), ',');

            Res_Part1 = 0;
        }

        public override void Run_Part2()
        {
            SantaFileReader day2 = new SantaFileReader(Path.Combine("Inputs", "Day3.txt"), ',');

            Res_Part2 = 0;
        }
    }
}
