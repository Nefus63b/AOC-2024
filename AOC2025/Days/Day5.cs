using Common;
using System.Data.SqlTypes;

namespace AOC2025.Days
{
    internal class Day5 : DayTemplate
    {
        public Day5() : base(5)
        {
           
        }

        public override void Run_Part1()
        {
            SantaFileReader data = new SantaFileReader(Path.Combine("Inputs", $"Day{Day}.txt"), ',');

            var lines = data.GetAllLines();
            var isHeader = true;
            var header = new List<(long Start, long End)>();
            var lots = new List<long>();
            foreach (var line in lines)
            {
                if (line.Length == 0)
                {
                    isHeader = false;
                    continue;
                }

                if (isHeader)
                {
                    var fields = line[0].Split('-', StringSplitOptions.RemoveEmptyEntries);
                    header.Add((long.Parse(fields[0]), long.Parse(fields[1])));
                }
                else
                {
                    lots.Add(long.Parse(line[0]));
                }
            }

            var count = 0;
            foreach (var lot in lots)
            {
                foreach (var (Start, End) in header)
                {
                    if (lot >= Start && lot <= End)
                    {
                        count++;
                        break;
                    }
                }
            }

                Res_Part1 = count;
        }

        public override void Run_Part2()
        {
            SantaFileReader data = new SantaFileReader(Path.Combine("Inputs", $"Day{Day}.txt"), ',');

            var lines = data.GetAllLines();
            var header = new List<(long Start, long End)>();
            foreach (var line in lines)
            {
                if (line.Length == 0)
                {
                    break;
                }
                var fields = line[0].Split('-', StringSplitOptions.RemoveEmptyEntries);
                header.Add((long.Parse(fields[0]), long.Parse(fields[1])));
            }

            var oldSourceCount = 0;
            var source = header.ToList();
            var merged = new List<(long Start, long End)>();
            while (oldSourceCount != source.Count)
            {
                merged = MergeList(source);
                oldSourceCount = source.Count;
                source = merged.ToList();
            }

            source = source.OrderBy(s => s.Start).ThenBy(s => s.End).ToList();

            long total = 0;
            foreach (var range in source)
            {
                var currentRange = range.End - range.Start + 1;
                //Console.Write($"Range: {range.Start}-{range.End} => {currentRange}\n");
                total += currentRange;
            }
            
            Res_Part2 = total;

            // 355648227273762 => To hight
            // 352509891817883 => To hight
            // 352509891817881 => OK

        }

        private List<(long Start, long End)> MergeList(List<(long Start, long End)> source)
        {
            var merged = new List<(long Start, long End)>();
            foreach (var (Start, End) in source)
            {
                bool isMerged = false;
                for (int i = 0; i < merged.Count; i++)
                {
                    var (rStart, rEnd) = merged[i];
                    if (!isMerged &&
                           (Start >= rStart && Start <= rEnd+1)
                        || (End >= rStart-1 && End <= rEnd)
                        || (Start <= rStart && End >= rEnd)
                       )
                    {
                        // Overlap
                        merged[i] = (Math.Min(rStart, Start), Math.Max(rEnd, End));
                        isMerged = true;
                        break;
                    }
                }
                if (!isMerged)
                {
                    merged.Add((Start, End));
                }
            }

            return merged;
        }
    }
                    
}
