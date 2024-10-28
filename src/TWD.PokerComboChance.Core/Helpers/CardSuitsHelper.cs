using TWD.PokerComboChance.Core.Enums;
using TWD.PokerComboChance.Core.Models;

namespace TWD.PokerComboChance.Core.Helpers;

public static class CardSuitsHelper
{
    public static Dictionary<CardSuits, int> GetCardSuitsCount(this IEnumerable<Card> cards)
    {
        var dctCard = new Dictionary<CardSuits, int>();
        foreach (var item in cards)
        {
            if (!item.IsUseInCombination && !dctCard.TryAdd(item.Suit, 1))
            {
                dctCard[item.Suit]++;
            }
        }

        return dctCard;
    }
}
