using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = System.Numerics.Vector3;

public class PlayerStats : MonoBehaviour
{
    private CharacterData characterData;
    public CharacterData.Stats baseStats;
    [SerializeField] private CharacterData.Stats actialStats;
    private HUD hud;
    private float health;

    public ParticleSystem damageEffect;

    [Header("Experience/Level")] 
    public int experience = 0;
    public int level = 0;
    public int experienceCap;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    [Header("I-Frames")] 
    public float invincibilityDuration;
    private float invincibilityTimer;
    private bool isInvincible;

    public List<LevelRange> levelRanges;

    private PlayerInventory inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    [Header("UI")]
    public Image healthBar;
    public Image expBar;
    public TMP_Text levelText;

    private void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();
        inventory = GetComponent<PlayerInventory>();

        baseStats = actialStats = characterData.stats;
        health = actialStats.maxHealth;
    }

    private void Start()
    {
        inventory.Add(characterData.StartingWeapon);
        experienceCap = levelRanges[0].experienceCapIncrease;
        
        
    }
}



