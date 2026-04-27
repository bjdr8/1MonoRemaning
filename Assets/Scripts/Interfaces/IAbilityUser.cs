using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbilityUser
{
    public List<AbilityScript> abilityList { get; set; }
    public List<AbilityScript> abilityCooldownList { get; set; }
    public Rigidbody2D rb { get; set; }
    public void UseAbility();
}
