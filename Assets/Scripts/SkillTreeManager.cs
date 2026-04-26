using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager
{
    //private SkillGroupNode skilltree;
    private SkilltreeSave2 skilltreeSave; // script to save and load data to a txt file
    private GameObject skillButtonPrefab; // button prefab to make the buttons for the skill tree

    private GameManager2 gameManager; // kan efficienter

    public List<SkillNodeBase> skillsList = new List<SkillNodeBase>();
    public SkillTreeManager(ScriptableSkillNode rootOfSkillTree,
        RectTransform skillTreePanel,
        GameObject buttonPrefab,
        GameManager2 gameManager,
        PlayerProfile2 playerProfile)
    {
        skillButtonPrefab = buttonPrefab;
        this.gameManager = gameManager;

        skilltreeSave = new SkilltreeSave2(this, playerProfile);

        CreateSkillUI(GenerateSkilltree(rootOfSkillTree, playerProfile), new Vector2(0, 0), skillTreePanel, playerProfile);
    }

    private SkillGroupNode GenerateSkilltree(ScriptableSkillNode rootNode, PlayerProfile2 playerProfile)
    {
        SkillGroupNode currentNode = new SkillGroupNode(rootNode.skillName, rootNode.xpCosts, rootNode.effects);
        playerProfile.OnXpChanged += (xp) => currentNode.ImageChange(xp);
        skillsList.Add(currentNode);
        currentNode.unlocked = rootNode.unlocked;

        if (currentNode.effectsList != null)
        {
            foreach (var effect in currentNode.effectsList)
            {
                effect.nameId = rootNode.skillName;
            }
        }

        foreach (ScriptableSkillNode child in rootNode.children)
        {
            if (child.children.Count == 0)
            {
                SkillLeafNode skillNode = new SkillLeafNode(child.skillName, child.xpCosts, child.effects);

                if (skillNode.effectsList != null)
                {
                    foreach (var effect in skillNode.effectsList)
                    {
                        effect.nameId = child.skillName;
                    }
                }
                playerProfile.OnXpChanged += (xp) => skillNode.ImageChange(xp);
                currentNode.children.Add(skillNode);
                skillsList.Add(skillNode);
            }
            else
            {
                SkillGroupNode childGroup = GenerateSkilltree(child, playerProfile);
                currentNode.children.Add(childGroup);
            }
        }

        return currentNode;
    }

    void CreateSkillUI(SkillNodeBase skill, Vector2 position, RectTransform skillTreePanel, PlayerProfile2 playerProfile)
    {
        GameObject buttonObj = GameObject.Instantiate(skillButtonPrefab, skillTreePanel);
        gameManager.skillButtonList.Add(buttonObj);
        RectTransform rt = buttonObj.GetComponent<RectTransform>();
        rt.anchoredPosition = position;

        Image image = buttonObj.GetComponentInChildren<Image>();

        skill.image = image;

        TextMeshProUGUI text = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        text.text = skill.skillName;
        Button button = buttonObj.GetComponent<Button>();
        //button.onClick.AddListener(() => Debug.Log($"Clicked on {skill.skillName}"));
        button.onClick.AddListener(() =>
        {
            int cost = skill.Buy(playerProfile.xp);
            if (cost == 0) return;
            playerProfile.RemoveXp(cost);
            skilltreeData.AddUnlockedSkill(skill.skillName);
        });


        if (skill is SkillGroupNode group)
        {
            float xSpacing = 160f;
            float ySpacing = 100f;
            int count = group.children.Count;
            int index = 0;

            float totalWidth = GetSubtreeWidth(group);
            float currentX = position.x - (totalWidth * xSpacing / 2f);

            foreach (SkillNodeBase skillnode in group.children)
            {
                float childWidth = GetSubtreeWidth(skillnode);
                Vector2 childPos = position + new Vector2(currentX + (childWidth * xSpacing / 2f), -ySpacing);
                CreateSkillUI(skillnode, childPos, skillTreePanel, playerProfile);
                currentX += childWidth * xSpacing;
                index++;
            }
        }
    }

    float GetSubtreeWidth(SkillNodeBase skill)
    {
        if (skill is SkillGroupNode group)
        {
            float width = 0f;
            foreach (var child in group.children)
            {
                width += GetSubtreeWidth(child);
            }
            return Mathf.Max(width, 1f); // Zorg dat lege groepen minstens 1 breedte-eenheid zijn
        }
        return 1f; // Een enkele node is 1 eenheid breed
    }

    private void buySkill()
    {

    }

    public void SaveSkills()
    {
        //gameDataFacade.SaveAll();
        skilltreeData.Save();
    }

    public void LoadSkills()
    {
        skilltreeData.Load();
    }

    public void ResetSkills()
    {
        skilltreeData.ResetSkills();
    }
}
