using Common;
using System.Reflection.Metadata;

namespace AOC2025.Days
{
    internal class Day6 : DayTemplate
    {
        public Day6() : base(6)
        {
           
        }

        public override void Run_Part1()
        {
            SantaFileReader data = new SantaFileReader(Path.Combine("Inputs", $"Day{Day}.txt"), ' ');
            var operations = new (char Operation, List<long> Values)[data.ColumnsCount];
            var lines = data.GetAllLines();
            var lastLine = lines.Last();

            for (int i = 0; i < data.ColumnsCount; i++)
            {
                operations[i] = (Operation: lastLine[i].Trim()[0], Values: new List<long>());
            }

            foreach (var line in lines)
            {
                if (line == lastLine)
                {
                    continue;
                }
                for (int i = 0; i < line.Length; i++)
                {
                    var item = line[i];
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        var currentOp = operations[i];
                        if (long.TryParse(item.Trim(), out long value))
                        {
                            currentOp.Values.Add(value);
                        }
                    }
                }
                
            }
            long total = 0;
            foreach (var operation in operations)
            {
                switch (operation.Operation)
                {
                    case '+':
                        total += operation.Values.Sum();
                        break;
                    case '*':
                        long prod = 1;
                        foreach (var val in operation.Values)
                        {
                            prod *= val;
                        }
                        total += prod;
                        break;
                    default:
                        throw new InvalidOperationException($"Operation {operation.Operation} is not supported.");
                }
            }
            Res_Part1 = total;
        }

        public override void Run_Part2()
        {
            SantaFileReader data = new SantaFileReader(Path.Combine("Inputs", $"Day{Day}.txt"), ' ');

            Res_Part2 = 0;
        }
    }
                    
}
