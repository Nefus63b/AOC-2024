using Common;

namespace AOC2025.Days
{
    internal class Day4 : DayTemplate
    {
        public Day4() : base(4)
        {
           
        }

        public override void Run_Part1()
        {
            SantaFileReader data = new SantaFileReader(Path.Combine("Inputs", $"Day{Day}.txt"), ',');

            Res_Part1 = 0;
        }

        public override void Run_Part2()
        {
            SantaFileReader data = new SantaFileReader(Path.Combine("Inputs", $"Day{Day}.txt"), ',');

            Res_Part2 = 0;
        }
    }
}
