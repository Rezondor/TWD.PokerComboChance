using TWD.PokerComboChance.Application.Interfaces;
using TWD.PokerComboChance.Core.Enums;
using TWD.PokerComboChance.Core.Helpers;
using TWD.PokerComboChance.Core.Models;

namespace TWD.PokerComboChance.Calculators.Models.Combination;

public class ThreeOfKindCombination : ICombination
{
    public bool IsCombination(ICollection<Card> cards, out ICollection<Card> cardsUsed)
    {
        Dictionary<CardValues, int> dctCard = cards.GetCardValueCount();

        var maxPair = dctCard.Where(x => x.Value == 3)
                        .OrderByDescending(x => x.Key)
                        .FirstOrDefault();

        if (maxPair.Key == CardValues.None)
        {
            cardsUsed = [];
            return false;
        }

        cardsUsed = cards
                    .Where(x => x.Value == maxPair.Key && !x.IsUseInCombination)
                    .Select(x =>
                    {
                        x.IsUseInCombination = true;
                        return x;
                    })
                    .ToList(); 
       
        return true;
    }
}
