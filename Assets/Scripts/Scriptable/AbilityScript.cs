using UnityEngine;

public abstract class AbilityScript : ScriptableObject
{
    public string nameId;
    public abstract void ApplyEffect(PlayerControler player);
    public abstract void RevertEffect(PlayerControler player);
}
