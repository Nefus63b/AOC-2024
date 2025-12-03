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
            SantaFileReader data = new SantaFileReader(Path.Combine("Inputs", $"Day{Day}.txt"), ',');

            var totalJolts = (long)0;
            foreach (var lineValues in data.GetAllLines())
            {
                var currentJolts = CalculateJolts(lineValues[0], 2);
                totalJolts += currentJolts;
            }

            Res_Part1 = totalJolts;

            // 17244
        }

        public override void Run_Part2()
        {
            SantaFileReader data = new SantaFileReader(Path.Combine("Inputs", $"Day{Day}.txt"), ',');

            var totalJolts = (long)0;
            foreach (var lineValues in data.GetAllLines())
            {
                var currentJolts = CalculateJolts(lineValues[0], 12);
                totalJolts += currentJolts;
            }

            Res_Part2 = totalJolts;

            // 171435596092638
        }

        private long CalculateJolts(string line, int maxBatteries)
        {
            var currentJoltsTuple = new Tuple<long, int>(0, 0);
            var currentJolts = (long)0;
            for (int i = 0; i < maxBatteries; i++)
            {
                var maxIndex = maxBatteries - i - 1;
                currentJoltsTuple = GetBiggestJolt(line, currentJoltsTuple.Item2, maxIndex);
                currentJolts += currentJoltsTuple.Item1 * (long)Math.Pow(10, maxBatteries - i - 1);
            }

            return currentJolts;
        }

        private Tuple<long,int> GetBiggestJolt(string line, int startIndex, int maxIndex)
        {
            var biggestJolt = 0;
            var biggestJoltIndex = 0;
            for (var i = startIndex; i < line.Length - maxIndex; i++)
            {
                var jolts = int.Parse(line[i].ToString());
                if (jolts > biggestJolt)
                {
                    biggestJolt = jolts;
                    biggestJoltIndex = i;
                }
            }
            return new Tuple<long, int>(biggestJolt, biggestJoltIndex + 1);
        }
    }
}
