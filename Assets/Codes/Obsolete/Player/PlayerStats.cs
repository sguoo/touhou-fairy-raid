using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vector3 = System.Numerics.Vector3;

public class PlayerStats : MonoBehaviour
{
    private CharacterData characterData;
    public CharacterData.Stats baseStats;
    [SerializeField] private CharacterData.Stats actialStats;
    private HUD hud;
    private float health;

    

    #region Current Stats Propertis

    

    public float CurrentHealth
    {
        get { return health; }
        set
        {
            if (health != value)
            {
                health = value;
                if (compositiveManager.instanc != null)
                {
                    compositiveManager.instanc.currentHealthDisplay.text = string.Format("Health: {0} / {1}",
                        health, actialStats.maxHealth
                        );
                }
                
            }
        }
    }
    
    
    public float MaxHealth
    {
        get { return actialStats.maxHealth; }
        set
        {
            if (actialStats.maxHealth != value)
            {
                actialStats.maxHealth = value;
                if (compositiveManager.instanc != null)
                {
                    compositiveManager.instanc.currentHealthDisplay.text = string.Format("Health: {0} / {1}",
                        health, actialStats.maxHealth
                        );
                }
                
            }
        }
    }

    public float CurrentRecovery
    {
        get { return Recovery; }
        set { Recovery = value; }
    }
    
    public float Recovery
    {
        get { return actialStats.recovery; }
        set
        {
            if (actialStats.recovery != value)
            {
                actialStats.recovery = value;
                if (compositiveManager.instanc != null)
                {
                    compositiveManager.instanc.currentHealthDisplay.text = "Recovery: " + actialStats.recovery;
                }
                
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get { return MoveSpeed; }
        set { MoveSpeed = value; }
    }

    public float MoveSpeed
    {
        get { return actialStats.moveSpeed; }
        set
        {
            if (actialStats.moveSpeed != value)
            {
                actialStats.moveSpeed = value;
                if (compositiveManager.instanc != null)
                {
                    compositiveManager.instanc.currentMoveSpeedDisplay.text = "MoveSpeed: " + actialStats.moveSpeed;
                }
            }
        }
    }

    public float CurrentMight
    {
        get { return Might; }
        set { Might = value; }
    }

    public float Might
    {
        get { return actialStats.might; }
        set
        {
            if (actialStats.might != value)
            {
                actialStats.might = value;
                if (compositiveManager.instanc != null)
                {
                    compositiveManager.instanc.currentMightDisplay.text = "Might: " + actialStats.might;
                }
            }
        }
    }

    public float CurrentProjcetileSpeed
    {
        get { return Speed; }
        set { Speed = value; }
    }

    public float Speed
    {
        get { return actialStats.speed; }
        set
        {
            if (actialStats.speed != value)
            {
                actialStats.speed = value;
                if (compositiveManager.instanc != null)
                {
                    compositiveManager.instanc.currentProjectileSpeedDisplay.text =
                        "Projectile Speed: " + actialStats.speed;
                }
            }
        }
    }

    public float CurrentMagnet
    {
        get { return Magnet; }
        set { Magnet = value; }
    }

    public float Magnet
    {
        get { return actialStats.magnet; }
        set
        {
            if (actialStats.magnet != value)
            {
                actialStats.magnet = value;
                if (compositiveManager.instanc != null)
                {
                    compositiveManager.instanc.currentMagnetDisplay.text = "Magnet: " + actialStats.magnet;
                }
            }
        }
    }   
    
    #endregion
    
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
        compositiveManager.instanc.currentHealthDisplay.text = "Heath: " + CurrentHealth;
        compositiveManager.instanc.currentRecoveryDisplay.text = "Recovery: " + CurrentRecovery;
        compositiveManager.instanc.currentMoveSpeedDisplay.text = "Move Speed: " + CurrentMoveSpeed;
        compositiveManager.instanc.currentMightDisplay.text = "Might: " + Might;
        compositiveManager.instanc.currentProjectileSpeedDisplay.text = "Projectile Speed: " + CurrentProjcetileSpeed;
        compositiveManager.instanc.currentMagnetDisplay.text = "Magnet: " + CurrentMagnet;
        
        compositiveManager.instanc.AssignChosenCharacterUI(characterData);

        UpdateHealthBar();
        UpdateExpBar();
        UpdateLevelText();

    }
    void Update() 
    { 
        if (invincibilityTimer > 0) 
        { 
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }
        Recover();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpCheker();
        UpdateExpBar();
    }   

    void LevelUpCheker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;
            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }

            experienceCap += experienceCapIncrease;
            UpdateLevelText();
            compositiveManager.instanc.StartLevelUp();
        }
    }

    void UpdateExpBar()
    {
        expBar.fillAmount = (float)experience / experienceCap;
    }

    void UpdateLevelText()
    {
        levelText.text = "LV " + level.ToString();
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            CurrentHealth -= dmg;
            if(damageEffect) Destroy(Instantiate(damageEffect, transform.position, Quaternion.identity), 5f);
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            if (CurrentHealth <= 0)
            {
                Kill();
            }

            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = CurrentHealth / actialStats.maxHealth;
    }

    public void Kill()
    {
        if (!compositiveManager.instanc.isGameOver)
        {
            compositiveManager.instanc.AssignLevelReachedUI(level);
            compositiveManager.instanc.GameOver();
        }
    }

    public void RestoreHealth(float amount)
    {
        if (CurrentHealth < actialStats.maxHealth)
        {
            CurrentHealth += amount;
            if (CurrentHealth > actialStats.maxHealth)
            {
                CurrentHealth = actialStats.maxHealth;
            }
        }
    }

    void Recover()
    {
        if (CurrentHealth < actialStats.maxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;
            CurrentHealth += Recovery * Time.deltaTime;

            if (CurrentHealth > actialStats.maxHealth)
            {
                CurrentHealth = actialStats.maxHealth;
            }
        }
    }

    [System.Obsolete(
        "Old function that is kept to maintain compatibility with the InventoryManager. Will be removed soon.")]
    public void SpawnWeapon(GameObject weapon)
    {
        if (weaponIndex >= inventory.weaponSlots.Count - 1)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }

        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
    }

    [System.Obsolete("No need to spawn passive items directly now.")]
    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (passiveItemIndex >= inventory.passiveSlots.Count - 1)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }

        GameObject spawnPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnPassiveItem.transform.SetParent(transform);

        passiveItemIndex++;
    }
    
}



