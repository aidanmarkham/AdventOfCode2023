public static class Day09
{
    public static void DoPartOne()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day09.txt");

        var sum = 0;
        
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            var deltas = new List<List<int>>();
            
            var numbers = line.Split(' ');
            
            // populate initial line
            deltas.Add(new List<int>());
            for (int j = 0; j < numbers.Length; j++)
            {
                deltas[0].Add(int.Parse(numbers[j]));
            }

            // create sub levels 
            bool done = false;
            int level = 0;
            while (!done)
            {
                if(level + 1 == deltas.Count) deltas.Add(new List<int>());

                bool allZeros = true;
                for (int j = 1; j < deltas[level].Count; j++)
                {
                    var diff = deltas[level][j] - deltas[level][j - 1];
                    deltas[level+1].Add(diff);

                    if (diff != 0) allZeros = false;
                }

                level++;
                if (allZeros) done = true;
            }

            // stage one complete, now to calculate the next digit
            deltas[deltas.Count - 1].Add(0);
            for (int j = deltas.Count - 2; j >= 0; j--)
            {
                // the last element of the next level down
                var diff = deltas[j + 1][deltas[j + 1].Count - 1];
                
                // add that to this element
                deltas[j].Add(deltas[j][deltas[j].Count - 1] + diff);
            }

            var next = deltas[0][deltas[0].Count - 1];
            Console.WriteLine("Next Value: " + next);
            sum += next;
        }
        
        Console.WriteLine("Sum: " + sum);
    }
    
    public static void DoPartTwo()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day09.txt");

        var sum = 0;
        
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            var deltas = new List<List<int>>();
            
            var numbers = line.Split(' ');
            
            // populate initial line
            deltas.Add(new List<int>());
            for (int j = 0; j < numbers.Length; j++)
            {
                deltas[0].Add(int.Parse(numbers[j]));
            }

            // create sub levels 
            bool done = false;
            int level = 0;
            while (!done)
            {
                if(level + 1 == deltas.Count) deltas.Add(new List<int>());

                bool allZeros = true;
                for (int j = 1; j < deltas[level].Count; j++)
                {
                    var diff = deltas[level][j] - deltas[level][j - 1];
                    deltas[level+1].Add(diff);

                    if (diff != 0) allZeros = false;
                }

                level++;
                if (allZeros) done = true;
            }

            // stage one complete, now to calculate the next digit
            deltas[deltas.Count - 1].Add(0);
            for (int j = deltas.Count - 2; j >= 0; j--)
            {
                // the last element of the next level down
                var diff = deltas[j + 1][0];
                
                // add that to this element
                deltas[j].Insert(0, deltas[j][0] - diff);
            }

            var next = deltas[0][0];
            Console.WriteLine("Previous Value: " + next);
            sum += next;
        }
        
        Console.WriteLine("Sum: " + sum);
    }
}