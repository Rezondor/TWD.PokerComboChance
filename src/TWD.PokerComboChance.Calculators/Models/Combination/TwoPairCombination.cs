using System.Runtime.CompilerServices;
using TWD.PokerComboChance.Application.Interfaces;
using TWD.PokerComboChance.Core.Models;

namespace TWD.PokerComboChance.Calculators.Models.Combination;

public class TwoPairCombination : ICombination
{
    public bool IsCombination(ICollection<Card> cards, out ICollection<Card> cardsUsed)
    {
        var onePairHelper = new OnePairCombination();

        if (!onePairHelper.IsCombination(cards, out var firstComb))
        {
            cardsUsed = [];
            return false;
        }

        if (!onePairHelper.IsCombination(cards, out var secondComb))
        {
            firstComb = firstComb.Select(x =>
            {
                x.IsUseInCombination = false;
                return x;
            }).ToList();
            
            cardsUsed = [];
            return false;
        }

        cardsUsed = [.. firstComb, .. secondComb];
        return true;
    }
}
