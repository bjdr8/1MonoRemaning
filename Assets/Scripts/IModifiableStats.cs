using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public interface IModifiableStats
{
    public List<StatModifierScript> statModList { get; set; }
    public void ChangeStat(StatModifier.Stats statChanged, float statChangeAmount, bool isAdding);
}
