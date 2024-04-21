using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptObject characterData;
    
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    private float currentRecovery;
    [HideInInspector]
    private float currentMoveSpeed;
    [HideInInspector]
    private float currentMight;
    [HideInInspector]
    private float currentProjectileSpeed;
    [HideInInspector] 
    public float currentMagnet;

    public float CurrentHealth
    {
        get { return currentHealth;}
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
            }
        }
    }

    public List<GameObject> spawnedWeapons;

    [Header("I-Frames")] 
    public float invincibilityDuration;
    private float invincibilityTimer;
    private bool isInvincible;

    private PlayerCollector collector;
    private InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;
    
    private void Awake()
    {
        characterData = CharacterSelector.GetData();
        if(CharacterSelector.chara)
            CharacterSelector.chara.DestroySingleton();
        collector = GetComponent<PlayerCollector>();
        inventory = GetComponent<InventoryManager>();
        
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMagnet = characterData.Magnet;
        SpawnWeapon(characterData.StartingWeapon);
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

    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
         GameManager.instance.health -= dmg;
        invincibilityTimer = invincibilityDuration;
        isInvincible = true;
        
        }
        if (GameManager.instance.health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        if (!compositiveManager.instanc.isGameOver)
        {
            compositiveManager.instanc.ChangeState(compositiveManager.GameState.GameOver);
        }
    }

    public void RestoreHealth(float amount)
    {
        if (GameManager.instance.health < characterData.MaxHealth)
        {
            GameManager.instance.health += amount;
            if (GameManager.instance.health > characterData.MaxHealth)
                GameManager.instance.health = characterData.MaxHealth;
        }
    }

    void Recover()
    {
        if (GameManager.instance.health < characterData.MaxHealth)
        {
            GameManager.instance.health += currentRecovery * Time.deltaTime;
            if (GameManager.instance.health > characterData.MaxHealth)
            {
                GameManager.instance.health = characterData.MaxHealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if (weaponIndex >= inventory.weaponsSlots.Count -1)
        {
            Debug.LogError("인벤토리 슬롯은 이미 꽉 차있다");
            return;
        }
        
        GameObject spawnWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnWeapon.transform.SetParent(transform);
        inventory.Addweapon(weaponIndex, spawnWeapon.GetComponent<Item>());

        weaponIndex++;
    }  
    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (passiveItemIndex >= inventory.passiveSlots.Count -1)
        {
            Debug.LogError("인벤토리 슬롯은 이미 꽉 차있다");
            return;
        }
            
        GameObject spawnPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnPassiveItem.GetComponent<PassiveItem>());
 
        passiveItemIndex++;
    }
    
}
