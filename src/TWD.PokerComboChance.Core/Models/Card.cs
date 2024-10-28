using System.Diagnostics;
using TWD.PokerComboChance.Core.Enums;

namespace TWD.PokerComboChance.Core.Models;

/// <summary>
/// Карта
/// </summary>
[DebuggerDisplay("Масть - {Suit}, Значение - {Value}, Использована - {IsUseInCombination}")]
public class Card
{
    /// <summary>
    /// Масть
    /// </summary>
    public CardSuits Suit { get; set; }
    
    /// <summary>
    /// Значение
    /// </summary>
    public CardValues Value { get; set; }


    /// <summary>
    /// Использована ли в комбинации?
    /// </summary>
    public bool IsUseInCombination { get; set; } = false;
}
