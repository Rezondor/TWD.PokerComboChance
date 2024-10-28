using TWD.PokerComboChance.Application.Interfaces;
using TWD.PokerComboChance.Core.Models;

namespace TWD.PokerComboChance.Calculators.Models.Combination;

public class FullHouseCombination : ICombination
{
    public bool IsCombination(ICollection<Card> cards, out ICollection<Card> cardsUsed)
    {
        var pairHelper = new OnePairCombination();
        var threeOfKindHelper = new ThreeOfKindCombination();

        if (!threeOfKindHelper.IsCombination(cards, out var firstComb))
        {
            cardsUsed = [];
            return false;
        }

        if (!pairHelper.IsCombination(cards, out var secondComb))
        {
            firstComb = firstComb
                .Select(x =>
                {
                    x.IsUseInCombination = false;
                    return x;
                })
                .ToList();

            cardsUsed = [];
            return false;
        }

        cardsUsed = [.. firstComb, .. secondComb];
        return true;
    }
}
