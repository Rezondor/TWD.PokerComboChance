using TWD.PokerComboChance.Application.Interfaces;
using TWD.PokerComboChance.Core.Models;

namespace TWD.PokerComboChance.Calculators.Models.Combination;

public class StraightFlashCombination : ICombination
{
    public bool IsCombination(ICollection<Card> cards, out ICollection<Card> cardsUsed)
    {
        var flashHelper = new FlashCombination();

        if (!flashHelper.IsCombination(cards, out var firstComb))
        {
            cardsUsed = [];
            return false;
        }

        if (firstComb.Last().Value != firstComb.First().Value + 4)
        {
            firstComb = firstComb.Select(x =>
            {
                x.IsUseInCombination = false;
                return x;
            }).ToList();

            cardsUsed = [];
            return false;
        }

        cardsUsed = firstComb;
        return true;
    }
}
