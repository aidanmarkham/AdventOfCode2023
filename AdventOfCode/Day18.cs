using System.Data;

public static class Day18
{
    public static void DoPartOne()
    {
        bool drawMaps = false;
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day18.txt");

        (int x, int y) currentLocation = (0, 0);
        (int x, int y) startLocation = (0, 0);

        var locations = new List<(int x, int y)>();
        locations.Add(currentLocation);

        // add all locations to list
        for (int i = 0; i < lines.Length; i++)
        {
            char dir = lines[i].Split(' ')[0][0];
            int dist = int.Parse(lines[i].Split(' ')[1]);

            for (int j = 0; j < dist; j++)
            {
                switch (dir)
                {
                    case 'U':
                        currentLocation.y -= 1;
                        break;
                    case 'D':
                        currentLocation.y += 1;
                        break;
                    case 'L':
                        currentLocation.x -= 1;
                        break;
                    case 'R':
                        currentLocation.x += 1;
                        break;
                }

                locations.Add(currentLocation);
            }
        }

        (int x, int y) min = (int.MaxValue, int.MaxValue);
        (int x, int y) max = (int.MinValue, int.MinValue);

        foreach (var loc in locations)
        {
            if (loc.x < min.x) min.x = loc.x;
            if (loc.x > max.x) max.x = loc.x;
            if (loc.y < min.y) min.y = loc.y;
            if (loc.y > max.y) max.y = loc.y;
        }

        // adjust everything to be 0 indexed
        for (int i = 0; i < locations.Count; i++)
        {
            var loc = locations[i];

            loc.x -= min.x;
            loc.y -= min.y;

            locations[i] = loc;
        }

        startLocation.x -= min.x;
        startLocation.y -= min.y;
        max.x -= min.x;
        max.y -= min.y;
        min.x -= min.x;
        min.y -= min.y;

        var map = new int[max.y + 1, max.x + 1];
        for (int i = 0; i < locations.Count; i++)
        {
            var loc = locations[i];
            map[loc.y, loc.x] = 1;
        }

        int cellCount = 0;
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    if(drawMaps)Console.Write("#");
                    cellCount++;
                }
                else
                {
                    if(drawMaps)Console.Write(" ");
                }
            }
            if(drawMaps) Console.Write("\n");
        }
        if(drawMaps) Console.Write("\n");
        var edgeCells = cellCount;
        
        
        // confirmed one down and to the right is inside the pool for test and actual data
        startLocation.x += 1;
        startLocation.y += 1;
        map[startLocation.y, startLocation.x] = 1;
        
        // flood fill from start location
        var stack = new Stack<(int x, int y)>();
        stack.Push(startLocation);
        while (stack.Count > 0)
        {
            var loc = stack.Pop();
            if (map[loc.y + 1, loc.x] < 1)
            {
                map[loc.y + 1, loc.x] = 1;
                stack.Push((loc.x, loc.y + 1));
            }

            if (map[loc.y - 1, loc.x] < 1)
            {
                map[loc.y - 1, loc.x] = 1;
                stack.Push((loc.x, loc.y - 1));
            }

            if (map[loc.y, loc.x + 1] < 1)
            {
                map[loc.y, loc.x + 1] = 1;
                stack.Push((loc.x + 1, loc.y));
            }

            if (map[loc.y, loc.x - 1] < 1)
            {
                map[loc.y, loc.x - 1] = 1;
                stack.Push((loc.x - 1, loc.y));
            }
        }

        cellCount = 0;
        for (int y = 0; y < map.GetLength(0); y++)
        {
            var lineTotal = 0;
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    if(drawMaps)Console.Write("#");
                    cellCount++;
                    lineTotal++;
                }
                else
                {
                    if(drawMaps) Console.Write(" ");
                }
            }

            if (drawMaps)
            {
                Console.Write("\n");
            }
            else
            {
                //Console.WriteLine(y + ": " + lineTotal);
            }
        }
        
        Console.WriteLine("Edge Cells: " + edgeCells);
        Console.WriteLine("Cells: " + cellCount);
    }

    public static void DoPartTwo()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day18.txt");

        (long x, long y) currentLocation = (0, 0);

        var lineSegments = new List<(long x1, long y1, long x2, long y2)>();

        // bulid list of all line segments represented
        for (int i = 0; i < lines.Length; i++)
        {
            char dir = lines[i].Split(' ')[2][^2];
            long dist = long.Parse(lines[i].Split(' ')[2].Substring(2, 5), System.Globalization.NumberStyles.HexNumber);

            //char dir = lines[i].Split(' ')[0][0];
            //int dist = int.Parse(lines[i].Split(' ')[1]);

            var previousLocation = currentLocation;

            switch (dir)
            {
                case '3':
                case 'U':
                    currentLocation.y -= dist;
                    break;
                case '1':
                case 'D':
                    currentLocation.y += dist;
                    break;
                case '2':
                case 'L':
                    currentLocation.x -= dist;
                    break;
                case '0':
                case 'R':
                    currentLocation.x += dist;
                    break;
            }

            lineSegments.Add((previousLocation.x, previousLocation.y, currentLocation.x, currentLocation.y));
        }

        // calc mins and maxes
        (long x, long y) min = (long.MaxValue, long.MaxValue);
        (long x, long y) max = (long.MinValue, long.MinValue);

        foreach (var line in lineSegments)
        {
            if (line.x1 < min.x) min.x = line.x1;
            if (line.x2 < min.x) min.x = line.x2;
            if (line.x1 > max.x) max.x = line.x1;
            if (line.x2 > max.x) max.x = line.x2;
            if (line.y1 < min.y) min.y = line.y1;
            if (line.y2 < min.y) min.y = line.y2;
            if (line.y1 > max.y) max.y = line.y1;
            if (line.y2 > max.y) max.y = line.y2;
        }

        long total = 0;
        foreach (var line in lineSegments)
        {
            // horizontal
            if (line.y1 == line.y2)
            {
                total += Math.Abs(line.x2 - line.x1);
            }
            else
            {
                total += Math.Abs(line.y2 - line.y1);
            }
        }

        var edgeCells = total;
        
        for (long y = min.y; y <= max.y; y++)
        {
            if(y % 1000000 == 0) Console.WriteLine("y: " + y + "(" + min.y + "," + max.y+")");
            // y is the line we're currently evaluating
            var intersects = new List<(long x, long o)>();
            var parallels = new List<(long x1, long x2)>();
            foreach (var line in lineSegments)
            {
                if (Math.Sign(line.y1 - y) != Math.Sign(line.y2 - y))
                {
                    var lineMin = Math.Min(line.y1, line.y2);
                    var lineMax = Math.Max(line.y1, line.y2);

                    // orientation:
                    // -1 = error
                    // 0 = line above and below
                    // 1 = line above
                    // 2 = line below
                    var orientation = -1;
                    if (lineMin < y && lineMax > y)
                    {
                        orientation = 0;
                    }
                    else if (lineMin < y) // line above
                    {
                        orientation = 1;
                    }
                    else if (lineMax > y) // line below
                    {
                        orientation = 2;
                    }
                    intersects.Add((line.x1, orientation));
                }

                if (line.y1 == line.y2 && line.y1 == y)
                {
                    parallels.Add((line.x1, line.x2));
                }
            }
            
            intersects = intersects.OrderBy(x => x).ToList();
            
            //Console.WriteLine("Line " + y + ": " + intersects.Count + " intersects, " + parallels.Count +" parallels");
            bool inside = false;
            var flips = 0;
            var edgeCount = 0;
            
            // go segment by segment, stopping one before the last one
            for (int i = 0; i < intersects.Count - 1; i++)
            {
                var a = intersects[i];
                var b = intersects[i + 1];

                // is the current segment covered with a parallel
                var intersection = false;
                for (int p = 0; p < parallels.Count; p++)
                {
                    // if this intersect overlaps with a parallel
                    if (a.x == Math.Min(parallels[p].x1, parallels[p].x2) && 
                        b.x == Math.Max(parallels[p].x1, parallels[p].x2))
                    {
                        intersection = true;
                    }
                }
                
                if (a.o == 0) // a was a vertical line
                {
                    // so flip inside
                    inside = !inside;
                }
                else if(b.o != 0 && a.o != b.o && intersection) // the line continues on the other side
                {
                    inside = !inside;
                }
                
                if (intersection == false && inside) 
                {
                    total += (b.x - a.x) - 1;
                    flips++;
                }
            }

        }
        
        Console.WriteLine("Edge cells: " + edgeCells);
        Console.WriteLine("Total cells: " + total);
    }
}