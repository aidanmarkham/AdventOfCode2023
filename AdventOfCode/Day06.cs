public static class Day06
{
    public static void DoPartOne()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\Aesthetician Labs\\AdventOfCode2023\\AdventOfCode\\Day06.txt");

        var races = new List<(int, int)>();

        var times = lines[0].Split(':')[1].Trim().Split(' ').ToList();
        for (int i = times.Count - 1; i >= 0; i--)
        {
            if (times[i] == "") times.RemoveAt(i);
        }

        var distances = lines[1].Split(':')[1].Trim().Split(' ').ToList();
        for (int i = distances.Count - 1; i >= 0; i--)
        {
            if (distances[i] == "") distances.RemoveAt(i);
        }

        races.Add((int.Parse(times[0]), int.Parse(distances[0])));
        races.Add((int.Parse(times[1]), int.Parse(distances[1])));
        races.Add((int.Parse(times[2]), int.Parse(distances[2])));
        races.Add((int.Parse(times[3]), int.Parse(distances[3])));

        for (int i = 0; i < races.Count; i++)
        {
            var time = races[i].Item1;
            var distance = races[i].Item2;

            var possibilities = 0;

            for (int t = 0; t < time; t++)
            {
                var travelDuration = time - t;
                var speed = t;
                var totalDistance = travelDuration * speed;

                if (totalDistance > distance)
                {
                    possibilities++;
                }
                //Console.WriteLine("If we hold for " + t + ", the boat will travel for " + (time - t ) + "ms at a speed of " +
                //                 t + "mm/ms, reaching a total distance of " + totalDistance);
            }

            Console.WriteLine("Possibilities: " + possibilities + "\n");
        }
    }

    public static void DoPartTwo()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\Aesthetician Labs\\AdventOfCode2023\\AdventOfCode\\Day06.txt");


        var time = long.Parse(new string(lines[0].Split(':')[1].Where(c => char.IsNumber(c)).ToArray()));
        var distance = long.Parse(new string(lines[1].Split(':')[1].Where(c => char.IsNumber(c)).ToArray()));

        var possibilities = 0;

        for (long t = 0; t < time; t++)
        {
            var travelDuration = time - t;
            var speed = t;
            var totalDistance = travelDuration * speed;

            if (totalDistance > distance)
            {
                possibilities++;
            }
            //Console.WriteLine("If we hold for " + t + ", the boat will travel for " + (time - t ) + "ms at a speed of " +
            //                 t + "mm/ms, reaching a total distance of " + totalDistance);
        }

        Console.WriteLine("Possibilities: " + possibilities + "\n");
    }
}
