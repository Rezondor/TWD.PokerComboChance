using TWD.PokerComboChance.Application.Interfaces;
using TWD.PokerComboChance.Core.Enums;
using TWD.PokerComboChance.Core.Models;

namespace TWD.PokerComboChance.Calculators.Models.Combination;

public class StraightCombination : ICombination
{
    public bool IsCombination(ICollection<Card> cards, out ICollection<Card> cardsUsed)
    {
        var orderedValues = cards.Select(x => x.Value).Distinct().OrderByDescending(x=>x).ToList();

        if (orderedValues.Count < 5)
        {
            cardsUsed = [];
            return false;
        }

        for (int i = 0; i <orderedValues.Count - 4; i++)
        {
            if (orderedValues[i + 4] != orderedValues[i] - 4)
            {
                continue;
            }

            var result = new List<Card>(5);
            foreach (var item in Enumerable.Range((int)orderedValues[i] - 4, 5))
            {
                var a = cards.First(x => x.Value == (CardValues)item);
                a.IsUseInCombination = true;
                result.Add(a);
            }
            cardsUsed = result;
            return true;
        }

        cardsUsed = [];
        return false;

    }
}
