using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier
{
    public enum Stats
    {
        maxHp,
        movementSpeed,
        damage,
    }

    public void AddStatMod(IModifiableStats statObject, StatModifierScript statMod) 
    {
        statObject.statModList.Add(statMod);
    }
    public void RemoveStatMod(IModifiableStats statObject, StatModifierScript statMod)
    {
        statObject.statModList.Remove(statMod);
    }

    public void ApplyStatMods(IModifiableStats statObject)
    {
        foreach (StatModifierScript statMod in statObject.statModList)
        {
            statMod.ApplyEffect(statObject);
        }
    }
    public void RevertStatMods(IModifiableStats statObject)
    {
        foreach (StatModifierScript statMod in statObject.statModList)
        {
            statMod.RevertEffect(statObject);
        }
    }
}
