public static class Day10
{
    public static void DoPartOne()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day10.txt");

        var grid = new List<List<Pipe>>();
        (int x, int y) animalLocation = (0, 0);

        // build grid of pipes
        for (int i = 0; i < lines.Length; i++)
        {
            grid.Add(new List<Pipe>());
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == 'S') animalLocation = (j, i);

                grid[i].Add(new Pipe(lines[i][j]));
                grid[i][j].Coords = (j, i);
            }
        }

        Console.WriteLine("Map Size: " + grid.Count + ", " + grid[0].Count);

        // build connections
        for (int y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].Count; x++)
            {
                grid[y][x].BuildConnections(grid, (x, y));
            }
        }

        // build distances
        grid[animalLocation.y][animalLocation.x].Distance = 0;
        Queue<Pipe> pipeQueue = new Queue<Pipe>();
        pipeQueue.Enqueue(grid[animalLocation.y][animalLocation.x]);

        while (pipeQueue.Count > 0)
        {
            var pipe = pipeQueue.Dequeue();

            if (pipe.North != null && pipe.North.South == pipe)
            {
                // if the connected pip hasn't been evaluated, add it to the stack
                if (pipe.North.Distance == -1)
                {
                    if (!pipeQueue.Contains(pipe.North)) pipeQueue.Enqueue(pipe.North);
                }
                // if it has, and it represents a shorte way to the beginning, set our distance appropriately
                else if (pipe.Distance == -1 || pipe.North.Distance + 1 < pipe.Distance)
                {
                    pipe.Distance = pipe.North.Distance + 1;
                }
            }

            if (pipe.South != null && pipe.South.North == pipe)
            {
                // if the connected pip hasn't been evaluated, add it to the stack
                if (pipe.South.Distance == -1)
                {
                    if (!pipeQueue.Contains(pipe.South)) pipeQueue.Enqueue(pipe.South);
                }
                // if it has, and it represents a shorte way to the beginning, set our distance appropriately
                else if (pipe.Distance == -1 || pipe.South.Distance + 1 < pipe.Distance)
                {
                    pipe.Distance = pipe.South.Distance + 1;
                }
            }

            if (pipe.East != null && pipe.East.West == pipe)
            {
                // if the connected pip hasn't been evaluated, add it to the stack
                if (pipe.East.Distance == -1)
                {
                    if (!pipeQueue.Contains(pipe.East)) pipeQueue.Enqueue(pipe.East);
                }
                // if it has, and it represents a shorte way to the beginning, set our distance appropriately
                else if (pipe.Distance == -1 || pipe.East.Distance + 1 < pipe.Distance)
                {
                    pipe.Distance = pipe.East.Distance + 1;
                }
            }

            if (pipe.West != null && pipe.West.East == pipe)
            {
                // if the connected pip hasn't been evaluated, add it to the stack
                if (pipe.West.Distance == -1)
                {
                    if (!pipeQueue.Contains(pipe.West)) pipeQueue.Enqueue(pipe.West);
                }
                // if it has, and it represents a shorte way to the beginning, set our distance appropriately
                else if (pipe.Distance == -1 || pipe.West.Distance + 1 < pipe.Distance)
                {
                    pipe.Distance = pipe.West.Distance + 1;
                }
            }
        }

        // find pipe that's definitely adjacent to outside tile
        var upperLeftPipe = animalLocation;
        for (int y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].Count; x++)
            {
                if (grid[y][x].PartOfLoop)
                {
                    upperLeftPipe = (x, y);
                    break;
                }
            }

            if (upperLeftPipe != animalLocation) break;
        }

        // reset distances
        for (int y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].Count; x++)
            {
                grid[y][x].Distance = -1;
            }
        }

        // build distances (again, this time oriented around pipe with known outside adjacency
        grid[upperLeftPipe.y][upperLeftPipe.x].Distance = 0;
        pipeQueue = new Queue<Pipe>();
        pipeQueue.Enqueue(grid[upperLeftPipe.y][upperLeftPipe.x]);
        while (pipeQueue.Count > 0)
        {
            var pipe = pipeQueue.Dequeue();

            if (pipe.North != null && pipe.North.South == pipe)
            {
                // if the connected pip hasn't been evaluated, add it to the stack
                if (pipe.North.Distance == -1)
                {
                    if (!pipeQueue.Contains(pipe.North)) pipeQueue.Enqueue(pipe.North);
                }
                // if it has, and it represents a shorte way to the beginning, set our distance appropriately
                else if (pipe.Distance == -1 || pipe.North.Distance + 1 < pipe.Distance)
                {
                    pipe.Distance = pipe.North.Distance + 1;
                }
            }

            if (pipe.South != null && pipe.South.North == pipe)
            {
                // if the connected pip hasn't been evaluated, add it to the stack
                if (pipe.South.Distance == -1)
                {
                    if (!pipeQueue.Contains(pipe.South)) pipeQueue.Enqueue(pipe.South);
                }
                // if it has, and it represents a shorte way to the beginning, set our distance appropriately
                else if (pipe.Distance == -1 || pipe.South.Distance + 1 < pipe.Distance)
                {
                    pipe.Distance = pipe.South.Distance + 1;
                }
            }

            if (pipe.East != null && pipe.East.West == pipe)
            {
                // if the connected pip hasn't been evaluated, add it to the stack
                if (pipe.East.Distance == -1)
                {
                    if (!pipeQueue.Contains(pipe.East)) pipeQueue.Enqueue(pipe.East);
                }
                // if it has, and it represents a shorte way to the beginning, set our distance appropriately
                else if (pipe.Distance == -1 || pipe.East.Distance + 1 < pipe.Distance)
                {
                    pipe.Distance = pipe.East.Distance + 1;
                }
            }

            if (pipe.West != null && pipe.West.East == pipe)
            {
                // if the connected pip hasn't been evaluated, add it to the stack
                if (pipe.West.Distance == -1)
                {
                    if (!pipeQueue.Contains(pipe.West)) pipeQueue.Enqueue(pipe.West);
                }
                // if it has, and it represents a shorte way to the beginning, set our distance appropriately
                else if (pipe.Distance == -1 || pipe.West.Distance + 1 < pipe.Distance)
                {
                    pipe.Distance = pipe.West.Distance + 1;
                }
            }
        }

        // HARDCODE: Replace animal with appropriate icon
        grid[animalLocation.y][animalLocation.x].Icon = 'L';


        // run out in a direction, adding Is where needed
        var previous = grid[upperLeftPipe.y][upperLeftPipe.x];
        var current = grid[upperLeftPipe.y][upperLeftPipe.x].East;

        while (current != grid[upperLeftPipe.y][upperLeftPipe.x])
        {
            var x = current.Coords.x;
            var y = current.Coords.y;

            var northPipe = y - 1 < grid.Count && y - 1 >= 0 ? grid[y - 1][x] : null;
            var southPipe = y + 1 < grid.Count && y + 1 >= 0 ? grid[y + 1][x] : null;
            var eastPipe = x + 1 < grid[y].Count && x + 1 >= 0 ? grid[y][x + 1] : null;
            var westPipe = x - 1 < grid[y].Count && x - 1 >= 0 ? grid[y][x - 1] : null;

            var next = current;

            if (current.Icon == '-')
            {
                // heading east, south is inside
                if (previous == current.West)
                {
                    if (!southPipe.PartOfLoop)
                    {
                        southPipe.InsideLoop = true;
                    }

                    next = eastPipe;
                }

                // heading west, north is inside
                if (previous == current.East)
                {
                    if (!northPipe.PartOfLoop)
                    {
                        northPipe.InsideLoop = true;
                    }

                    next = westPipe;
                }
            }

            if (current.Icon == '|')
            {
                // heading south, west is inside
                if (previous == current.North)
                {
                    if (!westPipe.PartOfLoop)
                    {
                        westPipe.InsideLoop = true;
                    }

                    next = southPipe;
                }

                // heading north, east is inside
                if (previous == current.South)
                {
                    if (!eastPipe.PartOfLoop)
                    {
                        eastPipe.InsideLoop = true;
                    }

                    next = northPipe;
                }
            }

            if (current.Icon == '7')
            {
                // heading west, east is insde
                if (previous == current.South)
                {
                    if (!eastPipe.PartOfLoop)
                    {
                        eastPipe.InsideLoop = true;
                    }

                    next = westPipe;
                }

                if (previous == current.West)
                {
                    next = southPipe;
                }
            }

            if (current.Icon == 'J')
            {
                // heading north, east is insde
                if (previous == current.West)
                {
                    if (!eastPipe.PartOfLoop)
                    {
                        eastPipe.InsideLoop = true;
                    }

                    next = northPipe;
                }

                if (previous == current.North)
                {
                    next = westPipe;
                }
            }

            if (current.Icon == 'L')
            {
                // heading East, west is insde
                if (previous == current.North)
                {
                    if (!westPipe.PartOfLoop)
                    {
                        westPipe.InsideLoop = true;
                    }

                    next = eastPipe;
                }

                if (previous == current.East)
                {
                    next = northPipe;
                }
            }

            if (current.Icon == 'F')
            {
                // heading south, west is insde
                if (previous == current.East)
                {
                    if (!westPipe.PartOfLoop)
                    {
                        westPipe.InsideLoop = true;
                    }

                    next = southPipe;
                }

                if (previous == current.South)
                {
                    next = eastPipe;
                }
            }

            previous = current;
            current = next;
        }

        // display grid
        for (int y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].Count; x++)
            {
                Console.Write(grid[y][x].PrettyIcon());
            }

            Console.Write("\n");
        }
        
        // flood fill edges
        Queue<(int x, int y)> insideConnections = new Queue<(int x, int y)>();
        for (int y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].Count; x++)
            {
                var pipe = grid[y][x];
                if (pipe.InsideLoop) insideConnections.Enqueue((x, y));
            }
        }

        while (insideConnections.Count > 0)
        {
            var pipeCoords = insideConnections.Dequeue();

            var pipe = grid[pipeCoords.y][pipeCoords.x];

            pipe.InsideLoop = true;

            var y = pipeCoords.y;
            var x = pipeCoords.x;

            var northPipe = y - 1 < grid.Count && y - 1 >= 0 ? grid[y - 1][x] : null;
            var southPipe = y + 1 < grid.Count && y + 1 >= 0 ? grid[y + 1][x] : null;
            var eastPipe = x + 1 < grid[y].Count && x + 1 >= 0 ? grid[y][x + 1] : null;
            var westPipe = x - 1 < grid[y].Count && x - 1 >= 0 ? grid[y][x - 1] : null;

            // if adjacent isn't null, isn't part of the loop, and hasn't been marked connected
            if (northPipe != null && !northPipe.PartOfLoop && !northPipe.InsideLoop)
            {
                insideConnections.Enqueue((x, y - 1));
            }

            if (southPipe != null && !southPipe.PartOfLoop && !southPipe.InsideLoop)
            {
                insideConnections.Enqueue((x, y + 1));
            }

            if (eastPipe != null && !eastPipe.PartOfLoop && !eastPipe.InsideLoop)
            {
                insideConnections.Enqueue((x + 1, y));
            }

            if (westPipe != null && !westPipe.PartOfLoop && !westPipe.InsideLoop)
            {
                insideConnections.Enqueue((x - 1, y - 1));
            }
        }

        int count = 0;
        // display grid
        for (int y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].Count; x++)
            {
                Console.Write(grid[y][x].PrettyIcon());

                if (grid[y][x].InsideLoop && !grid[y][x].PartOfLoop)
                {
                    count++;
                }
            }

            Console.Write("\n");
        }

        Console.WriteLine("Total Inside: " + count);
    }

    private class Pipe
    {
        public Pipe(char i)
        {
            Icon = i;
        }

        public void BuildConnections(List<List<Pipe>> grid, (int x, int y) coords)
        {
            // Ground
            if (Icon == '.') return;
            // Animal
            if (Icon == 'S') return;

            var x = coords.x;
            var y = coords.y;

            var northPipe = y - 1 < grid.Count && y - 1 >= 0 ? grid[y - 1][x] : null;
            var southPipe = y + 1 < grid.Count && y + 1 >= 0 ? grid[y + 1][x] : null;
            var eastPipe = x + 1 < grid[y].Count && x + 1 >= 0 ? grid[y][x + 1] : null;
            var westPipe = x - 1 < grid[y].Count && x - 1 >= 0 ? grid[y][x - 1] : null;

            if (northPipe != null && northPipe.Icon == '.') northPipe = null;
            if (southPipe != null && southPipe.Icon == '.') southPipe = null;
            if (eastPipe != null && eastPipe.Icon == '.') eastPipe = null;
            if (westPipe != null && westPipe.Icon == '.') westPipe = null;

            // vertical pipe
            if (Icon == '|')
            {
                North = northPipe;
                South = southPipe;

                if (North != null && North.Icon == 'S')
                {
                    North.South = this;
                }

                if (South != null && South.Icon == 'S')
                {
                    South.North = this;
                }
            }

            // horizontal pipe
            if (Icon == '-')
            {
                East = eastPipe;
                West = westPipe;

                if (East != null && East.Icon == 'S')
                {
                    East.West = this;
                }

                if (West != null && West.Icon == 'S')
                {
                    West.East = this;
                }
            }

            // NE Bend
            if (Icon == 'L')
            {
                North = northPipe;
                East = eastPipe;

                if (North != null && North.Icon == 'S')
                {
                    North.South = this;
                }

                if (East != null && East.Icon == 'S')
                {
                    East.West = this;
                }
            }

            // NW Bend
            if (Icon == 'J')
            {
                North = northPipe;
                West = westPipe;

                if (North != null && North.Icon == 'S')
                {
                    North.South = this;
                }

                if (West != null && West.Icon == 'S')
                {
                    West.East = this;
                }
            }

            // SW Bend
            if (Icon == '7')
            {
                West = westPipe;
                South = southPipe;

                if (West != null && West.Icon == 'S')
                {
                    West.East = this;
                }

                if (South != null && South.Icon == 'S')
                {
                    South.North = this;
                }
            }

            // SE Bend
            if (Icon == 'F')
            {
                East = eastPipe;
                South = southPipe;

                if (East != null && East.Icon == 'S')
                {
                    East.West = this;
                }

                if (South != null && South.Icon == 'S')
                {
                    South.North = this;
                }
            }
        }

        public int GetConnectionCount()
        {
            int count = 0;

            if (North != null) count++;
            if (South != null) count++;
            if (East != null) count++;
            if (West != null) count++;

            return count;
        }

        public char Icon = '.';
        public bool PartOfLoop => Distance != -1;
        public long Distance = -1;
        public Pipe North = null;
        public Pipe South = null;
        public Pipe East = null;
        public Pipe West = null;
        public (int x, int y) Coords;
        public bool InsideLoop = false;

        public char PrettyIcon()
        {
            if (PartOfLoop)
            {
                if (Icon == 'S') return 'S';
                if (Icon == '|') return '║';
                if (Icon == '-') return '═';
                if (Icon == '7') return '╗';
                if (Icon == 'J') return '╝';
                if (Icon == 'F') return '╔';
                if (Icon == 'L') return '╚';
                return '?';
            }

            if (InsideLoop)
            {
                return 'I';
            }


            return '.';
        }
    }
}