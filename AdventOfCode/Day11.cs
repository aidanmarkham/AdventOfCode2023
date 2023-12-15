using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

public static class Day11
{
    public static void DoPartOne()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day11.txt").ToList();
        
        // print input
        Console.WriteLine("Input: ");
        for (int i = 0; i < lines.Count; i++)
        {
            Console.WriteLine(lines[i]);
        }
        
        // expand rows
        for (int i = 0; i < lines.Count; i++)
        {
            var allSpace = true;

            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] != '.') allSpace = false;
            }

            if (allSpace)
            {
                lines.Insert(i, lines[i]);
                i++;
            }
        }

        // expand columns
        for (int j = 0; j < lines[0].Length; j++)
        {
            var allSpace = true;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i][j] != '.') allSpace = false;
            }
            if (allSpace)
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    lines[i] = lines[i].Insert(j, ".");
                }

                j++;
            }
        }
        

        Console.WriteLine("\nExpanded: ");
        for (int i = 0; i < lines.Count; i++)
        {
            Console.WriteLine(lines[i]);
        }

        List<(int x, int y)> galaxies = new List<(int x, int y)>();
        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == '#')
                {
                    galaxies.Add((j, i));
                }
            }
        }

        long totalDistance = 0;
        for (int a = 0; a < galaxies.Count; a++)
        {
            for (int b = 0; b < galaxies.Count; b++)
            {
                totalDistance += ManhattanDistance(galaxies[a], galaxies[b]);
            }
        }
        Console.WriteLine("Total Distance: " + (totalDistance/2));
    }

    public static void DoPartTwo()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day11.txt").ToList();
        
        // print input
        Console.WriteLine("Input: ");
        for (int i = 0; i < lines.Count; i++)
        {
            Console.WriteLine(lines[i]);
        }
        
        // replace rows with spaces
        for (int i = 0; i < lines.Count; i++)
        {
            var allSpace = true;

            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == '#') allSpace = false;
            }

            if (allSpace)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    lines[i] = lines[i].ReplaceAt(j, ' ');
                }
            }
        }

        // expand replace column with spaces
        for (int j = 0; j < lines[0].Length; j++)
        {
            var allSpace = true;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i][j] == '#') allSpace = false;
            }
            if (allSpace)
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    lines[i] = lines[i].ReplaceAt(j, ' ');
                }
            }
        }
        

        Console.WriteLine("\nExpanded: ");
        for (int i = 0; i < lines.Count; i++)
        {
            Console.WriteLine(lines[i]);
        }

        List<(long x, long y)> galaxies = new List<(long x, long y)>();
        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == '#')
                {
                    galaxies.Add((j, i));
                }
            }
        }

        for (int g = 0; g < galaxies.Count; g++)
        {
            var galaxy = galaxies[g];
            var adjustedPos = galaxy;
            
            
            for (int y = 0; y < galaxy.y; y++)
            {
                if (lines[y][(int)galaxy.x] == ' ') adjustedPos.y += 999999;
            }
            for (int x = 0; x < galaxy.x; x++)
            {
                if (lines[(int)galaxy.y][x] == ' ') adjustedPos.x += 999999;
            }
            
            Console.WriteLine("Before adjustment: " + galaxy + " After: " + adjustedPos);
            galaxies[g] = adjustedPos;
        }
        
        long totalDistance = 0;
        for (int a = 0; a < galaxies.Count; a++)
        {
            for (int b = 0; b < galaxies.Count; b++)
            {
                totalDistance += ManhattanDistance(galaxies[a], galaxies[b]);
            }
        }
        Console.WriteLine("Total Distance: " + (totalDistance/2));
    }

    
    public static long ManhattanDistance((long x, long y) a, (long x, long y) b)
    {
        return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }
    
    public static string ReplaceAt(this string input, int index, char newChar)
    {
        if (input == null)
        {
            throw new ArgumentNullException("input");
        }
        char[] chars = input.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
    }
}