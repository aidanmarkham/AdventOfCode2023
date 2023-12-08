public static class Day05
{
    public static void DoPartOne()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\Aesthetician Labs\\AdventOfCode2023\\AdventOfCode\\Day05_Test.txt");

        var seeds = lines[0].Split(':')[1].Trim().Split(' ');


        int index = 3;

        // gen seed to soil map
        var maps = new List<List<(long, long, long)>>();
        while (index < lines.Length)
        {
            var line = lines[index];
            // we're at a label
            if (!char.IsDigit(line[0]))
            {
                index++;
                continue;
            }

            var map = new List<(long, long, long)>();
            while (lines[index].Trim() != "")
            {
                var dest = long.Parse(lines[index].Split(' ')[0]);
                var source = long.Parse(lines[index].Split(' ')[1]);
                var length = long.Parse(lines[index].Split(' ')[2]);
                map.Add((dest, source, length));
                index++;

                if (index == lines.Length) break;
            }

            maps.Add(map);
            index++;
        }

        long lowestLocation = long.MaxValue;

        for (int i = 0; i < seeds.Length; i++)
        {
            long seed = long.Parse(seeds[i]);

            Console.Write("Seed: " + seed);
            for (int m = 0; m < maps.Count; m++)
            {
                var map = maps[m];
                for (int j = 0; j < map.Count; j++)
                {
                    var dest = map[j].Item1;
                    var source = map[j].Item2;
                    var length = map[j].Item3;

                    if (seed >= source && seed < source + length)
                    {
                        var dist = seed - source;
                        seed = dest + dist;
                        break;
                    }
                }

                Console.Write(" " + seed);
            }

            Console.Write("\n");
            if (seed < lowestLocation)
            {
                lowestLocation = seed;
            }
        }

        Console.WriteLine("Lowest Location: " + lowestLocation);
    }

    public static void DoPartTwo()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\Aesthetician Labs\\AdventOfCode2023\\AdventOfCode\\Day05.txt");

        var seeds = lines[0].Split(':')[1].Trim().Split(' ');
        var ranges = new List<(long, long)>();

        // build list of seed ranges
        for (int i = 0; i < seeds.Length; i += 2)
        {
            ranges.Add((long.Parse(seeds[i]), long.Parse(seeds[i + 1])));
        }

        int index = 3;

        // gen seed to soil map
        var maps = new List<List<(long, long, long)>>();
        while (index < lines.Length)
        {
            var line = lines[index];
            // we're at a label
            if (!char.IsDigit(line[0]))
            {
                index++;
                continue;
            }

            var map = new List<(long, long, long)>();
            while (lines[index].Trim() != "")
            {
                var dest = long.Parse(lines[index].Split(' ')[0]);
                var source = long.Parse(lines[index].Split(' ')[1]);
                var length = long.Parse(lines[index].Split(' ')[2]);
                map.Add((dest, source, length));
                index++;

                if (index == lines.Length) break;
            }

            maps.Add(map);
            index++;
        }

        // this stores the current stage of ranges
        var currentRanges = new Stack<(long, long)>();
        var lowest = long.MaxValue;
        // go through the seed ranges 
        for (int i = 0; i < ranges.Count; i++)
        {
            currentRanges.Clear();
            // add starting range to this 
            currentRanges.Push(ranges[i]);

            // this stores the next ranges to run 
            var nextRanges = new Stack<(long, long)>();
            
            // go through all maps 
            for (int m = 0; m < maps.Count; m++)
            {
                Console.WriteLine("\nProcessing Map: " + m);
                // go through all the ranges we currently have
                while (currentRanges.Count > 0)
                {
                    var currentRange = currentRanges.Pop();
                 
                    Console.Write("Current Range: " + currentRange);
                    // turn current range into start and end
                    long r1 = currentRange.Item1;
                    long r2 = r1 + currentRange.Item2 - 1;

                    var map = maps[m];

                    var hadFit = false;
                    // go through the ranges in the map
                    for (int r = 0; r < map.Count; r++)
                    {
                        var m1 = map[r].Item2;
                        var m2 = m1 + map[r].Item3;

                        var offset = map[r].Item1 - map[r].Item2;

                        // entire range fits
                        if (m1 <= r1 && m2 >= r2)
                        {
                            
                            
                            // convert it and add it to the next ranges in it's entirety
                            var bottom = r1;
                            var top = r2;
                            nextRanges.Push((bottom + offset, top - bottom + 1));
                            Console.Write("\n   Map: " + map[r] +" Full fit. Creating Range: " + (bottom + offset, top - bottom + 1));
                            hadFit = true;
                            break;
                        }
                        // partial fit (range below map, m1 is inside our range)
                        if (r1 <= m1 && m1 <= r2)
                        {
                            
                            // take only the bit that counts in the range and add it to next ranges
                            nextRanges.Push((m1 + offset, r2 - m1 + 1));
                            Console.Write("\n   Map: " + map[r] +" Partial fit below. Creating Range: " + (m1 + offset, r2 - m1 + 1));
                            // add the remaining bit to the current range to go around again
                            currentRanges.Push((r1, m1 - r1));

                            hadFit = true;
                            break;
                        }
                        // partial fit (range above map, m2 is inside our range)
                        if (r1 <= m2 && m2 <= r2)
                        {
                            // take only the bit that counts in the range and add it to next ranges
                            // r1 -> m2
                            nextRanges.Push((r1 + offset, m2 - r1 + 1));
                            Console.Write("\n   Map: " + map[r] +" Partial fit above. Creating Range: " + (r1 + offset, m2 - r1 + 1));
                            // add the remaining bit to the current range to go around again
                            // m2 -> r2
                            currentRanges.Push((m2+1, r2 - m2));

                            hadFit = true;
                            break;
                        }
                    }

                    // if it didn't apply to any of the ranges, go ahead and consider it done
                    if (!hadFit)
                    {
                        Console.Write("\n   No fit.");
                        nextRanges.Push(currentRange);
                    }
                    
                    Console.Write("\n");
                }

                // now that we've gone through all current ranges, nextRanges will have the ranges we need for the next map session
                currentRanges = nextRanges;
                nextRanges = new Stack<(long, long)>();
            }

            
            foreach (var range in currentRanges)
            {
                Console.WriteLine("Range: " + range);
                if (range.Item1 < lowest)
                {
                    lowest = range.Item1;
                }
            }
        }
        
        Console.WriteLine("Lowest: " + lowest);
    }
}