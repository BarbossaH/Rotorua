public enum CardTypeEnum
{
    None,
    AttackCard,
    SkillCard,
    ExhaustCard
}

public enum CardLifecycleEnum
{
    None,
    Discard, //when it's used, it's expended into the discard pile.
    Exhaust, //when it's used, it's expended into the expenditure pile and removed from the current battle
    Purge, //when the current turn over, the cards still are in the hand, then they are expended into the expenditure pile and are removed from the current battle
}
public enum CardOwnershipEnum
{
    Common,
    Warrior,
    Ranger
}

public enum CardSourceEnum
{
    None,
    PlayerDeck,
    EventGenerated
}