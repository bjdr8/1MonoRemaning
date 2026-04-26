using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkillScriptable : ScriptableObject
{
    public string nameId;
    public virtual void ApplyEffect(IAbilityUser player) { Debug.LogError("Apply Effect To PlayerProfile2 NULL"); }
    public virtual void ApplyEffect(IModifiableStats player) { Debug.LogError("Apply Effect To PlayerControler2 NULL"); }
    public virtual void RevertEffect(IModifiableStats player) { Debug.LogError("Apply Effect To PlayerProfile2 NULL"); }
}
