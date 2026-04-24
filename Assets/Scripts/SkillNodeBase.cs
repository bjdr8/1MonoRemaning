using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkillNodeBase
{
    public string skillName;
    public int xpCosts;
    public bool unlocked;
    public bool bought;
    public bool active;
    public Image image;
    public List<BaseEffect> effectsList;

    public abstract void Unlock();

    public virtual int Buy(int playerXp)
    {
        if (unlocked == false)
        {
            Debug.Log("This skill is still locked");
            return 0;
        }

        if (xpCosts <= playerXp && bought == false)
        {
            Debug.Log("aquired skill");
            active = true;
            bought = true;
            return xpCosts;
        } else
        {
            Debug.Log("not enough xp to buy");
            return 0;
        }
    }

    public virtual int Reset()
    {
        active = false;
        Debug.Log("u sold the item");
        return xpCosts;
    }

    public virtual void ImageChange(int playerXp)
    {
        Debug.Log("Name: " + skillName);
        Debug.Log("image change called; " + "bought: " + bought + "; " + "playerXp: " + playerXp);

        if (bought == true)
        {
            image.color = Color.blue;
        }
        else if (playerXp >= xpCosts && unlocked == true)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.red;
        }
    }
}
