public static class Day07
{
    public static void DoPartOne()
    {
        var lines = File.ReadAllLines(
            "C:\\Development\\AeLa\\AdventOfCode2023\\AdventOfCode\\Day07.txt");

        var hands = new List<(string, int)>();

        var cards = new List<char>() { 'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T',  'Q', 'K', 'A' };
        
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            hands.Add((line.Split(' ')[0], int.Parse(line.Split()[1])));
        }

        Console.WriteLine("File Order: ");
        for (int i = 0; i < hands.Count; i++)
        {
            var hand = hands[i];
            
            Console.WriteLine(hand.Item1);
        }
        
        hands.Sort(delegate((string,int) h1, (string,int) h2) { return CompareHands(h1.Item1, h2.Item1, cards); });
        
        Console.WriteLine("Ranked: ");
        var rankTotal = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            var hand = hands[i];
            
            Console.WriteLine(hand.Item1 + " Rank: " + (i+1));

            rankTotal += (i + 1) * hand.Item2;
        }
        
        Console.WriteLine("Calculated Score: " + rankTotal);
    }

    public static int GradeHand(string hand, List<char> cardScores)
    {
        var cards = new Dictionary<int, int>();
        int jCount = 0;
        
        for (int i = 0; i < hand.Length; i++)
        {
            var card = hand[i];

            if (card == 'J')
            {
                jCount++;
                continue;
            }
            
            if (!cards.ContainsKey(cardScores.IndexOf(card)))
            {
                cards.Add(cardScores.IndexOf(card), 1);
            }
            else
            {
                cards[cardScores.IndexOf(card)]++;
            }
        }

        // catch the all J case
        if (jCount == 5)
        {
            return 6;
        }

        int highCount = 0;
        int highestKey = -1;
        foreach (var card in cards)
        {
            if (card.Value > highCount)
            {
                highCount = card.Value;
                highestKey = card.Key;
            }
        }

        if (highestKey == -1)
        {
            Console.WriteLine("Probably we shouldn't ever reach this");
        }
        else
        {
            cards[highestKey] += jCount;
        }
        
        // high card
        if (cards.Count == 5)
        {
            return 0;
        }

        // one pair
        if (cards.Count == 4)
        {
            return 1;
        }

        // this is either two pair or three of a kind
        if (cards.Count == 3)
        {
            var highestCount = 0;
            foreach (var card in cards)
            {
                if (card.Value > highestCount)
                {
                    highestCount = card.Value;
                }
            }

            // two pair
            if (highestCount == 2)
            {
                return 2;
            }
            // three of a kind
            if (highestCount == 3)
            {
                return 3;
            }
        }

        // this is either full house or four of a kind
        if (cards.Count == 2)
        {
            var highestCount = 0;
            foreach (var card in cards)
            {
                if (card.Value > highestCount)
                {
                    highestCount = card.Value;
                }
            }

            // two pair
            if (highestCount == 3)
            {
                return 4;
            }
            // three of a kind
            if (highestCount == 4)
            {
                return 5;
            }
        }

        // this is five of a kind
        if (cards.Count == 1)
        {
            return 6;
        }

        return -1;
    }

    public static int CompareHands(string h1, string h2, List<char> cardScores)
    {

        var h1Grade = GradeHand(h1, cardScores);
        var h2Grade = GradeHand(h2, cardScores);

        if (h1Grade > h2Grade)
        {
            return 1;
        }

        if (h1Grade < h2Grade)
        {
            return -1;
        }
        
        // they have the same grade
        int index = 0;
        while (index < h1.Length)
        {
            // h1 wins high card
            if (cardScores.IndexOf(h1[index]) > cardScores.IndexOf(h2[index]))
            {
                return 1;
            }
            // h1 wins high card
            if (cardScores.IndexOf(h1[index]) < cardScores.IndexOf(h2[index]))
            {
                return -1;
            }

            index++;
        }
        
        return 0;
    }
}
