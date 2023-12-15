using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;

public static class Day12
{
    public static void DoPartOne()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day12.txt");

        var allPosibilities = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var map = line.Split(' ')[0];
            var countStrings = line.Split(' ')[1].Split(',');
            var counts = new List<int>();
            for (int j = 0; j < countStrings.Length; j++)
            {
                counts.Add(int.Parse(countStrings[j]));
            }


            var list = new List<string>();
            list = PermuteMap(list, map);
            int possibilities = 0;
            //Console.WriteLine("\nOriginal: " + map);
            for (int j = 0; j < list.Count; j++)
            {
                var matches = MatchesCounts(list[j], counts);
                //Console.WriteLine(" - " + list[j] + " - " + (matches ? "match" : " no match"));
                if (matches) possibilities++;
            }
            
            Console.WriteLine("Possibilities: " + possibilities);
            allPosibilities += possibilities;
        }
        
        Console.WriteLine("Total: " + allPosibilities);
    }

    public static void DoPartTwo()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day12_Test.txt");

        var allPosibilities = 0;
        
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var map = line.Split(' ')[0];
            var countStrings = line.Split(' ')[1].Split(',');
            var counts = new List<int>();
            for (int j = 0; j < countStrings.Length; j++)
            {
                counts.Add(int.Parse(countStrings[j]));
            }
            
            /*
            Console.WriteLine("Before: ");
            Console.Write("Map: " + map + " Counts: ");
            for (int j = 0; j< counts.Count; j++)
            {
                Console.Write(counts[j] + ", ");
            }
            Console.Write("\n");
            
            
            var origCounts = new List<int>(counts);
            for (int j = 0; j < 4; j++)
            {
                map += "?" + map;
                counts.AddRange(origCounts);
            }
            
            
            Console.WriteLine("After: ");
            Console.Write("Map: " + map + " Counts: ");
            for (int j = 0; j< counts.Count; j++)
            {
                Console.Write(counts[j] + ", ");
            }
            Console.Write("\n");
            */
            
            
            var permutations = new List<string>();
            permutations = PermuteMap(permutations, map, counts);
            Console.WriteLine("Possibilities: " + permutations.Count);
            allPosibilities += permutations.Count;
        }
        
        Console.WriteLine("Total: " + allPosibilities);
    }

    public static List<string> PermuteMap(List<string> list, string map)
    {
        var solved = true;
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == '?') solved = false;
        }

        if (solved)
        {
            list.Add(map);
            return list;
        }

        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == '?')
            {
                var a = map.ReplaceAt(i, '.');
                var b = map.ReplaceAt(i, '#');

                list = PermuteMap(list, a);
                list = PermuteMap(list, b);
                break;
            }
        }

        return list;
    }

    public static List<string> PermuteMap(List<string> list, string map, List<int> counts)
    {
        var solved = true;
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == '?') solved = false;
        }

        if (solved && MatchesCounts(map, counts))
        {
            list.Add(map);
            return list;
        }

        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == '?')
            {
                var a = map.ReplaceAt(i, '.');
                var b = map.ReplaceAt(i, '#');

                Console.WriteLine(a + " Could match? " + CouldMatchCount(a, counts));
                if (CouldMatchCount(a, counts))
                {
                    list = PermuteMap(list, a, counts);
                }

                Console.WriteLine(b + " Could match? " + CouldMatchCount(b, counts));
                if (CouldMatchCount(b, counts))
                {
                    list = PermuteMap(list, b, counts);
                }
                break;
            }
        }

        return list;
    }
    
    public static bool MatchesCounts(string map, List<int> counts)
    {
        List<int> hashes = new List<int>();
        int current = 0;
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == '#')
            {
                current++;
            }
            else
            {
                if (current > 0)
                {
                    hashes.Add(current);
                    current = 0;
                }
            }
        }

        if (current > 0) hashes.Add(current);


        if (hashes.Count != counts.Count) return false;

        for (int i = 0; i < hashes.Count; i++)
        {
            if (hashes[i] != counts[i]) return false;
        }

        return true;
    }

    public static bool CouldMatchCount(string map, List<int> counts)
    {
        int index = 0;
        
        List<int> chunks = new List<int>();
        int current = 0;
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] != '.')
            {
                current++;
            }
            else
            {
                if (current > 0)
                {
                    chunks.Add(current);
                    current = 0;
                }
            }
        }
        if (current > 0)
        {
            chunks.Add(current);
            current = 0;
        }
        
        for (int i = 0; i < counts.Count; i++)
        {
            var count = counts[i];
            
            // skip over any dots
            if (index >= map.Length) return false;
            while (map[index] == '.')
            {
                index++;
                if (index >= map.Length) return false;
            }

            // run through the count
            for (int j = 0; j < count; j++)
            {
                if (index >= map.Length) return false;
                if (map[index] == '.') return false;
                index++;
            }

            // if this isn't the last count, we'll need to account for the space 
            if (i != counts.Count - 1)
            {
                //account for the space
                if (index >= map.Length) return false;
                if (map[index] == '#') return false;
                index++;
            }
        }

        return true;
    }
}