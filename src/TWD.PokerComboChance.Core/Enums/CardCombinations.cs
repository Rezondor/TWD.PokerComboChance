namespace TWD.PokerComboChance.Core.Enums;

/// <summary>
/// Комбинации
/// </summary>
public enum CardCombinations
{
    /// <summary>
    /// Старшая карта
    /// </summary>
    HightCard = 0,

    /// <summary>
    /// Одна пара
    /// </summary>
    OnePair = 1,

    /// <summary>
    /// Две пары
    /// </summary>
    TwoPair = 2,

    /// <summary>
    /// Сет
    /// </summary>
    ThreeOfKind = 3,
    
    /// <summary>
    /// Стрит
    /// </summary>
    Straight = 4,

    /// <summary>
    /// Флеш
    /// </summary>
    Flash = 5,

    /// <summary>
    /// Фулл хаус
    /// </summary>
    FullHouse = 6,

    /// <summary>
    /// Каре
    /// </summary>
    FourOfKind = 7,

    /// <summary>
    /// Стрит флеш
    /// </summary>
    StraightFlash = 8,

    /// <summary>
    /// Флеш рояль
    /// </summary>
    RoyalFlush = 9
}
