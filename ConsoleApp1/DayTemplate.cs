
namespace ConsoleApp1
{
    public abstract class DayTemplate
    {
        public short DayNumber { get; private set; }
        public DayTemplate(short dayNumber)
        {
            DayNumber = dayNumber;
        }

        public long Res_Part1 { get; set; }
        public long Res_Part2 { get; set; }

        public void Run()
        {
            Console.WriteLine("===========DAY {0}===========",DayNumber);

            Run_Part1();
            Console.WriteLine("Day PART-1 result : {0}", Res_Part1, DayNumber);

            Run_Part2();
            Console.WriteLine("Day PART-2 result : {0}", Res_Part2, DayNumber);
        }

        public abstract void Run_Part1();

        public abstract void Run_Part2();
    }
}
