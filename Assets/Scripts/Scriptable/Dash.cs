using UnityEngine;

[CreateAssetMenu(fileName = "DashAbility", menuName = "Skill System/Ability/Dash")]
public class Dash : AbilityScript
{
    public bool dashIsTeleport;
    public float dashDistance = 2f;
    public float dashForce = 10f;

    public override void ApplyEffect(IAbilityUser player)
    {
        Vector2 dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (dashDirection == Vector2.zero)
        {
            dashDirection = Vector2.up;
        }

        Vector2 dashTarget = player.rb.position + dashDirection * dashDistance;

        if (dashIsTeleport)
        {
            player.rb.MovePosition(dashTarget); //teleport option
        }
        else
        {
            player.rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
        }
    }
}
