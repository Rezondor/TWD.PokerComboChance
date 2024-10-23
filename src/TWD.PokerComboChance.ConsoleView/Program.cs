namespace TWD.PokerComboChance.ConsoleView;

public class Program
{
    static Random rand = new Random();

    // Колода карт (52 карты)
    static List<string> GenerateDeck()
    {
        var suits = new[] { "Hearts", "Diamonds", "Clubs", "Spades" };
        var ranks = new[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        var deck = new List<string>();
        foreach (var suit in suits)
        {
            foreach (var rank in ranks)
            {
                deck.Add(rank + " of " + suit);
            }
        }
        return deck;
    }

    static List<string> DealCards(List<string> deck, int count)
    {
        var hand = new List<string>();
        for (int i = 0; i < count; i++)
        {
            int index = rand.Next(deck.Count);
            hand.Add(deck[index]);
            deck.RemoveAt(index);  
        }
        return hand;
    }

    static bool HasPair(List<string> cards)
    {
        var ranks = cards.Select(card => card.Split(' ')[0]).ToList(); 
        var grouped = ranks.GroupBy(r => r).Where(g => g.Count() >= 2); 
        return grouped.Any();
    }

    static double SimulatePokerHands(int trials)
    {
        int pairCount = 0;
        for (int i = 0; i < trials; i++)
        {
            var deck = GenerateDeck();
            var playerHand = DealCards(deck, 2);  
            var tableCards = DealCards(deck, 5);  
            var totalCards = playerHand.Concat(tableCards).ToList(); 

            if (HasPair(totalCards))
            {
                pairCount++;
            }
        }
        return (double)pairCount / trials;
    }

    static void Main(string[] args)
    {
        int trials = 100000;
        double probability = SimulatePokerHands(trials);
        Console.WriteLine($"Вероятность получить хотя бы одну пару: {probability * 100}%");
    }
}
