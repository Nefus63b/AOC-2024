using Common;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace AOC2025.Days
{
    internal class Day7 : DayTemplate
    {
        private char[][] _matrix;
        public Day7() : base(7)
        {
           
        }

        public override void Run_Part1()
        {
            SantaFileReader data = new SantaFileReader(Path.Combine("Inputs", $"Day{Day}.txt"), ' ');
            var lines = data.GetAllLines().ToList();
            var matrix = lines.Select(line => line[0].ToCharArray()).ToArray();

            var splitCount = (long)0;
            for (int y = 0; y < matrix.Length; y++)
            {
                for (int x = 0; x < matrix[y].Length; x++)
                {
                    if (matrix[y][x] == '^' && matrix[y - 1][x] == '|')
                    {
                        if (x > 0)
                            matrix[y][x - 1] = '|';
                        if (x < matrix[y].Length-1)
                            matrix[y][x + 1] = '|';
                        splitCount++;
                    }
                    if (matrix[y][x] == '.')
                    {
                        if (y > 0 && (matrix[y - 1][x] == 'S' || matrix[y - 1][x] == '|'))
                            matrix[y][x] = '|';
                    }
                }
            }

            Res_Part1 = splitCount;

            // 1490
        }

        public override void Run_Part2()
        {
            SantaFileReader data = new SantaFileReader(Path.Combine("Inputs", $"Day{Day}.txt"), ' ');
            var lines = data.GetAllLines().ToList();
            _matrix = lines.Select(line => line[0].ToCharArray()).ToArray();

            var startX = FindBeamEvent(0, 0, 'S');
            var res = PathFind3(startX, 1);

            Res_Part2 = res;
        }

        public int PathFind(int x, int y)
        {
            if (y >= _matrix.Length)
            {
                return 1;
            }
            if (x < 0 || x >= _matrix[y].Length)
            {
                return 0;
            }
            if (_matrix[y][x] == '.')
            {
                return PathFind(x, y + 1);
            }
            if (_matrix[y][x] == '^')
            {
                var left = PathFind(x - 1, y + 1);
                var right = PathFind(x + 1, y + 1);
                return left + right;
            }

            throw new Exception("Invalid path");
        }

        public int PathFind2(int x, int y)
        {
            int result = 0;
            Stack<(int x, int y)> stack = new Stack<(int x, int y)>();
            stack.Push((x, y));
            long iterations = 0;
            while (stack.Count > 0)
            {
                iterations++;
                var (currentX, currentY) = stack.Pop();

                if (currentY >= _matrix.Length)
                {
                    result++;
                    continue;
                }

                if (currentX < 0 || currentX >= _matrix[currentY].Length)
                {
                    continue;
                }

                if (_matrix[currentY][currentX] == '.')
                {
                    stack.Push((currentX, currentY + 1));
                }
                else if (_matrix[currentY][currentX] == '^')
                {
                    stack.Push((currentX - 1, currentY + 1));
                    stack.Push((currentX + 1, currentY + 1));
                }
            }

            return result;
        }

        public long PathFind3(int x, int y)
        {
            long[] currentRow = new long[_matrix[0].Length];
            long[] nextRow = new long[_matrix[0].Length];
            currentRow[x] = 1;

            for (int row = y; row < _matrix.Length; row++)
            {
                Array.Clear(nextRow, 0, nextRow.Length);

                for (int col = 0; col < _matrix[row].Length; col++)
                {
                    if (currentRow[col] == 0) continue;

                    if (_matrix[row][col] == '.')
                    {
                        nextRow[col] += currentRow[col];
                    }
                    else if (_matrix[row][col] == '^')
                    {
                        if (col > 0) nextRow[col - 1] += currentRow[col];
                        if (col < _matrix[row].Length - 1) nextRow[col + 1] += currentRow[col];
                    }
                }

                // Swap rows for the next iteration
                var temp = currentRow;
                currentRow = nextRow;
                nextRow = temp;
            }

            // Sum all paths in the last row
            return currentRow.Sum();
        }


        public int FindBeamEvent(int startX, int y, char targetChar)
        {
            var line = _matrix[y];
            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] == targetChar)
                {
                    return x;
                }
            }
            return -1;
        }

    }
                    
}
