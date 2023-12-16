using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
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
            allPosibilities += possibilities;
        }
        
        Console.WriteLine("Total: " + allPosibilities);
    }
    public static void DoPartOneOpt()
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
            
            var permutations = new List<string>();
            permutations = PermuteMap(permutations, map, counts);
            allPosibilities += permutations.Count;
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
            // parse data
            var line = lines[i];
            var map = line.Split(' ')[0];
            var countStrings = line.Split(' ')[1].Split(',');
            var counts = new List<int>();
            for (int j = 0; j < countStrings.Length; j++)
            {
                counts.Add(int.Parse(countStrings[j]));
            }
            var countString = line.Split(' ')[1];
            
            
            // apply multiplication to data
            var origMap = map;
            var origCounts = countString;
            var origCountsList = new List<int>(counts);
            for (int j = 0; j < 4; j++)
            {
                map += "?" + origMap;
                countString += "," + origCounts;
                counts.AddRange(origCountsList);
            }
            
            
            // set up stack to iterate
            Stack<(string map, string work, string ranges)> perms = new Stack<(string map, string work, string ranges)>();
            perms.Push((map, map, countString));

            var possibles = new List<string>();
            while (perms.Count > 0)
            {
                var perm = perms.Pop();
                if (perm.ranges.Length == 0)
                {
                    if (MatchesCounts(perm.map, counts) && !possibles.Contains(perm.map))
                    {
                        possibles.Add(perm.map);
                    }
                    continue;
                }

                //Console.WriteLine(perms.Count + " Map : " + perm.map + " work: " + perm.work + " range: " + perm.ranges + " ");
                
                // pull the largest count out of the list of already placed counts
                var localPlacedCounts = perm.ranges.Split(',').ToList();
                int largest = -1;
                for (int j = 0; j < localPlacedCounts.Count; j++)
                {
                    if (int.Parse(localPlacedCounts[j]) > largest)
                    {
                        largest = int.Parse(localPlacedCounts[j]);
                    }
                }
                localPlacedCounts.Remove(largest.ToString());
                var newRanges = "";
                for (int j = 0; j < localPlacedCounts.Count; j++)
                {
                    newRanges += localPlacedCounts[j];
                    if (j < localPlacedCounts.Count - 1)
                    {
                        newRanges += ",";
                    }
                }
                
                // slide through this map, recursing anywhere the largest map could be placed
                for (int c = 0; c < perm.work.Length - largest + 1; c++)
                {
                    if (!perm.work.Substring(c, largest).Contains('.') && // this range doesn't contain any .
                        (c+largest == perm.work.Length || perm.map[c+largest] != '#')) // after this range is either the end, or at least isn't a #
                    {
                        var newMap = perm.map;
                        var newWork = perm.work;
                        for (int j = 0; j < largest; j++)
                        {
                            newMap = newMap.ReplaceAt(c + j, '#');
                            newWork = newWork.ReplaceAt(c + j, '.');
                        }

                        if (c + largest < perm.work.Length)
                        {
                            newMap = newMap.ReplaceAt(c + largest, '.');
                            newWork = newWork.ReplaceAt(c + largest, '.');
                        }
                
                        if (CouldMatchCount(newMap, counts) &&
                            !perms.Contains((newMap, newWork, newRanges)))
                        {
                           // Console.WriteLine("Permuting on possibility after placing " + largest + ": " + newMap);
                            perms.Push((newMap, newWork, newRanges));
                        }
                    }
                }
            }
            
            Console.WriteLine("Possibilities: " + possibles.Count);
            allPosibilities += possibles.Count;
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
        Console.WriteLine(map + " ");
        var solved = true;
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == '?') solved = false;
        }

        if (solved && MatchesCounts(map, counts))
        {
            //Console.WriteLine(map);
            list.Add(map);
            return list;
        }

        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == '?')
            {
                var a = map.ReplaceAt(i, '.');
                var b = map.ReplaceAt(i, '#');
                
                if (CouldMatchCount(a, counts))
                {
                    list = PermuteMap(list, a, counts);
                }
                
                if (CouldMatchCount(b, counts))
                {
                    list = PermuteMap(list, b, counts);
                }
                
                break;
            }
        }

        return list;
    }
    
    public static List<string> PermuteMap(List<string> list, string map, string work, List<int> counts, List<int> placedCounts)
    {
        // save if we're done
        if (placedCounts.Count == 0)
        {
            if (MatchesCounts(map, counts) && !list.Contains(map))
            {
                //Console.WriteLine("Adding to possibilities: " + map);
                list.Add(map);
            }
            return list;
        }

        // pull the largest count out of the list of already placed counts
        var localPlacedCounts = new List<int>(placedCounts);
        int largest = -1;
        for (int i = 0; i < localPlacedCounts.Count; i++)
        {
            if (localPlacedCounts[i] > largest)
            {
                largest = localPlacedCounts[i];
            }
        }
        localPlacedCounts.Remove(largest);

        // slide through this map, recursing anywhere the largest map could be placed
        for (int i = 0; i < work.Length - largest + 1; i++)
        {
            if (!work.Substring(i, largest).Contains('.') && // this range doesn't contain any .
                (i+largest == work.Length || map[i+largest] != '#')) // after this range is either the end, or at least isn't a #
            {
                var newMap = map;
                var newWork = work;
                for (int j = 0; j < largest; j++)
                {
                    newMap = newMap.ReplaceAt(i + j, '#');
                    newWork = newWork.ReplaceAt(i + j, '.');
                }

                if (i + largest < work.Length)
                {
                    newMap = newMap.ReplaceAt(i + largest, '.');
                    newWork = newWork.ReplaceAt(i + largest, '.');
                }
                
                if (CouldMatchCount(newMap, counts))
                {
                    Console.WriteLine("Permuting on possibility after placing " + largest + ": " + newMap);
                    list = PermuteMap(list, newMap, newWork, counts, localPlacedCounts);
                }
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
        // build list of chunks
        List<string> chunks = new List<string>();
        string current = "";
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] != '.')
            {
                current += map[i];
            }
            else
            {
                if (current.Length > 0)
                {
                    chunks.Add(current);
                    current = "";
                }
            }
        }
        if (current.Length > 0)
        {
            chunks.Add(current);
        }
        
        for (int i = 0; i < counts.Count; i++)
        {
            bool matched = false;
            
            for (int j = 0; j < chunks.Count; j++)
            {
                int len = chunks[j].Length;  
                if (len == 0) continue;
                
                // it's longer than it needs to be but might still be ok with ?s
                if (len > counts[i] && chunks[j].Contains('?'))
                {
                    for (int start = 0; start < len - counts[i] + 1; start++)
                    {
                        if (start + counts[i] == len)
                        {
                            chunks[j] = "";
                            matched = true;
                            break;
                        }
                        if (chunks[j][start + counts[i]] == '?')
                        {
                            chunks[j] = chunks[j].Remove(start, counts[i]+1);
                            matched = true;
                            break;
                        }
                    }
                }
                // a perfect chunk!
                else if (len == counts[i])
                {
                    chunks[j] = "";
                    matched = true;
                    break;
                }
                // it's too long 
                else if (len > counts[i])
                {
                    chunks[j] = "";
                }
                // it's too short 
                else
                {
                    chunks[j] = "";
                }

                if (matched) break;
            }
            
            if (!matched) return false;
        }

        return true;
    }
}