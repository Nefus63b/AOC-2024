using Common;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace AOC2025.Days
{
    internal class Day8 : DayTemplate
    {
        internal class Point3D{
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public Point3D(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }
            public double DistanceFrom(Point3D other)
            {
                return Math.Sqrt(Math.Pow(other.X - X, 2) + Math.Pow(other.Y - Y, 2) + Math.Pow(other.Z - Z, 2));
            }

            public override string ToString()
            {
                return $"({X},{Y},{Z})";
            }
        }

        public Day8() : base(8)
        {
           
        }

        public override void Run_Part1()
        {
            var data = new SantaFileReader(Path.Combine("Inputs", $"Day{Day}.txt"), ',');
            var points = GetPointsFromLines(data.GetAllLines());
            var orderedDistances = GetOrderedDistances(points);
            var ConnectionGroups = MakeFirstConnection(orderedDistances, 1000);
            
            var counts = new List<long>();
            foreach (var group in ConnectionGroups)
            {
                var distinctPoints = (long)group.SelectMany(connection => new[] { connection.From, connection.To })
                                            .Distinct()
                                            .Count();
                counts.Add(distinctPoints);

            }
            var total = (long)1;
            counts.OrderByDescending(g => g).Take(3).ToList().ForEach(c => total *= c);
            
            Res_Part1 = total;

            // 244188
        }

        public override void Run_Part2()
        {
            var data = new SantaFileReader(Path.Combine("Inputs", $"Day{Day}.txt"), ',');

            var points = GetPointsFromLines(data.GetAllLines());
            var orderedDistances = GetOrderedDistances(points);
            var ConnectionGroups = MakeFirstConnection(orderedDistances, orderedDistances.Count);

            var lastJoin = ConnectionGroups.First().Last();

            Res_Part2 = (long)lastJoin.From.X * (long)lastJoin.To.X;

            // 8361881885
        }

        private static List<Point3D> GetPointsFromLines(IEnumerable<string[]> lines)
        {
            var points = new List<Point3D>();
            foreach (var line in lines)
            {
                if (line.Length >= 3 &&
                    int.TryParse(line[0], out int x) &&
                    int.TryParse(line[1], out int y) &&
                    int.TryParse(line[2], out int z))
                {
                    points.Add(new Point3D(x, y, z));
                }
            }
            return points;
        }

        private static List<(Point3D From, Point3D To, double distance)> GetOrderedDistances(List<Point3D> points)
        {
            var distanceList = new List<(Point3D From, Point3D To, double distance)>();

            for (var sourceIndex = 0; sourceIndex < points.Count; sourceIndex++)
            {
                for (var destIndex = sourceIndex + 1; destIndex < points.Count; destIndex++)
                {
                    var distance = points[sourceIndex].DistanceFrom(points[destIndex]);
                    distanceList.Add((points[sourceIndex], points[destIndex], distance));
                }
            }
            return distanceList.OrderBy(t => t.distance).ToList();
        }

        private static List<List<(Point3D From, Point3D To)>> MakeFirstConnection(List<(Point3D From, Point3D To, double distance)> orderedDistances,long maximum)
        {
            var connections = new List<List<(Point3D From, Point3D To)>>();
            for (var count = 0; count < maximum; count++)
            {
                var item = orderedDistances[count];
                var existingFound = false;
                foreach (var connection in connections)
                {
                    var containsFrom = connection.Any(c => c.From == item.From || c.To == item.From);
                    var containsTo = connection.Any(c => c.From == item.To || c.To == item.To);
                    if (containsFrom || containsTo)
                    {
                        existingFound = true;
                        if (!(containsFrom && containsTo))
                        {
                            connection.Add((item.From, item.To));
                        }
                        break;
                    }
                }
                if (!existingFound)
                {
                    connections.Add(new List<(Point3D From, Point3D To)>() { (item.From, item.To) });
                }

                while (OptimizeConnections(connections) > 0)
                {
                    // keep optimizing
                }
            }

            return connections;
        }

        private static long OptimizeConnections(List<List<(Point3D From, Point3D To)>> groupsOfConnections)
        {
            var movesMade = (long)0;

            for (int i = 0; i < groupsOfConnections.Count; i++)
            {
                var target = groupsOfConnections[i];
                for (int j = i + 1; j < groupsOfConnections.Count; j++)
                {
                    var source = groupsOfConnections[j];
                    for (int k = 0; k < source.Count; k++)
                    {
                        var item = source[k];
                        var containsFrom = target.Any(c => c.From == item.From || c.To == item.From);
                        var containsTo = target.Any(c => c.From == item.To || c.To == item.To);
                        if (containsFrom ^ containsTo)
                        {
                            target.Add(item);
                            source.RemoveAt(k);
                            movesMade++;
                            k--;
                        }
                    }
                    if (source.Count == 0)
                    {
                        groupsOfConnections.RemoveAt(j);
                        j--;
                    }
                }
            }
            return movesMade;
        }
    }
                    
}
