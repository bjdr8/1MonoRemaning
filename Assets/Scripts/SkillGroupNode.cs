using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGroupNode : SkillNodeBase
{
    public List<SkillNodeBase> children = new List<SkillNodeBase>();
    public SkillGroupNode(string skillName, int xpCosts, List<BaseEffect> effects)
    {
        this.skillName = skillName;
        this.xpCosts = xpCosts;
        effectsList = effects;
    }

    public void AddSkillNode(SkillNodeBase skillNode) => children.Add(skillNode);
    public override int Buy(int playerXp)
    {
        if (base.Buy(playerXp) != 0)
        {
            if (children.Count > 0)
            {
                foreach (var child in children)
                {
                    child.unlocked = true;
                }
            }
            return xpCosts;
        }
        return 0;
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
