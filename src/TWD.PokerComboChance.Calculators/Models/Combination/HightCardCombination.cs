using TWD.PokerComboChance.Application.Interfaces;
using TWD.PokerComboChance.Core.Enums;
using TWD.PokerComboChance.Core.Models;

namespace TWD.PokerComboChance.Calculators.Models.Combination;

public class HightCardCombination : ICombination
{
    public bool IsCombination(ICollection<Card> cards, out ICollection<Card> cardsUsed)
    {
        var maxVal = new Card() { Suit = CardSuits.All, Value = CardValues.None };
        foreach (var card in cards)
            if (!card.IsUseInCombination && card.Value > maxVal.Value)
            {
                maxVal.IsUseInCombination = false;
                maxVal = card;
                maxVal.IsUseInCombination = true;
            }

        cardsUsed = [maxVal];

        return true;
    }
}
