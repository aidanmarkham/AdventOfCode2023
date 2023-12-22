using System.Data;
using System.Net;
using System.Runtime.CompilerServices;

public static class Day17
{
    public static void DoPartOne()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day17_Test.txt");

        // print out the map 

        List<List<int>> heatMap = new List<List<int>>();
        for (int y = 0; y < lines.Length; y++)
        {
            heatMap.Add(new List<int>());
            for (int x = 0; x < lines[y].Length; x++)
            {
                heatMap[y].Add(-1);
            }
        }

        (int x, int y) start = (0, 0);
        (int x, int y) destination = (lines[0].Length-1, lines.Length-1);

        // location, dir, heatloss, straights
        // dir: east = 0, south = 1, west = 2, north = 3
        var stack = new Stack<((int x, int y) pos, int dir, int heat, int straights, List<(int x, int y)> path)>();
        stack.Push((start, 0, 0, 0, new List<(int x, int y)>()));
        //stack.Push((start, 1, 0, 0, new List<(int x, int y)>()));
        //stack.Push((start, 2, 0, 0, new List<(int x, int y)>()));
        //stack.Push((start, 3, 0, 0, new List<(int x, int y)>()));
        while (stack.Count > 0)
        {
            //Console.WriteLine("Stack Height: " + stack.Count);
            var state = stack.Pop();
            
            if (state.pos == destination)
            {
                Console.WriteLine("We made it to the end with " + state.heat + " heatloss.");
                
                for (int y = 0; y < lines.Length; y++)
                {
                    for (int x = 0; x < lines[y].Length; x++)
                    {
                        if(state.path.Contains((x , y)) || state.pos == (x, y))
                        {
                            Console.Write("█");
                        }
                        else
                        {
                            Console.Write(lines[y][x]);
                        }
                    }
                    Console.Write("\n");
                }
                Console.Write("\n");
                continue;
            }
            
            var left= state.pos;
            var leftDir = state.dir;
            var right = state.pos;
            var rightDir = state.dir;
            var straight = state.pos;
            var straightDir = state.dir;

            switch (state.dir)
            {
                case 0:
                    straight.x++;
                    left.y--;
                    leftDir = 3;
                    right.y++;
                    rightDir = 1;
                    break;
                case 1:
                    straight.y++;
                    left.x++;
                    leftDir = 0;
                    right.x--;
                    rightDir = 2;
                    break;
                case 2:
                    straight.x--;
                    left.y++;
                    leftDir = 1;
                    right.y--;
                    rightDir = 3;
                    break;
                case 3:
                    straight.y--;
                    left.x--;
                    leftDir = 2;
                    right.x++;
                    rightDir = 0;
                    break;
            }

            // case where we go left
            if (left.y >= 0 && left.y < lines.Length && left.x >= 0 && left.x < lines[left.y].Length)
            {
                var calculatedHeat = state.heat + int.Parse(""+lines[left.y][left.x]);
                
                // if we've never been to that tile or we had in a less efficient manner
                if (heatMap[left.y][left.x] == -1 || heatMap[left.y][left.x] >= calculatedHeat)
                {
                    heatMap[left.y][left.x] = calculatedHeat;

                    var newList = new List<(int x, int y)>(state.path);
                    newList.Add(state.pos);
                    stack.Push((left, leftDir, calculatedHeat, 0, newList));
                }
            }
            
            // case where we go right
            if (right.y >= 0 && right.y < lines.Length && right.x >= 0 && right.x < lines[right.y].Length)
            {
                var calculatedHeat = state.heat + int.Parse(""+lines[right.y][right.x]);
                // if we've never been to that tile or we had in a less efficient manner
                if (heatMap[right.y][right.x] == -1 || heatMap[right.y][right.x] >= calculatedHeat)
                {
                    heatMap[right.y][right.x] = calculatedHeat;
                    
                    var newList = new List<(int x, int y)>(state.path);
                    newList.Add(state.pos);
                    stack.Push((right, rightDir, calculatedHeat, 0, newList));
                }
            }
            
            if (straight.y >= 0 && straight.y < lines.Length && straight.x >= 0 && straight.x < lines[straight.y].Length &&
                state.straights < 2)
            {
                var calculatedHeat = state.heat + int.Parse(""+lines[straight.y][straight.x]);
                // if we've never been to that tile or we had in a less efficient manner
                if (heatMap[straight.y][straight.x] == -1 || heatMap[straight.y][straight.x] >= calculatedHeat)
                {
                    heatMap[straight.y][straight.x] = calculatedHeat;
                    
                    var newList = new List<(int x, int y)>(state.path);
                    newList.Add(state.pos);
                    stack.Push((straight, straightDir, calculatedHeat, state.straights + 1, newList));
                }
            }
        }
    }
}