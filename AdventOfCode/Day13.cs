using System.Diagnostics;

public static class Day13
{
    public static void DoPartOne()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day13.txt");
        var fields = new List<List<string>>();

        fields.Add(new List<string>());
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Length == 0)
            {
                fields.Add(new List<string>());
            }
            else
            {
                fields[fields.Count - 1].Add(lines[i]);
            }
        }

        int total = 0;
        foreach (var field in fields)
        {
            Console.WriteLine("Field: " + fields.IndexOf(field));
            var reflection = FindReflections(field)[0];
            var newReflection = reflection;
            Console.WriteLine(" - " + (reflection.isVertical ? "Vertical " : "Horizontal ") + "Reflection at: " +
                              reflection.index);


            for (int y = 0; y < field.Count; y++)
            {
                for (int x = 0; x < field[y].Length; x++)
                {
                    var newField = new List<string>(field);

                    if (newField[y][x] == '.')
                    {
                        newField[y] = newField[y].ReplaceAt(x, '#');
                    }
                    else
                    {
                        newField[y] = newField[y].ReplaceAt(x, '.');
                    }

                    var refs = FindReflections(newField);
                    if (refs.Contains(reflection)) refs.Remove(reflection);
                    for (int i = 0; i < refs.Count; i++)
                    {
                        Console.WriteLine(x + ", " + y + (refs[i].isVertical ? "Vertical " : "Horizontal ") + "Reflection at: " +
                                          refs[i].index);
                        newReflection = refs[i];
                        break;
                    }

                    if (newReflection != reflection) break;
                }
                
                if (newReflection != reflection) break;
            }

    
            if (newReflection.isVertical)
            {
                total += newReflection.index;
            }
            else
            {
                total += newReflection.index * 100;
            }
        }

        Console.WriteLine("Total: " + total);
    }

    public static List<(int index, bool isVertical)> FindReflections(List<string> field)
    {
        var reflections = new List<(int index, bool isVertical)>();

        // check for horizontal reflection
        for (int i = 0; i < field.Count - 1; i++)
        {
            int checkIndex = 0;
            bool matching = true;
            while (i - checkIndex >= 0 && i + 1 + checkIndex < field.Count)
            {
                if (field[i - checkIndex] != field[i + 1 + checkIndex])
                {
                    matching = false;
                    break;
                }

                checkIndex++;
            }

            if (matching)
            {
                reflections.Add((i + 1, false));
            }
        }

        // check for Vertical reflection
        for (int i = 0; i < field[0].Length - 1; i++)
        {
            int checkIndex = 0;
            bool matching = true;
            while (i - checkIndex >= 0 && i + 1 + checkIndex < field[0].Length)
            {
                for (int j = 0; j < field.Count; j++)
                {
                    if (field[j][i - checkIndex] != field[j][i + 1 + checkIndex])
                    {
                        matching = false;
                        break;
                    }
                }


                checkIndex++;
            }

            if (matching)
            {
                reflections.Add((i + 1, true));
            }
        }

        return reflections;
    }
}