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

            var lines = data.GetAllLines();
            var matrix = lines.Select(line => line[0].ToCharArray()).ToArray();

            var pass = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] != '@')
                    {
                        continue;
                    }
                    var count = CountSpecificCharAround(i, j, 1, '.', matrix);
                    if (count < 4)
                    {
                        pass++;
                    }
                }
            }

            Res_Part1 = pass;
        }

        public override void Run_Part2()
        {
            SantaFileReader data = new SantaFileReader(Path.Combine("Inputs", $"Day{Day}.txt"), ',');

            var lines = data.GetAllLines();
            var matrix = lines.Select(line => line[0].ToCharArray()).ToArray();

            var total = 0;
            var pass = 0;
            do
            {
                pass = 0;
                for (int i = 0; i < matrix.Length; i++)
                {
                    for (int j = 0; j < matrix[i].Length; j++)
                    {
                        if (matrix[i][j] != '@')
                        {
                            continue;
                        }
                        var count = CountSpecificCharAround(i, j, 1, '.', matrix);
                        if (count < 4)
                        {
                            matrix[i][j] = 'x';
                            pass++;
                        }
                    }
                }

                for (int i = 0; i < matrix.Length; i++)
                {
                    for (int j = 0; j < matrix[i].Length; j++)
                    {
                        if (matrix[i][j] == 'x')
                        {
                            matrix[i][j] = '.';
                        }
                    }
                }
                
                total += pass;
            } while (pass > 0);

            Res_Part2 = total;
        }

        public static int CountSpecificCharAround(int xPos, int yPos, int maxDistance, char emptyChar, char[][] matrix)
        {
            var count = 0;
            for (int x = xPos - maxDistance; x <= xPos + maxDistance; x++)
            {

                for (int y = yPos - maxDistance; y <= yPos + maxDistance; y++)
                {
                    if (x == xPos && y == yPos)
                    {
                        continue;
                    }

                    if (x >= 0 && x < matrix.Length && y >= 0 && y < matrix[x].Length)
                    {
                        if (matrix[x][y] != emptyChar)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        public static char[][] ConvertStringArrayToCharMatrix(string[] lines)
        {
            return lines.Select(line => line.ToCharArray()).ToArray();
        }
    }
                    
}
