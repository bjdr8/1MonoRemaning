using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier
{
    private bool statModified = false;
    private List<StatModifierScript> activeStatModList = new List<StatModifierScript>();
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
        if (statModified)
        {
            RevertStatMods(statObject);
            foreach (StatModifierScript statMod in statObject.statModList)
            {
                statMod.ApplyEffect(statObject);
                activeStatModList.Add(statMod);
            }
        }
        else
        {
            foreach (StatModifierScript statMod in statObject.statModList)
            {
                statMod.ApplyEffect(statObject);
                activeStatModList.Add(statMod);
            }
        }
        statModified = true;
    }
    public void RevertStatMods(IModifiableStats statObject)
    {
        foreach (StatModifierScript statMod in activeStatModList)
        {
            statMod.RevertEffect(statObject);
        }
        activeStatModList.Clear();
        statModified = false;
    }
}
