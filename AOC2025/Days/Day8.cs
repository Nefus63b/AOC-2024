using Common;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace AOC2025.Days
{
    internal class Day8 : DayTemplate
    {
        internal class Point3D
        {
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
            var connectionGroups = MakeFirstConnection(orderedDistances, 1000);

            var total = connectionGroups
                .Select(group => group.SelectMany(connection => new[] { connection.From, connection.To }).Distinct().Count())
                .OrderByDescending(count => count)
                .Take(3)
                .Aggregate(1L, (acc, count) => acc * count);

            Res_Part1 = total;
        }

        public override void Run_Part2()
        {
            var data = new SantaFileReader(Path.Combine("Inputs", $"Day{Day}.txt"), ',');
            var points = GetPointsFromLines(data.GetAllLines());
            var orderedDistances = GetOrderedDistances(points);
            var connectionGroups = MakeFirstConnection(orderedDistances, orderedDistances.Count);

            var lastJoin = connectionGroups.First().Last();
            Res_Part2 = (long)lastJoin.From.X * (long)lastJoin.To.X;
        }

        private static List<Point3D> GetPointsFromLines(IEnumerable<string[]> lines)
        {
            return lines
                .Where(line => line.Length >= 3 &&
                               int.TryParse(line[0], out _) &&
                               int.TryParse(line[1], out _) &&
                               int.TryParse(line[2], out _))
                .Select(line => new Point3D(int.Parse(line[0]), int.Parse(line[1]), int.Parse(line[2])))
                .ToList();
        }

        private static List<(Point3D From, Point3D To, double distance)> GetOrderedDistances(List<Point3D> points)
        {
            var distanceList = new List<(Point3D From, Point3D To, double distance)>();
            for (var i = 0; i < points.Count; i++)
            {
                for (var j = i + 1; j < points.Count; j++)
                {
                    distanceList.Add((points[i], points[j], points[i].DistanceFrom(points[j])));
                }
            }
            return distanceList.OrderBy(t => t.distance).ToList();
        }

        private static List<List<(Point3D From, Point3D To)>> MakeFirstConnection(List<(Point3D From, Point3D To, double distance)> orderedDistances, long maximum)
        {
            var connections = new List<List<(Point3D From, Point3D To)>>();
            var distanceSubset = orderedDistances.Take((int)maximum).ToList();

            foreach (var item in distanceSubset)
            {
                var targetGroup = connections.FirstOrDefault(group =>
                    group.Any(c => c.From == item.From || c.To == item.From || c.From == item.To || c.To == item.To));

                if (targetGroup != null)
                {
                    if (!targetGroup.Any(c => (c.From == item.From && c.To == item.To) || (c.From == item.To && c.To == item.From)))
                    {
                        targetGroup.Add((item.From, item.To));
                    }
                }
                else
                {
                    connections.Add(new List<(Point3D From, Point3D To)> { (item.From, item.To) });
                }
            }

            OptimizeConnections(connections);
            return connections;
        }

        private static void OptimizeConnections(List<List<(Point3D From, Point3D To)>> groupsOfConnections)
        {
            for (int i = 0; i < groupsOfConnections.Count; i++)
            {
                var target = groupsOfConnections[i];
                for (int j = i + 1; j < groupsOfConnections.Count; j++)
                {
                    var source = groupsOfConnections[j];
                    if (source.Any(item => target.Any(c => c.From == item.From || c.To == item.From || c.From == item.To || c.To == item.To)))
                    {
                        target.AddRange(source);
                        groupsOfConnections.RemoveAt(j);
                        j--;
                    }
                }
            }
        }
    }
                    
}
