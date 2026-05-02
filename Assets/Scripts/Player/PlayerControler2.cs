using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControler2 : IAbilityUser
{
    public List<AbilityScript> abilityList { get; set; } = new List<AbilityScript>();
    public List<AbilityScript> abilityCooldownList { get; set; } = new List<AbilityScript>();
    public Rigidbody2D rb { get; set; }
    private WeaponController weaponController;

    public PlayerControler2(GameObject player, List<WeaponScript> weaponScripts)
    {
        rb = player.GetComponent<Rigidbody2D>();
        weaponController = new WeaponController(player, weaponScripts);
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

    public void ChangeWeapon()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            weaponController.EquipWeapon(WeaponController.WeaponName.Pistol);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            weaponController.EquipWeapon(WeaponController.WeaponName.Rifle);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            weaponController.EquipWeapon(WeaponController.WeaponName.Minigun);
        }
    }

    public void Movement(PlayerProfile2 playerProfile)
    {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.AddForce(moveDirection * playerProfile.MovementSpeed, ForceMode2D.Force);
    }

    public void Update(PlayerProfile2 playerProfile)
    {
        Movement(playerProfile);
        UseAbility();
        Cooldowntimer();
        ChangeWeapon();
        weaponController.Update();
    }
}
