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
        foreach( var node in nodes)
        {
            if (node.Key[2] == 'A')
            {
                currentNodes.Add(node.Key);
            } 
        }
        var turnIndex = 0;
        var step = 0;
        bool done = false;
        while (!done)
        {
            Console.WriteLine("Step: " + step);
            if (turnIndex == turns.Length) turnIndex = 0;
            
            var dir = turns[turnIndex];
            for (int i = 0; i < currentNodes.Count; i++)
            {
                //Console.WriteLine("Step: " + step + " Current Node: " + currentNodes[i]);
                
                if (dir == 'L')
                {
                    currentNodes[i] = nodes[currentNodes[i]].Item1;
                }
                else
                {
                    currentNodes[i] = nodes[currentNodes[i]].Item2;
                }
            }

            turnIndex++;
            step++;

            // see if we're done
            var allZ = true;
            for (int i = 0; i < currentNodes.Count; i++)
            {
                if (currentNodes[i][2] != 'Z')
                {
                    allZ = false;
                }
            }
            done = allZ;
        }

        Console.WriteLine("Steps: " + step);
    }
}
