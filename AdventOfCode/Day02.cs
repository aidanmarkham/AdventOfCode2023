static class Day02
{
    public static void Do()
    {
        var lines = File.ReadAllLines("C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day02.txt");

        var games = new List<Game>();
        
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            var game = new Game();
            
            // pull out game number
            game.number = int.Parse(new string(line.Split(':')[0].Where(c => char.IsNumber(c)).ToArray()));
            
            // clip game number from the line
            line = line.Split(':')[1];
            
            var pulls = line.Split(';');
            foreach (var pull in pulls)
            {
                var cubes = pull.Split(',');
                foreach (var cube in cubes)
                {
                    var count = int.Parse(new string(cube.Where(c => char.IsNumber(c)).ToArray()));
                    var color = new string(cube.Where(c => !char.IsNumber(c)).Where(c => !char.IsWhiteSpace(c)).ToArray());
                    if (game.cubes.ContainsKey(color))
                    {
                        game.cubes[color] = Math.Max(game.cubes[color], count);
                    }
                    else
                    {
                        game.cubes.Add(color, count);
                    }
                }
            }
            
            games.Add(game);
        }

        var total = 0;
        
        for (int i = 0; i < games.Count; i++)
        {
            var game = games[i];
            
            Console.Write("\nGame " + game.number + ": ");
            foreach (var cube in game.cubes)
            {
                Console.Write(cube.Value + " " + cube.Key + ", ");
            }

            var power = game.cubes["red"] * game.cubes["blue"] * game.cubes["green"];
            
            Console.Write(" Power: " + power);

            total += power;
        }
        
        Console.WriteLine("\nTotal: " + total);
    }

    public class Game
    {
        public int number;
        public Dictionary<string, int> cubes = new Dictionary<string, int>();
    }
}