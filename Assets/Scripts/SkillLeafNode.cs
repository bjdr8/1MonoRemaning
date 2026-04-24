using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLeafNode : SkillNodeBase
{
    public SkillLeafNode(string skillName, int xpCosts, List<BaseEffect> effects)
    {
        this.skillName = skillName;
        this.xpCosts = xpCosts;
        effectsList = effects;
    }

    public override void Unlock()
    {
        Debug.Log("item got unlocked now u can buy it with xp");
    }

    public override void ImageChange(int playerXp)
    {
        base.ImageChange(playerXp);
    }
}
