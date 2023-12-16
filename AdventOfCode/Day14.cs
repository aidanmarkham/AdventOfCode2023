using System.Diagnostics;

public static class Day14
{
    public static void DoPartOne()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day14.txt");

        bool rocksAtRest = false;

        // tumble all the rocks north
        while (!rocksAtRest)
        {
            rocksAtRest = true;
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    // if we've found a rock
                    if (lines[y][x] == 'O')
                    {
                        // run up vertically
                        bool resting = true;

                        for (int i = 0; i <= y; i++)
                        {
                            // it's resting on a square rock
                            if (lines[y - i][x] == '#')
                            {
                                break;
                            }

                            // it's floating in the air!
                            if (lines[y - i][x] == '.')
                            {
                                rocksAtRest = false;
                                resting = false;
                                break;
                            }
                        }

                        // slide it north
                        if (!resting && lines[y - 1][x] == '.')
                        {
                            lines[y - 1] = lines[y - 1].ReplaceAt(x, 'O');
                            lines[y] = lines[y].ReplaceAt(x, '.');
                        }
                    }

                    //Console.Write(lines[y][x]);
                }
                //Console.Write("\n");
            }
            //Console.Write("\n");
        }

        var totalWeight = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var lineWeight = lines.Length - i;

            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == 'O')
                {
                    totalWeight += lineWeight;
                }
            }
        }

        Console.WriteLine("Total Weight: " + totalWeight);
    }


    public static void DoPartTwo()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day14.txt");

        bool rocksAtRest = false;

        int cycles = 1000000000;
        int maxCycleLength = 100;
        
        List<int> weights = new List<int>();
        
        for (int c = 0; c < cycles; c++)
        {
            // actually do the cycles
            for (int dir = 0; dir < 4; dir++)
            {
                rocksAtRest = false;
                while (!rocksAtRest)
                {
                    rocksAtRest = true;
                    for (int y = 0; y < lines.Length; y++)
                    {
                        for (int x = 0; x < lines[y].Length; x++)
                        {
                            // if we've found a rock
                            if (lines[y][x] == 'O')
                            {
                                bool resting = true;

                                if (dir == 0)
                                {
                                    for (int i = 0; i <= y; i++)
                                    {
                                        // it's resting on a square rock
                                        if (lines[y - i][x] == '#')
                                        {
                                            break;
                                        }

                                        // it's floating in the air!
                                        if (lines[y - i][x] == '.')
                                        {
                                            rocksAtRest = false;
                                            resting = false;
                                            break;
                                        }
                                    }

                                    // slide it north
                                    if (!resting && lines[y - 1][x] == '.')
                                    {
                                        lines[y - 1] = lines[y - 1].ReplaceAt(x, 'O');
                                        lines[y] = lines[y].ReplaceAt(x, '.');
                                    }
                                }

                                if (dir == 1)
                                {
                                    for (int i = 0; i <= x; i++)
                                    {
                                        // it's resting on a square rock
                                        if (lines[y][x - i] == '#')
                                        {
                                            break;
                                        }

                                        // it's floating in the air!
                                        if (lines[y][x - i] == '.')
                                        {
                                            rocksAtRest = false;
                                            resting = false;
                                            break;
                                        }
                                    }

                                    // slide it west
                                    if (!resting && lines[y][x - 1] == '.')
                                    {
                                        lines[y] = lines[y].ReplaceAt(x - 1, 'O');
                                        lines[y] = lines[y].ReplaceAt(x, '.');
                                    }
                                }

                                if (dir == 2)
                                {
                                    for (int i = 0; i < lines.Length - y; i++)
                                    {
                                        // it's resting on a square rock
                                        if (lines[y + i][x] == '#')
                                        {
                                            break;
                                        }

                                        // it's floating in the air!
                                        if (lines[y + i][x] == '.')
                                        {
                                            rocksAtRest = false;
                                            resting = false;
                                            break;
                                        }
                                    }

                                    // slide it south
                                    if (!resting && lines[y + 1][x] == '.')
                                    {
                                        lines[y + 1] = lines[y + 1].ReplaceAt(x, 'O');
                                        lines[y] = lines[y].ReplaceAt(x, '.');
                                    }
                                }

                                if (dir == 3)
                                {
                                    for (int i = 0; i < lines[y].Length - x; i++)
                                    {
                                        // it's resting on a square rock
                                        if (lines[y][x + i] == '#')
                                        {
                                            break;
                                        }

                                        // it's floating in the air!
                                        if (lines[y][x + i] == '.')
                                        {
                                            rocksAtRest = false;
                                            resting = false;
                                            break;
                                        }
                                    }

                                    // slide it east
                                    if (!resting && lines[y][x + 1] == '.')
                                    {
                                        lines[y] = lines[y].ReplaceAt(x + 1, 'O');
                                        lines[y] = lines[y].ReplaceAt(x, '.');
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
            // calculate the weight
            var totalWeight = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                var lineWeight = lines.Length - i;

                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == 'O')
                    {
                        totalWeight += lineWeight;
                    }
                }
            }
            
            //collect the weights
            weights.Add(totalWeight);

            if (weights.Count > maxCycleLength * 2)
            {
                for (int i = 2; i < maxCycleLength; i++)
                {
                    // create a string from the last i weights

                    var a = "";
                    for (int j = 0; j < i; j++)
                    {
                        a += weights[^(j+1)].ToString();
                    }

                    var b = "";
                    for (int j = i; j < i * 2; j++)
                    {
                        b += weights[^(j+1)].ToString();
                    }
                    if (a == b && c + i < cycles)
                    {
                        var len = i;
                        Console.WriteLine("Cycle of length " + len + " found at " + c + ": " + a);
                        
                        // skip forward
                        while (c + len < cycles)
                        {
                            c += len;
                        }
                        break;
                    }
                }
            }
        }
        
        // calculate the weight
        var weight = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var lineWeight = lines.Length - i;

            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == 'O')
                {
                    weight += lineWeight;
                }
            }
        }
        
        Console.WriteLine("Total Weight: " + weight);
    }
}