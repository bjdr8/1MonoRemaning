using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControler2 : IAbilityUser
{
    public List<AbilityScript> abilityList { get; set; } = new List<AbilityScript>();
    public List<AbilityScript> abilityCooldownList { get; set; } = new List<AbilityScript>();
    public Rigidbody2D rb { get; set; }

    private Weapon2 equipedWeapon;
    private List<GameObject> weapons;

    //private GameManager2 gameManager;

    //private Ar ar;
    //private Pistol pistol;
    //private MiniGun minigun;

    public PlayerControler2(GameObject player)
    {
        rb = player.GetComponent<Rigidbody2D>();
        //this.movementSpeed = movementSpeed;
        //this.weapons = weapons;
        //this.gameManager = gameManager;
        //SetupWeapons();
    }

    public void UseAbility()
    {
        if (abilityList.Count == 0) return;
        foreach (AbilityScript ability in abilityList)
        {
            if (Input.GetKeyDown(ability.abilityKey))
            {
                ability.ApplyEffect(this);
                abilityCooldownList.Add(ability);
            }
        }
    }

    public void Cooldowntimer() // function for checking if abilities are off cooldown
    {
        foreach (AbilityScript ability in abilityCooldownList)
        {
            ability.cooldownTimer -= Time.deltaTime;
            if (ability.cooldownTimer <= 0)
            {
                AbilityOffCooldown(ability);
            }
        }
    }

    public void AbilityOnCooldown(AbilityScript ability)
    {
        abilityList.Remove(ability);
        ability.cooldownTimer = ability.cooldownTime;
        abilityCooldownList.Add(ability);
    }

    public void AbilityOffCooldown(AbilityScript ability)
    {
        abilityCooldownList.Remove(ability);
        abilityList.Add(ability);
    }

    //public void Shooting(Vector2 direction)
    //{
    //    equipedWeapon.fireCooldown -= Time.deltaTime;

    //    if (Input.GetMouseButton(0) && equipedWeapon.fireCooldown <= 0f)
    //    {
    //        equipedWeapon.Shoot(direction, this.gameManager);
    //        equipedWeapon.fireCooldown = equipedWeapon.fireRate; // Reset cooldown
    //    }
    //}

    //public void ChangeWeapon()
    //{
    //    if (Input.GetKeyUp(KeyCode.Alpha1))
    //    {
    //        equipedWeapon = pistol;
    //        ChangeVisibleWeapon(0);
    //    }
    //    if (Input.GetKeyUp(KeyCode.Alpha2))
    //    {
    //        equipedWeapon = ar;
    //        ChangeVisibleWeapon(1);
    //    }
    //    if (Input.GetKeyUp(KeyCode.Alpha3))
    //    {
    //        equipedWeapon = minigun;
    //        ChangeVisibleWeapon(2);
    //    }
    //}

    public void ChangeVisibleWeapon(int weaponNumber)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weaponNumber == i)
            {
                weapons[i].SetActive(true);
            }
            else
            {
                weapons[i].SetActive(false);
            }
        }
    }

    public void Movement(PlayerProfile2 playerProfile)
    {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.AddForce(moveDirection * playerProfile.MovementSpeed, ForceMode2D.Force);
    }

    //private void SetupWeapons()
    //{
    //    for (int i = 0; i < weapons.Count; i++)
    //    {
    //        switch (weapons[i].name)
    //        {
    //            case "Ar":
    //                ar = new Ar(weapons[i]);
    //                break;
    //            case "Pistol":
    //                pistol = new Pistol(weapons[i]);
    //                break;
    //            case "MiniGun":
    //                minigun = new MiniGun(weapons[i]);
    //                break;
    //        }
    //    }
    //    equipedWeapon = ar;
    //}
}
