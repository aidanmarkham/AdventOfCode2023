using System.Diagnostics;

class Day01
{
    public void Do()
    {
// read in text as lines
        var lines = File.ReadAllLines("C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day01.txt");

        var total = 0;

// define all valid numbers as strings
        var nums = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

// go through each line
        for (int i = 0; i < lines.Length; i++)
        {
            // get the current line
            var line = lines[i];

            // if the current line contains a number
            var containedNum = true;
            while (containedNum)
            {
                containedNum = false;
                Dictionary<string, int> indices = new Dictionary<string, int>();
                // find indices of all contained numbers
                for (int n = 0; n < nums.Count; n++)
                {
                    // set the index if it occurs in this line
                    if (line.Contains(nums[n]))
                    {
                        containedNum = true;
                        indices.Add(nums[n], line.IndexOf(nums[n]));
                    }
                }

                // sort that list
                var sortedNums = nums.ToList().OrderBy(x => indices.ContainsKey(x) ? indices[x] : int.MaxValue)
                    .ToList();

                if (sortedNums.Count == 0) continue;
                if (!indices.ContainsKey(sortedNums[0])) continue;

                var index = indices[sortedNums[0]];
                if (index == int.MaxValue) continue;

                line = line.Substring(0, index) +
                       (nums.IndexOf(sortedNums[0]) + 1).ToString() +
                       line.Substring(index + sortedNums[0].Length);
            }

            // strip remaining characters
            line = new string(line.Where(c => char.IsNumber(c)).ToArray());

            var firstNumber = line[0];

            var frontParsed = line;

            line = lines[i];

            containedNum = true;
            while (containedNum)
            {
                containedNum = false;
                Dictionary<string, int> indices = new Dictionary<string, int>();
                // find indices of all contained numbers
                for (int n = 0; n < nums.Count; n++)
                {
                    // set the index if it occurs in this line
                    if (line.Contains(nums[n]))
                    {
                        containedNum = true;
                        indices.Add(nums[n], line.LastIndexOf(nums[n]));
                    }
                }

                // sort that list
                var sortedNums = nums.ToList().OrderBy(x => indices.ContainsKey(x) ? -indices[x] : int.MaxValue)
                    .ToList();

                if (sortedNums.Count == 0) continue;
                if (!indices.ContainsKey(sortedNums[0])) continue;

                var index = indices[sortedNums[0]];
                if (index == int.MaxValue) continue;

                line = line.Substring(0, index) +
                       (nums.IndexOf(sortedNums[0]) + 1).ToString() +
                       line.Substring(index + sortedNums[0].Length);
            }

            // strip remaining characters
            line = new string(line.Where(c => char.IsDigit(c)).ToArray());

            var secondNumber = line[^1];
            var number = "" + firstNumber + secondNumber;

            total += int.Parse(number);

            Console.WriteLine((i + 1) + " Original: " + lines[i] + " Front Parsed: " + frontParsed + " Back Parsed: " +
                              line + " Sum: " +
                              number + " Total: " + total);
        }

        Console.WriteLine("Total: " + total);
    }
}