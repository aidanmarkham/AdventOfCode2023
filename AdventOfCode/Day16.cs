using Microsoft.VisualBasic;

public static class Day16
{
    public static void DoPartOne()
    {
        var field = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day16.txt");

        var energized = (string[])field.Clone();

        var lasers = new Stack<(int x, int y, int dir)>();

        lasers.Push((-1, 0, 1));
        var allLasers = new List<(int x, int y, int dir)>();
        while (lasers.Count > 0)
        {
            var laser = lasers.Pop();

            if (allLasers.Contains(laser))
            {
                continue;
            }
            else
            {
                allLasers.Add(laser);
            }

            if (laser.y >= 0 && laser.y < field.Length && laser.x >= 0 && laser.x <= field[laser.y].Length)
            {
                energized[laser.y] = energized[laser.y].ReplaceAt(laser.x, '#');
            }


            (int x, int y) pos = (laser.x, laser.y);
            var dest = pos;

            switch (laser.dir)
            {
                case 0:
                    dest.y--;
                    break;
                case 1:
                    dest.x++;
                    break;
                case 2:
                    dest.y++;
                    break;
                case 3:
                    dest.x--;
                    break;
            }

            // we've hit a wall, this laser is done
            if (dest.y < 0 || dest.y >= field.Length || dest.x < 0 || dest.x >= field[dest.y].Length)
            {
                continue;
            }

            var destChar = field[dest.y][dest.x];

            if (destChar == '.')
            {
                lasers.Push((dest.x, dest.y, laser.dir));
            }
            else if (destChar == '|')
            {
                // directions: n = 0, e = 1, s = 2, w = 3
                switch (laser.dir)
                {
                    case 0:
                        lasers.Push((dest.x, dest.y, laser.dir));
                        break;
                    case 1:
                        lasers.Push((dest.x, dest.y, 0));
                        lasers.Push((dest.x, dest.y, 2));
                        break;
                    case 2:
                        lasers.Push((dest.x, dest.y, laser.dir));
                        break;
                    case 3:
                        lasers.Push((dest.x, dest.y, 0));
                        lasers.Push((dest.x, dest.y, 2));
                        break;
                }
            }
            else if (destChar == '-')
            {
                // directions: n = 0, e = 1, s = 2, w = 3
                switch (laser.dir)
                {
                    case 0:
                        lasers.Push((dest.x, dest.y, 1));
                        lasers.Push((dest.x, dest.y, 3));
                        break;
                    case 1:
                        lasers.Push((dest.x, dest.y, laser.dir));
                        break;
                    case 2:
                        lasers.Push((dest.x, dest.y, 1));
                        lasers.Push((dest.x, dest.y, 3));
                        break;
                    case 3:
                        lasers.Push((dest.x, dest.y, laser.dir));
                        break;
                }
            }
            else if (destChar == '/')
            {
                // directions: n = 0, e = 1, s = 2, w = 3
                switch (laser.dir)
                {
                    case 0:
                        lasers.Push((dest.x, dest.y, 1));
                        break;
                    case 1:
                        lasers.Push((dest.x, dest.y, 0));
                        break;
                    case 2:
                        lasers.Push((dest.x, dest.y, 3));
                        break;
                    case 3:
                        lasers.Push((dest.x, dest.y, 2));
                        break;
                }
            }
            else if (destChar == '\\')
            {
                // directions: n = 0, e = 1, s = 2, w = 3
                switch (laser.dir)
                {
                    case 0:
                        lasers.Push((dest.x, dest.y, 3));
                        break;
                    case 1:
                        lasers.Push((dest.x, dest.y, 2));
                        break;
                    case 2:
                        lasers.Push((dest.x, dest.y, 1));
                        break;
                    case 3:
                        lasers.Push((dest.x, dest.y, 0));
                        break;
                }
            }

            /*
            Console.WriteLine("\nLasers: " + lasers.Count);
            for (int y = 0; y < field.Length; y++)
            {
                for (int x = 0; x < field[y].Length; x++)
                {
                    var laserFound = false;
                    foreach (var l in lasers)
                    {
                        if (l.x == x && l.y == y)
                        {
                            laserFound = true;
                            switch (l.dir)
                            {
                                case 0:
                                    Console.Write("^");
                                    break;
                                case 1:
                                    Console.Write(">");
                                    break;
                                case 2:
                                    Console.Write("v");
                                    break;
                                case 3:
                                    Console.Write("<");
                                    break;
                            }

                            break;
                        }
                    }

                    if (!laserFound)
                    {
                        Console.Write(field[y][x]);
                    }
                }
                Console.Write("\n");
            }
            */
        }

        var total = 0;
        for (int i = 0; i < energized.Length; i++)
        {
            for (int j = 0; j < energized[i].Length; j++)
            {
                if (energized[i][j] == '#') total++;
            }
        }

        Console.Write("Total Energized Tiles: " + total);
    }

    public static void DoPartTwo()
    {
        var field = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day16.txt");

        var totals = new List<int>();
        
        // left and right sides
        for (int y = 0; y < field.Length; y++)
        {
            var left = RunLaserSim((-1, y, 1), field);
            totals.Add(left);
            
            var right = RunLaserSim((field[y].Length, y, 3), field);
            totals.Add(right);
            
            Console.WriteLine("Left: " + left + " Right: " + right);
        }
        // top and bottom sides
        for (int x = 0; x < field[0].Length; x++)
        {
            var top = RunLaserSim((x, -1, 2), field);
            totals.Add(top);
            
            var bottom = RunLaserSim((x, field.Length, 0), field);
            totals.Add(bottom);
            
            Console.WriteLine("Top: " + top + " Bottom: " + bottom);
        }

        int largest = 0;

        for (int i = 0; i < totals.Count; i++)
        {
            if (totals[i] > largest)
            {
                largest = totals[i];
            }
        }
        
        Console.WriteLine("Most energized: " + largest);
    }

    public static int RunLaserSim((int x, int y, int dir) l, string[] field)
    {
        var energized = (string[])field.Clone();
        var lasers = new Stack<(int x, int y, int dir)>();
        lasers.Push(l);
        var allLasers = new List<(int x, int y, int dir)>();
        while (lasers.Count > 0)
        {
            var laser = lasers.Pop();

            if (allLasers.Contains(laser))
            {
                continue;
            }
            else
            {
                allLasers.Add(laser);
            }

            if (laser.y >= 0 && laser.y < field.Length && laser.x >= 0 && laser.x < field[laser.y].Length)
            {
                energized[laser.y] = energized[laser.y].ReplaceAt(laser.x, '#');
            }


            (int x, int y) pos = (laser.x, laser.y);
            var dest = pos;

            switch (laser.dir)
            {
                case 0:
                    dest.y--;
                    break;
                case 1:
                    dest.x++;
                    break;
                case 2:
                    dest.y++;
                    break;
                case 3:
                    dest.x--;
                    break;
            }

            // we've hit a wall, this laser is done
            if (dest.y < 0 || dest.y >= field.Length || dest.x < 0 || dest.x >= field[dest.y].Length)
            {
                continue;
            }

            var destChar = field[dest.y][dest.x];

            if (destChar == '.')
            {
                lasers.Push((dest.x, dest.y, laser.dir));
            }
            else if (destChar == '|')
            {
                // directions: n = 0, e = 1, s = 2, w = 3
                switch (laser.dir)
                {
                    case 0:
                        lasers.Push((dest.x, dest.y, laser.dir));
                        break;
                    case 1:
                        lasers.Push((dest.x, dest.y, 0));
                        lasers.Push((dest.x, dest.y, 2));
                        break;
                    case 2:
                        lasers.Push((dest.x, dest.y, laser.dir));
                        break;
                    case 3:
                        lasers.Push((dest.x, dest.y, 0));
                        lasers.Push((dest.x, dest.y, 2));
                        break;
                }
            }
            else if (destChar == '-')
            {
                // directions: n = 0, e = 1, s = 2, w = 3
                switch (laser.dir)
                {
                    case 0:
                        lasers.Push((dest.x, dest.y, 1));
                        lasers.Push((dest.x, dest.y, 3));
                        break;
                    case 1:
                        lasers.Push((dest.x, dest.y, laser.dir));
                        break;
                    case 2:
                        lasers.Push((dest.x, dest.y, 1));
                        lasers.Push((dest.x, dest.y, 3));
                        break;
                    case 3:
                        lasers.Push((dest.x, dest.y, laser.dir));
                        break;
                }
            }
            else if (destChar == '/')
            {
                // directions: n = 0, e = 1, s = 2, w = 3
                switch (laser.dir)
                {
                    case 0:
                        lasers.Push((dest.x, dest.y, 1));
                        break;
                    case 1:
                        lasers.Push((dest.x, dest.y, 0));
                        break;
                    case 2:
                        lasers.Push((dest.x, dest.y, 3));
                        break;
                    case 3:
                        lasers.Push((dest.x, dest.y, 2));
                        break;
                }
            }
            else if (destChar == '\\')
            {
                // directions: n = 0, e = 1, s = 2, w = 3
                switch (laser.dir)
                {
                    case 0:
                        lasers.Push((dest.x, dest.y, 3));
                        break;
                    case 1:
                        lasers.Push((dest.x, dest.y, 2));
                        break;
                    case 2:
                        lasers.Push((dest.x, dest.y, 1));
                        break;
                    case 3:
                        lasers.Push((dest.x, dest.y, 0));
                        break;
                }
            }
        }

        var total = 0;
        for (int i = 0; i < energized.Length; i++)
        {
            for (int j = 0; j < energized[i].Length; j++)
            {
                if (energized[i][j] == '#') total++;
            }
        }
        return total;
    }
}