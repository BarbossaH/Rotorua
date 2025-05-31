namespace Common.Defs
{
    public enum EffectTypeEnum { None, Damage, Aoe, Buff, Debuff, LifeSteal, Action }
    public enum ElementEnum { None, Fire, Frost, Nature, Arcane }
    public enum SkillTypeEnum { None, Active, Passive }
    public enum ButtStackingTypeEnum { Replace, Additive, MaxOnly }



    public enum TriggerTimingEnum
    {
        None,
        OnTurnBegin, //triggered at the beginning of the turn
        OnTurnEnd, //triggered at the end of the turn
        OnAttack, //triggered when attacking
        OnHit, //triggered when receiving attack
        OnAdd, //triggered when the buff is added, generally not used
        OnRemove //triggered when then buff is removed
    }

    public enum BuffStatusTypeEnum
    {
        None,
        Attack, //Attack power bonus
        Defense, //Defense bonus
        Vulnerable, //Vulnerable, take extra damage
        CriticalRate, //Critical hit rate
    }
}