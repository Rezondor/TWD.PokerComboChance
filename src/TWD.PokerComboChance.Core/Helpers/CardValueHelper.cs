using TWD.PokerComboChance.Core.Enums;
using TWD.PokerComboChance.Core.Models;

namespace TWD.PokerComboChance.Core.Helpers;

public static class CardValueHelper
{
    public static Dictionary<CardValues, int> GetCardValueCount(this IEnumerable<Card> cards)
    {
        var dctCard = new Dictionary<CardValues, int>();
        foreach (var item in cards)
        {
            if (!item.IsUseInCombination && !dctCard.TryAdd(item.Value, 1))
            {
                dctCard[item.Value]++;
            }
        }

        return dctCard;
    }
}
