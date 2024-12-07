
namespace ConsoleApp1
{
    public abstract class DayTemplate
    {
        private short _day;
        public DayTemplate(short dayNumber)
        {
            _day = dayNumber;
        }

        public long Res_Part1 { get; set; }
        public long Res_Part2 { get; set; }

        public void Run()
        {
            Console.WriteLine("===========DAY {0}===========",_day);

            Run_Part1();
            Console.WriteLine("Day PART-1 result : {0}", Res_Part1, _day);

            Run_Part2();
            Console.WriteLine("Day PART-2 result : {0}", Res_Part2, _day);
        }

        public abstract void Run_Part1();

        public abstract void Run_Part2();
    }
}
