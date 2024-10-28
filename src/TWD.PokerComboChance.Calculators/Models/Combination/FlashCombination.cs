using TWD.PokerComboChance.Application.Interfaces;
using TWD.PokerComboChance.Core.Enums;
using TWD.PokerComboChance.Core.Helpers;
using TWD.PokerComboChance.Core.Models;

namespace TWD.PokerComboChance.Calculators.Models.Combination;

public class FlashCombination : ICombination
{
    public bool IsCombination(ICollection<Card> cards, out ICollection<Card> cardsUsed)
    {
        var dctCard = cards.GetCardSuitsCount();

        var maxSuit = dctCard
                        .Where(x => x.Value > 4)
                        .OrderByDescending(x => x.Value)
                        .FirstOrDefault();

        if (maxSuit.Key == CardSuits.All)
        {
            cardsUsed = [];
            return false;
        }

        cardsUsed = cards
                    .OrderBy(x => x.Value)
                    .Where(x => x.Suit == maxSuit.Key && !x.IsUseInCombination)
                    .TakeLast(5)
                    .ToList();


        cardsUsed = cardsUsed.Select(x =>
        {
            x.IsUseInCombination = true;
            return x;
        })
        .ToList();

        return true;
    }
}
