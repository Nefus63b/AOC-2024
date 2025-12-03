
namespace Common
{
    public abstract class DayTemplate
    {
        public short Day { get; init; }
        public DayTemplate(short dayNumber)
        {
            Day = dayNumber;
        }

        public long Res_Part1 { get; set; }
        public long Res_Part2 { get; set; }

        public void Run()
        {
            Console.WriteLine("===========DAY {0}===========", Day);

            Run_Part1();
            Console.WriteLine("Day {1} PART-1 result : {0}", Res_Part1, Day);

            Run_Part2();
            Console.WriteLine("Day {1} PART-2 result : {0}", Res_Part2, Day);
        }

        public abstract void Run_Part1();

        public abstract void Run_Part2();
    }
}
