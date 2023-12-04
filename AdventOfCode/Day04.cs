public static class Day04
{
    public static void DoPartOne()
    {
        var lines = File.ReadAllLines("C:\\Development\\Aesthetician Labs\\AdventOfCode2023\\AdventOfCode\\Day04_Test.txt");

        var totalValue = 0;
        
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            var cardNumber = i + 1;
            var winningNumber = line.Split(':')[1].Split('|')[0].Split(' ').ToList();
            var myNumbers = line.Split(':')[1].Split('|')[1].Split(' ').ToList();

            var cardValue = 0;
            
            for (int n = 0; n < winningNumber.Count; n++)
            {
                if(winningNumber[n].Length == 0) continue;

                for (int o = 0; o < myNumbers.Count; o++)
                {
                    if(myNumbers[o].Length == 0) continue;
                    
                    if (winningNumber[n] == myNumbers[o])
                    {
                        if (cardValue == 0)
                        {
                            cardValue = 1;
                        }
                        else
                        {
                            cardValue = cardValue * 2;
                        }
                    }
                }
            }
            
            Console.WriteLine("Card " + cardNumber + " value: " + cardValue);

            totalValue += cardValue;
        }
        
        Console.WriteLine("Total Value: " + totalValue);
    }
    
    public static void DoPartTwo()
    {
        var lines = File.ReadAllLines("C:\\Development\\Aesthetician Labs\\AdventOfCode2023\\AdventOfCode\\Day04.txt");

        var totalCards = 0;

        List<int> bonusCards = new List<int>();
        
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            var cardNumber = i + 1;
            var winningNumber = line.Split(':')[1].Split('|')[0].Split(' ').ToList();
            var myNumbers = line.Split(':')[1].Split('|')[1].Split(' ').ToList();

            var cardValue = 0;
            for (int n = 0; n < winningNumber.Count; n++)
            {
                if(winningNumber[n].Length == 0) continue;

                for (int o = 0; o < myNumbers.Count; o++)
                {
                    if(myNumbers[o].Length == 0) continue;
                    
                    if (winningNumber[n] == myNumbers[o])
                    {
                        cardValue++;
                    }
                }
            }
            
            Console.WriteLine("Card " + cardNumber + " value: " + cardValue);

            var currentCopies = bonusCards.Count > 0 ? 1 + bonusCards[0] : 1;
            if( bonusCards.Count > 0) bonusCards.RemoveAt(0);
            
            for (int c = 0; c < cardValue; c++)
            {
                if (c >= bonusCards.Count)
                {
                    bonusCards.Add(currentCopies);
                }
                else
                {
                    bonusCards[c] += currentCopies;
                }
            }

            totalCards += currentCopies;
        }
        
        Console.WriteLine("Total Cards: " + totalCards);
    }
}