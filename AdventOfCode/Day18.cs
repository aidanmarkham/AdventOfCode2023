using System.Data;

public static class Day18
{
    public static void DoPartOne()
    {
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

        int cellCount = 0;
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    Console.Write("#");
                    cellCount++;
                }
                else
                {
                    Console.Write(" ");
                }
            }

            Console.Write("\n");
        }
        Console.WriteLine("Width: " + map.GetLength(1) + " Height: " + map.GetLength(0));
        Console.WriteLine("Cells: " + cellCount);
    }

    public static void DoPartTwo()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day18_Test.txt");

        (long x, long y) currentLocation = (0, 0);

        var locations = new List<(long x, long y)>();
        locations.Add(currentLocation);

        // add all locations to list
        for (int i = 0; i < lines.Length; i++)
        {
            char dir = lines[i].Split(' ')[2][^2];
            int dist = int.Parse(lines[i].Split(' ')[2].Substring(2,5), System.Globalization.NumberStyles.HexNumber);

            //char dir = lines[i].Split(' ')[0][0];
            //int dist = int.Parse(lines[i].Split(' ')[1]);

            for (int j = 0; j < dist; j++)
            {
                switch (dir)
                {
                    case '3':
                    case 'U':
                        currentLocation.y -= 1;
                        break;
                    case '1':
                    case 'D':
                        currentLocation.y += 1;
                        break;
                    case '2':
                    case 'L':
                        currentLocation.x -= 1;
                        break;
                    case '0':
                    case 'R':
                        currentLocation.x += 1;
                        break;
                }

                locations.Add(currentLocation);
            }
        }

        (long x, long y) min = (long.MaxValue, long.MaxValue);
        (long x, long y) max = (long.MinValue, long.MinValue);

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
        
        max.x -= min.x;
        max.y -= min.y;
        min.x -= min.x;
        min.y -= min.y;

        long cellCount = 0;
        Console.WriteLine("Total Height: " + (max.y - min.y) + " Total Width: " + (max.x - min.x) + " Locations: " + locations.Count);
        for (long y = min.y -1; y <= max.y + 1; y++)
        {
            bool inside = false;
            bool crossing = false;
            int entryOrientation = -1;
            for (long x = min.x -1; x <= max.x + 1; x++)
            {
                bool onEdge = locations.Contains((x, y));
                if (onEdge) // we're on an edge
                {
                    // we just started crossing 
                    if (!crossing)
                    {
                        bool edgeAbove = locations.Contains((x, y - 1));
                        bool edgeBelow = locations.Contains((x, y + 1));
                        if (edgeAbove && edgeBelow)
                        {
                            entryOrientation = 0;
                        }
                        else if (edgeAbove)
                        {
                            entryOrientation = 1;
                        }
                        else if (edgeBelow)
                        {
                            entryOrientation = 2;
                        }
                        else
                        {
                            entryOrientation = -1;
                        }

                        crossing = true;
                    }
                }
                else // we're not on an edge
                {
                    if (crossing) // but we just were 
                    {
                        // information for the edge we just exited
                        bool edgeAbove = locations.Contains((x-1, y - 1));
                        bool edgeBelow = locations.Contains((x-1, y + 1));
                        var exitOrientation = -1;
                        if (edgeAbove && edgeBelow)
                        {
                            exitOrientation = 0;
                        }
                        else if (edgeAbove)
                        {
                            exitOrientation = 1;
                        }
                        else if (edgeBelow)
                        {
                            exitOrientation = 2;
                        }
                        
                        switch (entryOrientation)
                        {
                            case 0: // we entered a full edge, we have to be inside now
                                inside = !inside;
                                break;
                            case 1: // edge ONLY above
                                if (exitOrientation != 1)
                                {
                                    inside = !inside;
                                }
                                break;
                            case 2: // edge ONLY below
                                if (exitOrientation != 2)
                                {
                                    inside = !inside;
                                }
                                break;
                        }

                        crossing = false;
                        
                    }
                }

                if (onEdge || inside)
                {
                    cellCount++;
                }
            }
        }

        Console.WriteLine("Width: " + max.x + " Height: " + max.y);
        Console.WriteLine("Cells: " + cellCount);
    }
}