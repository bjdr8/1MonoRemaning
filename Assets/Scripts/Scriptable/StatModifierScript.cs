using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatModifierScript : BaseSkillScriptable
{
    public StatModifier.Stats statChanged;
    public float statChangeAmount;
    public override void ApplyEffect(IModifiableStats player)
    {
        player.ChangeStat(statChanged, statChangeAmount, true);
    }

    public override void RevertEffect(IModifiableStats player)
    {
        player.ChangeStat(statChanged, statChangeAmount, false);
    }

}
