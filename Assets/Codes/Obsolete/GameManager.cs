using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public CharacterData characterData;
    public static GameManager instance;
    public GameObject power;
    public float gameTime;
    public float maxGameTime = 2 * 60f;
    [Header("#Player Info")] public float maxhealth;
    public float health;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    public float recovery;
    public float might;
    public float magnet;
    [Header("# Game Object")]
    public PoolManager pool;
    public Player Player;
    
    

    
    void Awake()
    {
        instance = this;
        recovery = characterData.stats.recovery;
        might = characterData.stats.might;
        magnet = characterData.stats.magnet;
        maxhealth = characterData.stats.maxHealth;
        health = maxhealth;
        
    }
    
    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp(int expp)
    {
        exp += expp;

        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
            compositiveManager.instanc.StartLevelUp();
        }
    }

    
}
