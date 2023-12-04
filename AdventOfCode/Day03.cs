using System.Diagnostics;
using System.Threading.Tasks.Dataflow;

public static class Day03
{
    public static void Do()
    {
        var lines = File.ReadAllLines("C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day03.txt");

        List<List<char>> grid = new List<List<char>>();
        List<(int, int, int)> numberPositions = new List<(int, int, int)>();
        List<(int, int)> gearPositions = new List<(int, int)>();
        for (int y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            grid.Add(new List<char>());
            for (int x = 0; x < line.Length; x++)
            {
                char cell = line[x];
                if (cell == '.') cell = ' ';
                if(cell == '*') gearPositions.Add((x,y));
                if (char.IsSymbol(cell) || char.IsPunctuation(cell)) cell = '$';


                grid[y].Add(cell);
            }
        }

        for (int y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].Count; x++)
            {
                var cell = grid[y][x];
                Console.Write(cell);
                //Console.Write(Adjacent(x, y, 3, 4) ? "t":"f");
                // we're at the beginning of a number
                if (char.IsNumber(cell) && (x == 0 || !char.IsNumber(grid[y][x - 1])))
                {
                    var endx = x;
                    while (endx < grid[y].Count && char.IsNumber(grid[y][endx]))
                    {
                        endx++;
                    }

                    numberPositions.Add((x, y, endx - x));
                }
            }

            Console.Write("\n");
        }

        var total = 0;
        for (int i = 0; i < numberPositions.Count; i++)
        {
            bool numberHasAdjacentSymbol = false;
            string num = "";
            for (int z = 0; z < numberPositions[i].Item3; z++)
            {
                num += grid[numberPositions[i].Item2][numberPositions[i].Item1 + z];
                
                if (HasAdjacentSymbol(numberPositions[i].Item1 + z, numberPositions[i].Item2, grid))
                {
                    numberHasAdjacentSymbol = true;
                }
            }
            
            if (numberHasAdjacentSymbol) total += int.Parse(num);
        }

        Console.WriteLine("Sum: " + total);


        var totalRatio = 0;
        for (int i = 0; i < gearPositions.Count; i++)
        {
            List<int> adjacentNumbers = new List<int>();
            for (int n = 0; n < numberPositions.Count; n++)
            {
                for (int x = 0; x < numberPositions[n].Item3; x++)
                {
                    if (Adjacent(gearPositions[i].Item1, gearPositions[i].Item2, numberPositions[n].Item1 + x,
                            numberPositions[n].Item2))
                    {
                        if (!adjacentNumbers.Contains(n))
                        {
                            adjacentNumbers.Add(n);
                        }
                    }
                }
            }
            Console.WriteLine("Gear " + i + ": Position (" + gearPositions[i].Item1 + ", " + gearPositions[i].Item2 + ") Adjacent Numbers:");

            var ratio = 0;
            for (int n = 0; n < adjacentNumbers.Count; n++)
            {
                var number = GetNumber(numberPositions[adjacentNumbers[n]].Item1,
                    numberPositions[adjacentNumbers[n]].Item2,
                    numberPositions[adjacentNumbers[n]].Item3,
                    grid);
                Console.WriteLine("    - " + number + " pos: " +  numberPositions[adjacentNumbers[n]].Item1 + ", " + numberPositions[adjacentNumbers[n]].Item2);

                if (adjacentNumbers.Count == 2)
                {
                    if (n == 0)
                    {
                        ratio = number;
                    }
                    else
                    {
                        ratio *= number;
                    }
                }
            }
            if (adjacentNumbers.Count == 2)
            {
                Console.WriteLine("    - Gear Ratio: " + ratio);
                totalRatio += ratio;
            }

            
        }
        Console.WriteLine("Total Ratio: " + totalRatio);
    }

    public static bool HasAdjacentSymbol(int x, int y, List<List<char>> grid)
    {
        var hasSymbol = false;
        
        // left
        if(x - 1 >= 0 && char.IsSymbol(grid[y][x-1])) hasSymbol = true;
        // right
        if(x + 1 < grid[y].Count && char.IsSymbol(grid[y][x+1])) hasSymbol = true;
        
        // up
        if(y - 1 >= 0 && char.IsSymbol(grid[y-1][x])) hasSymbol = true;
        // down
        if(y + 1 < grid.Count && char.IsSymbol(grid[y+1][x])) hasSymbol = true;
        
        // up left
        if(x - 1 >= 0 && y - 1 >= 0 && char.IsSymbol(grid[y-1][x-1])) hasSymbol = true;
        // up right
        if(x + 1 < grid[y].Count && y - 1 >= 0 && char.IsSymbol(grid[y-1][x+1])) hasSymbol = true;
        // down left
        if(x - 1 >= 0 && y + 1 < grid.Count && char.IsSymbol(grid[y+1][x-1])) hasSymbol = true;
        // down right
        if(x + 1 < grid[y].Count && y + 1 < grid.Count && char.IsSymbol(grid[y+1][x+1])) hasSymbol = true;
        
        
        return hasSymbol;
    }

    public static bool Adjacent(int x1, int y1, int x2, int y2)
    {
        var adjacent = false;
        if (Math.Abs(x1 - x2) <= 1 && Math.Abs(y1 - y2) <= 1) adjacent = true;
        return adjacent;
    }

    public static int GetNumber(int x, int y, int length, List<List<char>> grid)
    {
        string num = "";
        for (int z = 0; z < length; z++)
        {
            num += grid[y][x + z];
        }
        return int.Parse(num);
    }
}