using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBehaviors
{
    public virtual void ActivateEffect(CharacterStats _character)
    {
        Debug.Log($"Status Behaviors was activated!");
    }
}

#region Buffs
public class Regen : StatusBehaviors
{
    public override void ActivateEffect(CharacterStats _character)
    {
        _character.HealDamage(_character.maxHp / 10);
    }
}
#endregion

#region Debuffs
public class Poison : StatusBehaviors
{
    public override void ActivateEffect(CharacterStats _character)
    {
        _character.TakeDamage(_character.maxHp / 10, "None");
    }
}
#endregion