using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class PlayerProfile2 : IModifiableStats
{
    public int maxHp = 6;
    public int hp;
    private float movementSpeed = 25f;
    private float ExtraDamage = 0;
    private float playerDrag = 25f;
    public int xp { get; private set; } = 500;
    public float MovementSpeed { get { return movementSpeed; } private set { movementSpeed = value; } }

    public List<StatModifierScript> statModList { get; set; } = new List<StatModifierScript>();

    public event Action<int> OnXpChanged;

    public PlayerControler2 playerControler;

    public PlayerProfile2(GameObject playerObject, List<WeaponScript> weaponScripts)
    {
        playerObject.GetComponent<Rigidbody2D>().drag = playerDrag;
        playerControler = new PlayerControler2(playerObject, weaponScripts);
    }

    public void AddXp(int xp)
    {
        this.xp += xp;
        OnXpChanged.Invoke(this.xp);
        Debug.Log(this.xp);
    }

    public void RemoveXp(int xp)
    {
        this.xp -= xp;
        OnXpChanged.Invoke(this.xp);
        Debug.Log(this.xp);
    }

    public void LoadStatModList(List<StatModifierScript> statModList)
    {
        this.statModList = statModList;
    }

    public void ChangeStat(StatModifier.Stats statChanged, float statChangeAmount, bool positive)
    {
        if (!positive)
        {
            statChangeAmount = -statChangeAmount;
        }
        switch (statChanged)
        {
            case StatModifier.Stats.maxHp:
                maxHp += (int)statChangeAmount;
                break;
            case StatModifier.Stats.movementSpeed:
                movementSpeed += statChangeAmount;
                break;
            case StatModifier.Stats.damage:
                ExtraDamage += statChangeAmount;
                break;
                //case Stats.attackSpeed:
                //    foreach (GameObject weapon in playerControler.weapons)
                //    {
                //        weapon.GetComponent<Weapon2>().fireRate -= statChangeAmount;
                //    }
                //    break;
                //case Stats.defense:
                //    // defense stat can be implemented as damage reduction or something similar, but for now it does nothing
                //    break;
        }
    }

    public void Update()
    {
        playerControler.Update(this);
    }
}
