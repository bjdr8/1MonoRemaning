using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SkilltreeSave2 // make seperate save class for literally everything in the game so like the skilltree but also the player xp etc
{
    public List<SkillNodeBase> skillsList;


    [SerializeField] private List<string> unlockedSkills = new List<string>();
    //[SerializeField] private List<string> activeSkills = new List<string>();

    private IModifiableStats modifiableStatObj;
    public SkillTreeManager skillManager;

    public SkilltreeSave2(SkillTreeManager skillManager, IModifiableStats modifiableStatObj)
    {
        this.skillManager = skillManager;
        this.modifiableStatObj = modifiableStatObj;
    }

    public void Save()
    {
        skillsList = skillManager.skillTreeNodeList;
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(GetSavePath(), json);
    }
    // i don't need to save everything. if i save the skilltree and i guess maybe the player profile for the xp i don't need to save anything else becasue from the skilltree data i can just replace everything with the data from the save of the skilltree (except xp from what i can think of right now)
    public void Load() // in the middel of changing the loading of the skill tree. right now it ovverides the skilltreemanager again with the save data. i still need to see how i would implement the changes to where the skill list are. my toughts right now would be to put it in the skilltree manager so basicly a function that says check skillnodes and then it checks wich one are bought and active then checks what effect it has and depending on the effect it gives it to the player controler3 or the playerprogile
    {
        string path = GetSavePath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);

            for (int i = 0; i < skillManager.skillTreeNodeList.Count; i++)
            {
                for (int j = 0; j < skillsList.Count; j++)
                {
                    if (skillManager.skillTreeNodeList[i].skillName == skillsList[j].skillName)
                    {
                        skillManager.skillTreeNodeList[i] = skillsList[j];
                    }
                }
            }
        }
        else
        {
            Debug.Log("Save Not Found");
        }
    }

    public void ResetSkills()
    {
        unlockedSkills.Clear();
        //activeSkills.Clear();
        UpdatePassiveEffects();
        foreach (SkillNodeBase skill in skillManager.skillTreeNodeList)
        {
            if (skill.skillName == "SkillTreeRoot")
            {
                skill.unlocked = true;
                skill.active = false;
                skill.bought = false;
            }
            else
            {
                skill.unlocked = false;
                skill.active = false;
                skill.bought = false;
            }
            skill.ImageChange(0);
        }

        string path = GetSavePath();
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        Save(); // Optional: save the cleared state
    }

    public void AddUnlockedSkill(string skill)
    {
        unlockedSkills.Add(skill);
        AddActiveSkill(skill);
    }

    public void RemoveUnlockedSkill(string skill)
    {
        unlockedSkills.Remove(skill);
    }

    public void AddActiveSkill(string skill)
    {
        //activeSkills.Add(skill);
        UpdatePassiveEffects();
    }

    public void RemoveActiveSkill(string skill)
    {
        //activeSkills.Remove(skill);
        UpdatePassiveEffects();
    }

    public void UpdatePassiveEffects()
    {
        //modifiableStatObj.Init(activeSkills);
    }

    public void UpdateSkills()
    {

    }

    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "skilltree.json");
    }
}
