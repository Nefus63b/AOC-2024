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
    internal class Day2 : DayTemplate
    {
        public Day2() : base(2)
        {
           
        }

        public override void Run_Part1()
        {
            SantaFileReader day2 = new SantaFileReader(Path.Combine("Inputs", "Day2.txt"), ',');
            var data = day2.GetAllLines().SelectMany(line => line).ToArray();
            var invalidIds = new List<long>();

            foreach (var rangeStr in data)
            {
                var range = rangeStr.Split('-').Select(s => long.Parse(s)).ToArray();
                var start = range[0];
                var end = range[1];

                for (var i = start; i <= end; i++)
                {
                    var strId = i.ToString();
                    var digitsCount = strId.Length;
                    var part1 = strId.Substring(0, digitsCount / 2);
                    var part2 = strId.Substring(digitsCount / 2, digitsCount - (digitsCount / 2));
                    if (part1 == part2)
                    {
                        invalidIds.Add(i);
                    }
                }
            }

            Res_Part1 = invalidIds.Sum();
            // 32976912643
        }

        public override void Run_Part2()
        {
            SantaFileReader day2 = new SantaFileReader(Path.Combine("Inputs", "Day2.txt"), ',');
            var data = day2.GetAllLines().SelectMany(line => line).ToArray();
            var invalidIds = new List<long>();

            //foreach (var rangeStr in data)
            //{
            //    var range = rangeStr.Split('-').Select(s => long.Parse(s)).ToArray();
            //    var start = range[0];
            //    var end = range[1];

            //    for (var i = start; i <= end; i++)
            //    {
            //        var strId = i.ToString();
            //        for (var j = 1; j < strId.Length; j++)
            //        {
            //            var part1 = strId.Substring(0, j);
            //            var part2 = strId.Substring(j, strId.Length - j);
            //            Regex regex = new Regex($"^(?:{Regex.Escape(part1)})+$");
            //            if (regex.IsMatch(part2))
            //            {
            //                if (!invalidIds.Contains(i)) invalidIds.Add(i);
            //                break;
            //            }
            //        }
            //    }
            //}

            foreach (var rangeStr in data)
            {
                var range = rangeStr.Split('-').Select(long.Parse).ToArray();
                var start = range[0];
                var end = range[1];

                for (var i = start; i <= end; i++)
                {
                    var strId = i.ToString();
                    var length = strId.Length;

                    for (var j = 1; j < length; j++)
                    {
                        var part1 = strId[..j];
                        var part2 = strId[j..];

                        if (part2.Length % part1.Length == 0 && part2 == string.Concat(Enumerable.Repeat(part1, part2.Length / part1.Length)))
                        {
                            if (!invalidIds.Contains(i)) invalidIds.Add(i);
                            break; // No need to check further for this number
                        }
                    }
                }
            }

            Res_Part2 = invalidIds.Sum();
            // 54446379122
        }
    }
}
