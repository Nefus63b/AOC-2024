
using ConsoleApp1.FileReader;

namespace ConsoleApp1
{
    public class Day4 : DayTemplate
    {
        private RawTextFileReader _file;

        public Day4() : base(4)
        {
            _file = new RawTextFileReader($".\\Inputs\\Day{DayNumber}.txt");
        }

        public override void Run_Part1()
        {
            LoadFile();
            Res_Part1 = LookForStringInOneDirection("XMAS");

        }

        public override void Run_Part2()
        {
        }

        private void LoadFile()
        {
            _file.ReadData();
        }

        private long LookForStringInOneDirection(string textToFind)
        {
            
            
            if (_file.XSize == 0 || _file.YSize == 0)
            {
                return 0;
            }

            long count = 0;

            for (int y = 0;  y < _file.YSize; y++)
            {
                for (int x = 0;  x < _file.XSize; x++)
                {
                    for (int yStep = -1; yStep <= 1; yStep++)
                    {
                        for (int xStep = -1; xStep <= 1; xStep++)
                        {
                            if (LookFortAString(textToFind, x, y, xStep, yStep))
                            {
                                count++;
                            }
                        }
                    }
                }
            }

            return count;
        }

        private bool LookFortAString(string textToFind, int xStart, int yStart, int xStep, int yStep)
        {
            if (xStep == 0 && yStep == 0)
            {
                return false;
            }

            if (xStep < 0 && xStart < textToFind.Length)
            {
                return false;
            }
            if (yStep < 0 && yStart < textToFind.Length)
            {
                return false;
            }

            if (xStep > 0 || xStart > (_file.XSize - textToFind.Length))
            {
                return false;
            }
            if (yStep > 0 || yStart > (_file.YSize - textToFind.Length))
            {
                return false;
            }

            bool found = true;
            int x = xStart;
            int y = yStart;

            for (int i = 0;  i < textToFind.Length; i++)
            {
                var value = _file.Data[y][x];
                var attemptedValue = textToFind[i];

                if (value != attemptedValue)
                {
                    found = false;
                    break;
                }
                else
                {
                    found = true;
                }

                x += xStep;
                y += yStep;
            }

            return found;
        }
    }
}
