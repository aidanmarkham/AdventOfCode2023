public static class Day08
{
    public static void DoPartOne()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day08.txt");

        var turns = lines[0];
        var nodes = new Dictionary<string, (string, string)>();
        for (int i = 2; i < lines.Length; i++)
        {
            var line = lines[i];
            var node = line.Split('=')[0].Trim();
            var left = line.Split('=')[1].Trim().Split(',')[0].Substring(1);
            var right = line.Split('=')[1].Trim().Split(',')[1].Substring(1, 3);

            //Console.WriteLine("Node: " + node + " Left: " + left + " Right: " + right);

            nodes.Add(node, (left, right));
        }

        var currentNode = "AAA";
        var turnIndex = 0;
        var step = 0;
        while (currentNode != "ZZZ")
        {
            if (turnIndex == turns.Length) turnIndex = 0;

            var dir = turns[turnIndex];

            Console.WriteLine("Current Node: " + currentNode + " Turning: " + dir);
            if (dir == 'L')
            {
                currentNode = nodes[currentNode].Item1;
            }
            else
            {
                currentNode = nodes[currentNode].Item2;
            }

            turnIndex++;
            step++;
        }

        Console.WriteLine("Steps: " + step);
    }


    public static void DoPartTwo()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day08.txt");

        var turns = lines[0];
        var nodes = new Dictionary<string, (string, string)>();
        for (int i = 2; i < lines.Length; i++)
        {
            var line = lines[i];
            var node = line.Split('=')[0].Trim();
            var left = line.Split('=')[1].Trim().Split(',')[0].Substring(1);
            var right = line.Split('=')[1].Trim().Split(',')[1].Substring(1, 3);

            //Console.WriteLine("Node: " + node + " Left: " + left + " Right: " + right);

            nodes.Add(node, (left, right));
        }

        var currentNodes = new List<string>();
        foreach (var node in nodes)
        {
            if (node.Key[2] == 'A')
            {
                currentNodes.Add(node.Key);
            }
        }

        List<long> steps = new List<long>();
        for (int i = 0; i < currentNodes.Count; i++)
        {
            var currentNode = currentNodes[i];
            var turnIndex = 0;
            long step = 0;
            while (currentNode[2] != 'Z')
            {
                if (turnIndex == turns.Length) turnIndex = 0;

                var dir = turns[turnIndex];
                
                if (dir == 'L')
                {
                    currentNode = nodes[currentNode].Item1;
                }
                else
                {
                    currentNode = nodes[currentNode].Item2;
                }

                turnIndex++;
                step++;
            }

            steps.Add(step);
        }
        
        Console.WriteLine("All Steps: ");
        for (int i = 0; i < steps.Count; i++)
        {
            Console.WriteLine(" - Step: " + steps[i]);
        }
        Console.WriteLine("Least Common Multiple: " + LCM(steps.ToArray()));
    }
    
    static long LCM(long[] numbers)
    {
        return numbers.Aggregate(lcm);
    }
    static long lcm(long a, long b)
    {
        return Math.Abs(a * b) / GCD(a, b);
    }
    static long GCD(long a, long b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }
}
