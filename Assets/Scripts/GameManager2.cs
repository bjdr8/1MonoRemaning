using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager2 : MonoBehaviour
{
    private GameState currentState = GameState.StartingMenu;
    [Header("SKillTree info")]
    public ScriptableSkillNode rootNode;
    public GameObject skillButtonPrefab;
    public RectTransform skilltreePanel;
    public PassiveEffect passiveEffect;
    public List<BaseEffect> AllEffects;
    private SkilltreeSave2 skilltreeData;
    private SkillTreeManager skillTreeManager;

    [Header("Player info")]
    public GameObject player;
    public float playerMovementSpeed;
    public float playerDrag;
    private PlayerControler2 playerControler;
    private PlayerProfile2 playerProfile = new PlayerProfile2();
    private BoxCollider2D playerCollider;

    public List<GameObject> skillButtonList;
    public List<GameObject> weapons;
    private enum GameState
    {
        StartingMenu,
        GameOverMenu,
        SkillTreeMenu,
        WinMenu,
        Playing
    }
    void Start()
    {
        passiveEffect = new PassiveEffect(AllEffects);
        skilltreeData = new SkilltreeSave2(passiveEffect);
        skillTreeManager = new SkillTreeManager(rootNode, skilltreePanel, skilltreeData, skillButtonPrefab, this, playerProfile);
        playerControler = new PlayerControler2(player, playerMovementSpeed, playerDrag, playerProfile, weapons, this);
        foreach (SkillNodeBase nodeBase in skillTreeManager.skillsList)
        {
            nodeBase.ImageChange(playerProfile.xp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerControler.Movement();
        //switch (currentState)
        //{
        //    case GameState.StartingMenu:
        //        break;
        //    case GameState.GameOverMenu:
        //        break;
        //    case GameState.SkillTreeMenu:
        //        break;
        //    case GameState.WinMenu:
        //        break;
        //    case GameState.Playing:
        //        break;
        //}
    }

    public void SetMenuSkillTree()
    {
        currentState = GameState.SkillTreeMenu;
    }

    public void SetMenuGameOver()
    {
        currentState = GameState.GameOverMenu;
    }

    public void SetMenuMainMenu()
    {
        currentState = GameState.StartingMenu;
    }
}
