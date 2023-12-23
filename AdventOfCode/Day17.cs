using System.Collections;
using System.Data;
using System.Net;
using System.Runtime.CompilerServices;

public static class Day17
{
    public static void DoPartOne()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day17.txt");
        var map = new int[lines.Length, lines[0].Length];

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                map[y, x] = int.Parse("" + lines[y][x]);
            }
        }

        // print out the map 

        var heatMap = new Dictionary<(int x, int y, int dir, int heat), int>();

        (int x, int y) start = (0, 0);
        (int x, int y) destination = (lines[0].Length - 1, lines.Length - 1);

        // location, dir, heatloss, straights
        // dir: east = 0, south = 1, west = 2, north = 3
        var stack = new Stack<((int x, int y) pos, int dir, int heat, int straights)>();
        stack.Push((start, 0, 0, 0));
        stack.Push((start, 1, 0, 0));
        stack.Push((start, 2, 0, 0));
        stack.Push((start, 3, 0, 0));

        int minHeatLoss = 940;
        var bestState = stack.Peek();
        long iterations = 0;
        while (stack.Count > 0)
        {
            iterations++;

            var state = stack.Pop();

            if (iterations % 10000 == 0) Console.WriteLine("It: " + iterations + " St: " + stack.Count);
            // prune if this branch is already worse than the best one 

            if (state.heat >= minHeatLoss) continue;

            // record best ways to the destination
            if (state.pos == destination)
            {
                if (state.heat < minHeatLoss)
                {
                    minHeatLoss = state.heat;
                }
                else
                {
                    continue;
                }

                Console.WriteLine(
                    "We made it to the end with " + state.heat + " heatloss. Stack height: " + stack.Count +
                    " Iterations: " + iterations);
                continue;
            }

            // build representation for the three possible steps from here
            var left = state.pos;
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
            if (left.y >= 0 && left.y < map.GetLength(0) && left.x >= 0 && left.x < map.GetLength(1))
            {
                var calculatedHeat = state.heat + map[left.y, left.x];

                // prune if this would result in a worse case than we already have found
                if (calculatedHeat > minHeatLoss) continue;

                // if we've never been to that tile or we had in a less efficient manner
                if (heatMap.ContainsKey((left.x, left.y, leftDir, 0)))
                {
                    if (heatMap[(left.x, left.y, leftDir, 0)] > calculatedHeat)
                    {
                        heatMap[(left.x, left.y, leftDir, 0)] = calculatedHeat;
                        stack.Push((left, leftDir, calculatedHeat, 0));
                    }
                }
                else
                {
                    heatMap.Add((left.x, left.y, leftDir, 0), calculatedHeat);
                    stack.Push((left, leftDir, calculatedHeat, 0));
                }
            }

            // case where we go right
            if (right.y >= 0 && right.y < map.GetLength(0) && right.x >= 0 && right.x < map.GetLength(1))
            {
                var calculatedHeat = state.heat + map[right.y, right.x];

                // prune if this would result in a worse case than we already have found
                if (calculatedHeat > minHeatLoss) continue;

                // if we've never been to that tile or we had in a less efficient manner
                if (heatMap.ContainsKey((right.x, right.y, rightDir, 0)))
                {
                    if (heatMap[(right.x, right.y, rightDir, 0)] > calculatedHeat)
                    {
                        heatMap[(right.x, right.y, rightDir, 0)] = calculatedHeat;
                        stack.Push((right, rightDir, calculatedHeat, 0));
                    }
                }
                else
                {
                    heatMap.Add((right.x, right.y, rightDir, 0), calculatedHeat);
                    stack.Push((right, rightDir, calculatedHeat, 0));
                }
            }

            if (straight.y >= 0 && straight.y < map.GetLength(0) && straight.x >= 0 &&
                straight.x < map.GetLength(1) &&
                state.straights < 2)
            {
                var calculatedHeat = state.heat + map[straight.y, straight.x];

                // prune if this would result in a worse case than we already have found
                if (calculatedHeat > minHeatLoss) continue;

                // if we've never been to that tile or we had in a less efficient manner
                if (heatMap.ContainsKey((straight.x, straight.y, straightDir, state.straights + 1)))
                {
                    if (heatMap[(straight.x, straight.y, straightDir, state.straights + 1)] >= calculatedHeat)
                    {
                        heatMap[(straight.x, straight.y, straightDir, state.straights + 1)] = calculatedHeat;
                        stack.Push((straight, straightDir, calculatedHeat, state.straights + 1));
                    }
                }
                else
                {
                    heatMap.Add((straight.x, straight.y, straightDir, state.straights + 1), calculatedHeat);
                    stack.Push((straight, straightDir, calculatedHeat, state.straights + 1));
                }
            }
        }

        Console.WriteLine("Min Heat Loss: " + minHeatLoss + " iterations: " + iterations);
    }

    public static void DoPartTwo()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day17.txt");
        var map = new int[lines.Length, lines[0].Length];

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                map[y, x] = int.Parse("" + lines[y][x]);
            }
        }

        // print out the map 

        var heatMap = new Dictionary<(int x, int y, int dir, int straights), int>();

        (int x, int y) start = (0, 0);
        (int x, int y) destination = (lines[0].Length - 1, lines.Length - 1);

        int maxDist = 10;
        int minDist = 4;

        // location, dir, heatloss, straights
        // dir: east = 0, south = 1, west = 2, north = 3
        var stack = new Stack<((int x, int y) pos, int dir, int heat, int straights, string path)>();
        stack.Push((start, 0, 0, 0, ""));
        stack.Push((start, 1, 0, 0, ""));
        stack.Push((start, 2, 0, 0, ""));
        stack.Push((start, 3, 0, 0, ""));

        int minHeatLoss = 1500;
        string bestPath = "";
        long iterations = 0;

        while (stack.Count > 0)
        {
            iterations++;

            var state = stack.Pop();

            if (iterations % 1000000 == 0)
                Console.WriteLine("It: " + iterations + " St: " + stack.Count + " Ht: " + state.heat);
            // prune if this branch is already worse than the best one 

            if (state.heat >= minHeatLoss) continue;

            // record best ways to the destination
            if (state.pos == destination && state.straights >= minDist)
            {
                if (state.heat < minHeatLoss)
                {
                    minHeatLoss = state.heat;
                    bestPath = state.path + "(" + state.pos.x + ", " + state.pos.y + ")";
                }
                else
                {
                    continue;
                }

                Console.WriteLine(
                    "We made it to the end with " + state.heat + " heatloss. Stack height: " + stack.Count +
                    " Iterations: " + iterations);
                continue;
            }

            // build representation for the three possible steps from here
            var left = state.pos;
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

            var newStates = new List<((int x, int y) pos, int dir, int heat, int straights, string path)>();

            // case where we go left
            if (left.y >= 0 && left.y < map.GetLength(0) && left.x >= 0 && left.x < map.GetLength(1) &&
                state.straights >= minDist)
            {
                var calculatedHeat = state.heat + map[left.y, left.x];

                // prune if this would result in a worse case than we already have found
                if (calculatedHeat > minHeatLoss) continue;

                // if we've never been to that tile or we had in a less efficient manner
                if (heatMap.ContainsKey((left.x, left.y, leftDir, 1)))
                {
                    if (heatMap[(left.x, left.y, leftDir, 1)] > calculatedHeat)
                    {
                        heatMap[(left.x, left.y, leftDir, 1)] = calculatedHeat;
                        newStates.Add((left, leftDir, calculatedHeat, 1,
                            state.path + "(" + state.pos.x + ", " + state.pos.y + ")"));
                    }
                }
                else
                {
                    heatMap.Add((left.x, left.y, leftDir, 1), calculatedHeat);
                    newStates.Add((left, leftDir, calculatedHeat, 1,
                        state.path + "(" + state.pos.x + ", " + state.pos.y + ")"));
                }
            }

            if (right.y >= 0 && right.y < map.GetLength(0) && right.x >= 0 && right.x < map.GetLength(1) &&
                state.straights >= minDist)
            {
                var calculatedHeat = state.heat + map[right.y, right.x];

                // prune if this would result in a worse case than we already have found
                if (calculatedHeat > minHeatLoss) continue;

                // if we've never been to that tile or we had in a less efficient manner
                if (heatMap.ContainsKey((right.x, right.y, rightDir, 1)))
                {
                    if (heatMap[(right.x, right.y, rightDir, 1)] > calculatedHeat)
                    {
                        heatMap[(right.x, right.y, rightDir, 1)] = calculatedHeat;
                        newStates.Add((right, rightDir, calculatedHeat, 1,
                            state.path + "(" + state.pos.x + ", " + state.pos.y + ")"));
                    }
                }
                else
                {
                    heatMap.Add((right.x, right.y, rightDir, 1), calculatedHeat);
                    newStates.Add((right, rightDir, calculatedHeat, 1,
                        state.path + "(" + state.pos.x + ", " + state.pos.y + ")"));
                }
            }

            if (straight.y >= 0 && straight.y < map.GetLength(0) && straight.x >= 0 &&
                straight.x < map.GetLength(1) &&
                state.straights < maxDist)
            {
                var calculatedHeat = state.heat + map[straight.y, straight.x];

                // prune if this would result in a worse case than we already have found
                if (calculatedHeat > minHeatLoss) continue;

                // if we've never been to that tile or we had in a less efficient manner
                if (heatMap.ContainsKey((straight.x, straight.y, straightDir, state.straights + 1)))
                {
                    if (heatMap[(straight.x, straight.y, straightDir, state.straights + 1)] >= calculatedHeat)
                    {
                        heatMap[(straight.x, straight.y, straightDir, state.straights + 1)] = calculatedHeat;
                        newStates.Add((straight, straightDir, calculatedHeat, state.straights + 1,
                            state.path + "(" + state.pos.x + ", " + state.pos.y + ")"));
                    }
                }
                else
                {
                    heatMap.Add((straight.x, straight.y, straightDir, state.straights + 1), calculatedHeat);
                    newStates.Add((straight, straightDir, calculatedHeat, state.straights + 1,
                        state.path + "(" + state.pos.x + ", " + state.pos.y + ")"));
                }
            }
            
            // sort new states
            newStates = newStates.OrderBy(x => x.pos.x + x.pos.y).ToList();
            for (int i = 0; i < newStates.Count; i++)
            {
                stack.Push(newStates[i]);
            }
        }

        Console.WriteLine("Min Heat Loss: " + minHeatLoss + " iterations: " + iterations + "\n" + bestPath);
    }
}