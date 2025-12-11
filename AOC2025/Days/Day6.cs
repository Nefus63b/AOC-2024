using Common;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

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
            // 5784380717354
        }

        public override void Run_Part2()
        {
            SantaFileReader data = new SantaFileReader(Path.Combine("Inputs", $"Day{Day}.txt"), '!');

            var lines = data.GetAllLines().ToList();
            var lastLine = lines.Last();
            var allSigns = string.Join(" ", lastLine);

            var currentOperation = ' ';
            var digitsCount = 0;
            var operations = new List<(char Operation, int digitsCount, List<string> Values)>();

            for (int charIndex = 0; charIndex < allSigns.Length; charIndex++)
            {
                if (allSigns[charIndex] != ' ')
                {
                    if (digitsCount > 0)
                    {
                        operations.Add((currentOperation, digitsCount - 1, new List<string>()));
                    }
                    currentOperation = allSigns[charIndex];
                    digitsCount = 1;
                    continue;
                }
                digitsCount++;
            }
            operations.Add((currentOperation, digitsCount, new List<string>()));

            foreach (var line in lines)
            {
                if (line == lastLine)
                {
                    continue;
                }
                var currentPos = 0;
                foreach (var operation in operations)
                {
                    var value = line[0].Substring(currentPos, operation.digitsCount);
                    operation.Values.Add(value);
                    currentPos += operation.digitsCount + 1;
                }
            }

            long total = 0;
            foreach (var operation in operations)
            {
                var subTotal = (long)0;
                switch (operation.Operation)
                {
                    case '+':
                        for (int digitIndex = 0; digitIndex < operation.digitsCount; digitIndex++)
                        {
                            var value = ParseVerticalValue(operation, digitIndex);
                            subTotal += value;
                        }
                            total += subTotal;
                        break;
                    case '*':
                        subTotal = 1;
                        for (int digitIndex = 0; digitIndex < operation.digitsCount; digitIndex++)
                        {
                            var value = ParseVerticalValue(operation, digitIndex);
                            subTotal *= value;
                        }
                        total += subTotal;
                        break;
                    default:
                        throw new InvalidOperationException($"Operation {operation.Operation} is not supported.");
                }
            }

            Res_Part2 = total;

            // 7996218225744
        }

        private long ParseVerticalValue((char Operation, int digitsCount, List<string> Values) operation, int digitIndex)
        {
            var value = string.Empty;
            for (var lineIndex = 0; lineIndex < operation.Values.Count; lineIndex++)
            {
                var digit = operation.Values[lineIndex][digitIndex];
                if (digit != ' ')
                {
                    var digitValue = int.Parse(operation.Values[lineIndex][digitIndex].ToString());
                    value += digitValue.ToString();
                }
            }

            return long.Parse(value);
        }
    }
                    
}
