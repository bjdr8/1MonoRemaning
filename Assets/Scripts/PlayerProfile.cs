using System;
using UnityEngine;

public class PlayerProfile2
{
    public int maxHp = 6;
    public int hp;
    public int hpRegen = 0;
    public float hpRegenTimer = 10;
    public float hpRegenSetTimer = 60;

    public event Action<int> OnXpChanged;
    public int xp { get; private set; } = 500;
    private int profileLevel;
    public void AddXp(int xp)
    {
        this.xp += xp;
        OnXpChanged.Invoke(this.xp);
        Debug.Log(this.xp);
    }

    public void RemoveXp(int xp)
    {
        this.xp -= xp;
        OnXpChanged.Invoke(this.xp);
        Debug.Log(this.xp);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
