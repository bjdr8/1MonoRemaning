using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    [Header("Player info")]
    public GameObject player;
    public TextMeshProUGUI playerstatsTest;
    private PlayerProfile2 playerProfile;
    private BoxCollider2D playerCollider;

    [Header("Gun info")]
    public List<WeaponScript> weaponScripts;
    public List<GameObject> weapons;
    public List<Bullet> bulletList = new List<Bullet>();

    [Header("SkillTree info")] // saving and loading of the skilltree not implemented yet
    public ScriptableSkillNode rootNode;
    public RectTransform skilltreePanel;
    public GameObject skillButtonPrefab;
    private SkillTreeManager skillTreeManager;

    public List<GameObject> skillButtonList;

    private StatModifier statModifier = new StatModifier();

    private enum GameState
    {
        StartingMenu,
        GameOverMenu,
        SkillTreeMenu,
        WinMenu,
        Playing
    }
    private GameState currentState = GameState.StartingMenu;

    void Start()
    {
        playerProfile = new PlayerProfile2(player, weaponScripts);
        skillTreeManager = new SkillTreeManager(rootNode, skilltreePanel, skillButtonPrefab, this, playerProfile);
        //foreach (SkillNodeBase nodeBase in skillTreeManager.skillTreeNodeList)
        //{
        //    nodeBase.ImageChange(playerProfile.xp);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        playerstatsTest.text = "Player xp: " + playerProfile.xp + "\nPlayer movement speed: " + playerProfile.MovementSpeed + "\nPlayer Hp: " + playerProfile.maxHp;
        switch (currentState)
        {
            case GameState.StartingMenu:
                break;
            case GameState.GameOverMenu:
                break;
            case GameState.SkillTreeMenu:
                break;
            case GameState.WinMenu:
                break;
            case GameState.Playing:
                playerProfile.Update();
                break;
        }
    }

    public void SetMenuPlaying()
    {
        StartOfGame();
        currentState = GameState.Playing;
    }

    public void StartOfGame()
    {
        statModifier.ApplyStatMods(playerProfile);
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
