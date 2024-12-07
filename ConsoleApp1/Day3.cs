using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Day3 : DayTemplate
    {
        public Day3() : base(3)
        {
        }

        public override void Run_Part1()
        {
            using (StreamReader sr = new StreamReader(Path.Combine("Inputs", "Day3.txt")))
            {
                Regex regex = new Regex("((mul\\()\\d+([,])\\d+(\\)))|(do\\(\\))|(don't\\(\\))");
                string data = sr.ReadToEnd();

                var regexRes = regex.Matches(data);
                Res_Part1 = Day3_ReadProgram(regexRes, false);
            }
        }

        public override void Run_Part2()
        {
            long res = 0;
            using (StreamReader sr = new StreamReader(Path.Combine("Inputs", "Day3.txt")))
            {
                Regex regex = new Regex("((mul\\()\\d+([,])\\d+(\\)))|(do\\(\\))|(don't\\(\\))");
                string data = sr.ReadToEnd();

                var regexRes = regex.Matches(data);
                Res_Part2 = Day3_ReadProgram(regexRes, true);
            }
        }

        private static long Day3_ReadProgram(MatchCollection regexRes, bool checkEnabler = false)
        {
            long res = 0;
            bool enabled = true;
            foreach (var item in regexRes.ToList())
            {
                string text = item.Value;
                if (text == "do()")
                {
                    enabled = true;
                    continue;
                }
                if (text == "don't()")
                {
                    enabled = false;
                    continue;
                }

                if (enabled || !checkEnabler)
                {
                    text = text.Remove(0, 4);
                    var numbers = text.Remove(text.Length - 1, 1).Split(',');
                    int val1 = int.Parse(numbers[0]);
                    int val2 = int.Parse(numbers[1]);

                    res += val1 * val2;
                }
            }

            return res;
        }
    }
}
