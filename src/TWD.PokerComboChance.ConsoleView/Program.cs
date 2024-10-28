using System.Collections.Concurrent;
using TWD.PokerComboChance.Application.Interfaces;
using TWD.PokerComboChance.Calculators.Models.Combination;
using TWD.PokerComboChance.Core.Enums;
using TWD.PokerComboChance.Core.Models;
using System.Linq;

namespace TWD.PokerComboChance.ConsoleView;

public class Program
{
    static Random rand = new Random();

    // Колода карт (52 карты)
    static List<Card> GenerateDeck()
    {
        var suits = new[] { CardSuits.Hearts, CardSuits.Clubs, CardSuits.Spades, CardSuits.Diamonds };
        var ranks = new CardValues[13];

        for (int i = 2; i < 15; i++)
        {
            ranks[i - 2] = (CardValues)i;
        }

        var deck = new List<Card>();
        foreach (var suit in suits)
        {
            foreach (var rank in ranks)
            {
                deck.Add(new Card() { Suit = suit, Value = rank });
            }
        }
        return deck;
    }

    static List<Card> DealCards(List<Card> deck, int count)
    {
        var hand = new List<Card>();
        for (int i = 0; i < count; i++)
        {
            int index = rand.Next(deck.Count);
            hand.Add(deck[index]);
            deck.RemoveAt(index);
        }
        return hand;
    }

    static void Main(string[] args)
    {
        Dictionary<CardCombinations, ICombination> combs = new(10)
        {
            { CardCombinations.HightCard, new HightCardCombination()},
            { CardCombinations.OnePair, new OnePairCombination()},
            { CardCombinations.TwoPair, new TwoPairCombination()},
            { CardCombinations.ThreeOfKind, new ThreeOfKindCombination()},
            { CardCombinations.Straight, new StraightCombination()},
            { CardCombinations.Flash, new FlashCombination()},
            { CardCombinations.FullHouse, new FullHouseCombination()},
            { CardCombinations.FourOfKind, new FourOfKindCombination()},
            { CardCombinations.StraightFlash, new StraightFlashCombination()},
            { CardCombinations.RoyalFlush, new FiveOfKindCombination()},

        };

        ConcurrentDictionary<CardCombinations, int> results = new();
        foreach (var item in Enum.GetValues<CardCombinations>())
        {
            results.TryAdd(item, 0);
        }

        const int MaxCardInComb = 5;
        const int MaxSimCount = 1000000;
        int batch = MaxSimCount / 10;
        Console.WriteLine("Начало");
        Parallel.For(0, MaxSimCount, new ParallelOptions { MaxDegreeOfParallelism = 10}, (i) =>
        {
            if (i % batch == 0)
            {
                Console.WriteLine($"Пачка - {i}");
            }
            var deck = GenerateDeck();

            //var playerHand = DealCards(deck, 2);
            var playerHand = new List<Card>
            {
                new() { Suit = CardSuits.Spades, Value = CardValues.Ace},
                new() { Suit = CardSuits.Spades, Value = CardValues.King},
            };

            for (int j = 0; j < 2; j++)
            {
                deck.Remove(playerHand[j]);
            }

            var tableCards = DealCards(deck, 5);

            List<(CardCombinations combName, IEnumerable<Card> cards)> combinations = [];

            var countCardInComb = 0;
            var conc = playerHand.Concat(tableCards).ToList();
            while (countCardInComb < MaxCardInComb)
            {
                ICollection<Card> combCard = [];
                CardCombinations name = CardCombinations.HightCard;

                if (MaxCardInComb - countCardInComb != 5)
                {
                    combs[name].IsCombination(conc, out combCard);
                }
                else
                {
                    for (int j = 9; j >= 0; j--)
                    {
                        name = (CardCombinations)j;
                        if (combs[name].IsCombination(conc, out combCard))
                        {
                            break;
                        }
                    }

                }

                combinations.Add((name, combCard));
                countCardInComb += combCard.Count;

            }

            foreach (var card in combinations.Where(card => card.cards.Contains(playerHand[0]) || card.cards.Contains(playerHand[1])))
            {
                results[card.combName]++;
                break;
            }
        });

        foreach (var item in results)
        {
            Console.WriteLine($"Комбинация - {item.Key,15}| Шанс выпадения - {item.Value / (double)MaxSimCount,7:p5}");
        }


        /* Console.WriteLine();
         Console.WriteLine("Рука");
         foreach (var card in playerHand)
         {
             PrintCard(card);
         }

         Console.WriteLine();
         Console.WriteLine("Стол");
         foreach (var card in tableCards)
         {
             PrintCard(card);
         }

         Console.WriteLine();
         Console.WriteLine("Комбинации:");
         foreach (var comb in combinations)
         {
             Console.WriteLine(comb.Item1);
             var a = comb.Item2;
             foreach (var item in a)
             {
                 PrintCard(item);
             }
         }

         Console.WriteLine();*/
    }

    private static void PrintCard(Card card)
    {
        Console.WriteLine($"Масть - {card.Suit,8}, Значение - {card.Value,5}");
    }
}
