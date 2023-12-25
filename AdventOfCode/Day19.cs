public static class Day19
{
    public static void DoPartOne()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day19.txt");

        var workflows = new Dictionary<string, List<string>>();
        var parts = new List<(int x, int m, int a, int s)>();
        bool scanningWorkflows = true;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Trim().Length == 0)
            {
                scanningWorkflows = false;
                continue;
            }

            if (scanningWorkflows)
            {
                var label = lines[i].Split('{')[0];
                var rules = lines[i]
                    .Split('{')[1]
                    .Split('}')[0]
                    .Split(',').ToList();

                workflows.Add(label, rules);
            }
            else
            {
                // remove braces
                var part = lines[i].Substring(1)
                    .Split('}')[0];
                int x = int.Parse(part.Split(',')[0].Substring(2));
                int m = int.Parse(part.Split(',')[1].Substring(2));
                int a = int.Parse(part.Split(',')[2].Substring(2));
                int s = int.Parse(part.Split(',')[3].Substring(2));

                //Console.WriteLine("X: " + x + " M: " + m + " A: " + a + " S: " + s);
                parts.Add((x, m, a, s));
            }
        }

        int accepted = 0;
        foreach (var part in parts)
        {
            Console.WriteLine(part.x + " " + part.m + " " + part.a + " " + part.s);

            var currentWorkflow = "in";
            while (currentWorkflow != "A" && currentWorkflow != "R")
            {
                Console.Write(currentWorkflow + ", ");
                for (int i = 0; i < workflows[currentWorkflow].Count; i++)
                {
                    var rule = workflows[currentWorkflow][i];

                    // special case for the last rule
                    if (i == workflows[currentWorkflow].Count - 1)
                    {
                        currentWorkflow = rule;
                        break;
                    }

                    // first character of rule string = relevant property
                    var property = rule[0];

                    // second character = > or <
                    var comparator = rule[1];

                    // get the number associated with the rule
                    var value = int.Parse(rule.Split(':')[0].Substring(2));

                    var destination = rule.Split(':')[1];

                    if (comparator == '<')
                    {
                        if (property == 'x' && part.x < value)
                        {
                            currentWorkflow = destination;
                            break;
                        }

                        if (property == 'm' && part.m < value)
                        {
                            currentWorkflow = destination;
                            break;
                        }

                        if (property == 'a' && part.a < value)
                        {
                            currentWorkflow = destination;
                            break;
                        }

                        if (property == 's' && part.s < value)
                        {
                            currentWorkflow = destination;
                            break;
                        }
                    }
                    else
                    {
                        if (property == 'x' && part.x > value)
                        {
                            currentWorkflow = destination;
                            break;
                        }

                        if (property == 'm' && part.m > value)
                        {
                            currentWorkflow = destination;
                            break;
                        }

                        if (property == 'a' && part.a > value)
                        {
                            currentWorkflow = destination;
                            break;
                        }

                        if (property == 's' && part.s > value)
                        {
                            currentWorkflow = destination;
                            break;
                        }
                    }
                }
            }

            Console.Write(currentWorkflow + "\n");

            if (currentWorkflow == "A")
            {
                Console.WriteLine("Part accepted!");
                accepted += part.x + part.m + part.a + part.s;
            }

            if (currentWorkflow == "R")
            {
                Console.WriteLine("Part rejected!");
            }
        }

        Console.WriteLine("Accepted Part Total: " + accepted);
    }

    public static void DoPartTwo()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day19_Test.txt");

        var workflows = new Dictionary<string, string>();

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Trim().Length == 0)
            {
                break;
            }

            var label = lines[i].Split('{')[0];
            var rules = lines[i]
                .Split('{')[1]
                .Split('}')[0];

            workflows.Add(label, "{" + rules + "}");
        }

        // condense
        bool actionPerformed = true;
        while (actionPerformed)
        {
            actionPerformed = false;
            var toRemove = new List<string>();
            foreach (var workflowA in workflows)
            {
                foreach (var workflowB in workflows)
                {
                    if (workflowB.Value.Contains(workflowA.Key))
                    {
                        workflows[workflowB.Key] =
                            workflowB.Value.Replace(workflowA.Key, SimplifyRule(workflowA.Value));
                        if (!toRemove.Contains(workflowA.Key)) toRemove.Add(workflowA.Key);
                        actionPerformed = true;
                    }
                }
            }

            for (int i = 0; i < toRemove.Count; i++)
            {
                workflows.Remove(toRemove[i]);
            }
        }
        
        var finalRules = workflows["in"].Split('{')[1]
            .Split('}')[0].Split(',');

        var finalRule = "";
        for (int i = 0; i < finalRules.Length;i++)
        {
            if (finalRules[i].Length == 1)
            {
                finalRule += finalRules[i];
                break;
            }
            
            finalRule += finalRules[i] + ",";
        }
    }


    public static string SimplifyRule(string rule)
    {
        rule = rule
            .Split('{')[1]
            .Split('}')[0];

        var rules = rule.Split(',');

        var outcome = "";
        if (rules[0].Contains(':'))
        {
            outcome = rules[0].Split(':')[1];
        }
        else
        {
            outcome = rules[0];
        }

        for (int i = 0; i < rules.Length; i++)
        {
            if (rules[i].Contains(':'))
            {
                if (rules[i].Split(':')[1] != outcome)
                {
                    return rule;
                }
            }
            else
            {
                if (rules[i] != outcome)
                {
                    return rule;
                }
            }
        }

        return outcome;
    }
}