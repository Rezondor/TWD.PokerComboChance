using TWD.PokerComboChance.Core.Models;

namespace TWD.PokerComboChance.Application.Interfaces;

public interface ICombination
{
    public bool IsCombination(ICollection<Card> cards, out ICollection<Card> cardsUsed);
}
