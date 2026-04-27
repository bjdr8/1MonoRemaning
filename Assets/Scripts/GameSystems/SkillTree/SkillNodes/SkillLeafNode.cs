using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLeafNode : SkillNodeBase
{
    public SkillLeafNode(string skillName, int xpCosts, List<BaseSkillScriptable> effects)
    {
        this.skillName = skillName;
        this.xpCosts = xpCosts;
        nodeEffects = effects;
    }

    //public override void ImageChange(int playerXp)
    //{
    //    base.ImageChange(playerXp);
    //}
}
