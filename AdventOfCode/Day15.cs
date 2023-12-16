using System.Net;
using System.Reflection.Emit;
using Microsoft.VisualBasic;

public static class Day15
{
    public static void DoPartOne()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day15.txt");

        var strings = lines[0].Split(',');

        int total = 0;
        foreach (var str in strings)
        {
            int currentValue = 0;

            for (int i = 0; i < str.Length; i++)
            {
                char current = str[i];

                currentValue += (int)current;
                currentValue *= 17;
                currentValue = currentValue % 256;
            }

            Console.WriteLine(str + ": " + currentValue);
            total += currentValue;
        }

        Console.WriteLine("Total: " + total);
    }

    public static void DoPartTwo()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day15.txt");

        var strings = lines[0].Split(',');

        var boxes = new List<List<(string label, int focalLength)>>();

        for (int i = 0; i < 256; i++)
        {
            boxes.Add(new List<(string label, int focalLength)>());
        }

        foreach (var str in strings)
        {
            var label = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsLetter(str[i]))
                {
                    label += str[i];
                }
                else
                {
                    break;
                }
            }
            
            var box = Hashmap(label);
            var operation = str[label.Length];

            if (operation == '=')
            {
                var fl = int.Parse("" + str[label.Length+1]);
                bool replaced = false;
                for (int i = 0; i < boxes[box].Count; i++)
                {
                    if (boxes[box][i].label == label)
                    {
                        var l = boxes[box][i];
                        l.focalLength = fl;
                        boxes[box][i] = l;
                        replaced = true;
                        break;
                    }
                }

                if (!replaced) boxes[box].Add((label, fl));
            }
            else
            {
                for (int i = 0; i < boxes[box].Count; i++)
                {
                    if (boxes[box][i].label == label)
                    {
                        boxes[box].RemoveAt(i);
                        break;
                    }
                }
            }

            /*
            Console.WriteLine("\nAfter '" + str + "':");
            for (int i = 0; i < boxes.Count; i++)
            {
                if (boxes[i].Count > 0)
                {
                    Console.Write("Box " + i + ": ");

                    for (int j = 0; j < boxes[i].Count; j++)
                    {
                        Console.Write("[" + boxes[i][j].label + " " + boxes[i][j].focalLength + "]");
                    }

                    Console.WriteLine();
                }
            }
            */
        }

        var totalPower = 0;

        for (int i = 0; i < boxes.Count; i++)
        {
            for (int j = 0; j < boxes[i].Count; j++)
            {
                totalPower += (1 + i) * (1 + j) * boxes[i][j].focalLength;
            }
        }
        
        Console.WriteLine("Total Power: " + totalPower);
    }

    public static int Hashmap(string str)
    {
        int currentValue = 0;
        for (int i = 0; i < str.Length; i++)
        {
            char current = str[i];

            currentValue += (int)current;
            currentValue *= 17;
            currentValue = currentValue % 256;
        }

        return currentValue;
    }
}