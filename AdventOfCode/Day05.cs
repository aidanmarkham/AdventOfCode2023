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
            "C:\\Development\\Aesthetician Labs\\AdventOfCode2023\\AdventOfCode\\Day05_Test.txt");

        var seeds = lines[0].Split(':')[1].Trim().Split(' ');
        var ranges = new List<(long, long)>();

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
        
        
        // go through the seed ranges 
        for (int i = 0; i < ranges.Count; i++)
        {
            // this stores the current stage of ranges
            var currentRanges = new List<(long, long)>();
            currentRanges.Add(ranges[i]);

            // this stores the next ranges to run 
            var nextRanges = new List<(long, long)>();
            
            
            // go through all maps 
            for (int m = 0; m < maps.Count; m++)
            {
                if(currentRanges.Count > 0) Console.WriteLine("Current Ranges: ");
                for (int c = 0; c < currentRanges.Count; c++)
                {
                    Console.WriteLine(" - (" + currentRanges[c].Item1 + ", " + currentRanges[c].Item2 + ")");
                }
                
                // go through all the ranges we currently have
                for (int j = 0; j < currentRanges.Count; j++)
                {
                    // turn current range into start and end
                    long r1 = currentRanges[j].Item1;
                    long r2 = r1 + currentRanges[j].Item2 - 1;


                    var map = maps[m];
                    // go through the ranges in the map
                    for (int r = 0; r < map.Count; r++)
                    {
                        var m1 = map[r].Item2;
                        var m2 = m1 + map[r].Item3;

                        var offset = map[r].Item1 - map[r].Item2;
                        
                        Console.WriteLine("Comparing Ranges: (" + r1 + ", " + r2 + "), (" + m1 + ", " + m2 + ")");
                        
                        // the ranges overlap
                        if (m2 >= r1 && m1 <= r2)
                        {
                            var x1 = Math.Max(r1, m1);
                            var x2 = Math.Min(r2, m2);

                            nextRanges.Add((x1 + offset, x2 - x1 + 1));
                        }
                    }
                }

                // now that we've gone through all current ranges, nextRanges will have the ranges we need for the next map session
                currentRanges = nextRanges;
                nextRanges = new List<(long, long)>();
            }
            
            if(currentRanges.Count > 0) Console.WriteLine("Current Ranges: ");
            for (int c = 0; c < currentRanges.Count; c++)
            {
                Console.WriteLine(" - (" + currentRanges[c].Item1 + ", " + currentRanges[c].Item2 + ")");
            }
        }

        
    }
}