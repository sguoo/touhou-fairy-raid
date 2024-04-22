using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InventoryManager : MonoBehaviour
{
    public List<Item> weaponsSlots = new List<Item>(6);
    public int[] weaponLevels = new int[6];
    public List<Image> weaponUISlots = new List<Image>(6);
    public List<Image> passiveItemUISlots = new List<Image>(6);
    public List<PassiveItem> passiveSlots = new List<PassiveItem>(6);
    public int[] PassiveItemLevels = new int[6];
    private Bullet bullet;

    [System.Serializable]
    public class WeaponUpgrade
    {
        public int weaponUpgradeIndex;
        public GameObject Initalweapon;
        public ItemData itemData;
    }

    [System.Serializable]
    public class PassiveItemUpgrade
    {
        public int passiveItemUpgradeIndex;
        public GameObject InitalPassiveItem;
        public ItemData PassiveItemData;
    }

    [System.Serializable]
    public class UpgradeUI
    {
        public TextMeshProUGUI upgradeNameDisplay;
        public TextMeshProUGUI upgradeDescriptionDisplay;
        public Image upgradeIcon;
        public Button upgradeButton;
    }
    
    public List<WeaponUpgrade> weaponUpgradesOptions = new List<WeaponUpgrade>();
    public List<PassiveItemUpgrade> passiveItemUpgradeOptions = new List<PassiveItemUpgrade>();
    public List<UpgradeUI> upgradeUiOptions = new List<UpgradeUI>();

    private PlayerStats player;

    private void Start()
    {
        bullet = GetComponent<Bullet>();
        player = GetComponent<PlayerStats>();
    }

    public void Addweapon(int slotIndex, Item item)
    {
        weaponsSlots[slotIndex] = item;
        weaponLevels[slotIndex] = item.data.Level;
        weaponUISlots[slotIndex].enabled = true;
        weaponUISlots[slotIndex].sprite = item.data.itemIcon;
        
        if (compositiveManager.instanc != null && compositiveManager.instanc.choosingUpgrade)
        {
            compositiveManager.instanc.EndLevelUp();
        } 
    }

    public void AddPassiveItem(int slotIndex, PassiveItem passiveItem)
    {
        passiveSlots[slotIndex] = passiveItem;
        PassiveItemLevels[slotIndex] = passiveItem.PassiveItemData.Level;
        passiveItemUISlots[slotIndex].enabled = true;
        passiveItemUISlots[slotIndex].sprite = passiveItem.PassiveItemData.Icon;
        if (compositiveManager.instanc != null && compositiveManager.instanc.choosingUpgrade)
        {
            compositiveManager.instanc.EndLevelUp();
        }
    }

    public void LevelUpWeapon(int slotIndex, int upgradeIndex)
    {
        if (weaponsSlots.Count > slotIndex)
        {
            Item weapon = weaponsSlots[slotIndex];
            if (!weapon.data.NextLevelPrefab)
            {
                Debug.LogError("다음 슬롯이 없습니다.");
                return;
            }

            GameObject upgradedWeapon =
                Instantiate(weapon.data.NextLevelPrefab, transform.position, Quaternion.identity);
            Addweapon(slotIndex, upgradedWeapon.GetComponent<Item>());
            weapon.gameObject.SetActive(false);
            weaponLevels[slotIndex] = upgradedWeapon.GetComponent<Item>().data.Level;

            weaponUpgradesOptions[upgradeIndex].itemData = upgradedWeapon.GetComponent<Item>().data;
            
            if (compositiveManager.instanc.choosingUpgrade)
            {
                compositiveManager.instanc.EndLevelUp();
            }
        }   
    }

    public void LevelUpPassiveItem(int slotIndex, int upgradeIndex)
    {
        if (passiveSlots.Count > slotIndex)
        {
            PassiveItem passiveItem = passiveSlots[slotIndex];
            if (!passiveItem.PassiveItemData.NextLevelPrefab)
            {
                Debug.LogError("다음 레벨이 없습니다.");
                return;
            }

            GameObject upgradedPassiveItem = Instantiate(passiveItem.PassiveItemData.NextLevelPrefab,
                transform.position, Quaternion.identity);
            upgradedPassiveItem.transform.SetParent(transform);
            AddPassiveItem(slotIndex, upgradedPassiveItem.GetComponent<PassiveItem>());
            passiveItem.gameObject.SetActive(false);
            PassiveItemLevels[slotIndex] = upgradedPassiveItem.GetComponent<PassiveItem>().PassiveItemData.Level;

            passiveItemUpgradeOptions[upgradeIndex].PassiveItemData =
                upgradedPassiveItem.GetComponent<PassiveItem>().PassiveItemData;
            
            if (compositiveManager.instanc != null && compositiveManager.instanc.choosingUpgrade)
            {
                compositiveManager.instanc.EndLevelUp();
            }
        }
    }

    void ApplyUpgradeOptions()
    {
        List<WeaponUpgrade> availableWeaponUpgrades = new List<WeaponUpgrade>(weaponUpgradesOptions);
        List<PassiveItemUpgrade> availablePassiveItemUpgrades = new List<PassiveItemUpgrade>(passiveItemUpgradeOptions);
        
        foreach (var upgradeOption in upgradeUiOptions)
        {
            if (availableWeaponUpgrades.Count == 0 && availablePassiveItemUpgrades.Count == 0)
            {
                return;
            }

            int upgradeType;

            if (availableWeaponUpgrades.Count == 0)
            {
                upgradeType = 2;
            }
            else if (availablePassiveItemUpgrades.Count == 0)
            {
                upgradeType = 1;
            }
            else
            {
                upgradeType = Random.Range(1, 3);
            }
            
            if (upgradeType == 1)
            {
                WeaponUpgrade chosenWeaponUpgrade = availableWeaponUpgrades[Random.Range(0, availableWeaponUpgrades.Count)];
                availableWeaponUpgrades.Remove(chosenWeaponUpgrade);
                if (chosenWeaponUpgrade != null)
                {
                    EnableUpgradeUI(upgradeOption);
                    
                    bool newWeapon = false;
                    for (int i = 0; i < weaponsSlots.Count; i++)
                    {
                        if (weaponsSlots[i] != null && weaponsSlots[i].data == chosenWeaponUpgrade.itemData)
                        {
                            newWeapon = false;
                            if (!newWeapon)
                            {
                                if (!chosenWeaponUpgrade.itemData.NextLevelPrefab)
                                {
                                    DisableUpgradeUI(upgradeOption);
                                    break;
                                }
                                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpWeapon(i, chosenWeaponUpgrade.weaponUpgradeIndex));
                                upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.itemData
                                    .NextLevelPrefab.GetComponent<Item>().data.Description;
                                upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.itemData.NextLevelPrefab
                                    .GetComponent<Item>().data.Name;
                            }
                            break;
                        }
                        else
                        {
                            newWeapon = true;
                        }
                    }

                    if (newWeapon)
                    {
                        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnWeapon(chosenWeaponUpgrade.Initalweapon));
                        upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.itemData.Description;
                        upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.itemData.Name;
                    }

                    upgradeOption.upgradeIcon.sprite = chosenWeaponUpgrade.itemData.Icon;
                }
            }
            else if (upgradeType == 2)
            {
                PassiveItemUpgrade chosenPassiveItemUpgrade =
                    availablePassiveItemUpgrades[Random.Range(0, availablePassiveItemUpgrades.Count)];
                availablePassiveItemUpgrades.Remove(chosenPassiveItemUpgrade);
                
                if (chosenPassiveItemUpgrade != null)
                {
                    EnableUpgradeUI(upgradeOption);
                    
                    bool newPassiveItem = false;
                    for (int i = 0; i < passiveSlots.Count; i++)
                    {
                        if (passiveSlots[i] != null &&
                            passiveSlots[i].PassiveItemData == chosenPassiveItemUpgrade.PassiveItemData)
                        {
                            newPassiveItem = false;

                            if (!newPassiveItem)
                            {
                                if (!chosenPassiveItemUpgrade.PassiveItemData.NextLevelPrefab)
                                {
                                    DisableUpgradeUI(upgradeOption);
                                    break;
                                }
                                upgradeOption.upgradeButton.onClick.AddListener(()=>LevelUpPassiveItem(i, chosenPassiveItemUpgrade.passiveItemUpgradeIndex));
                                upgradeOption.upgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.PassiveItemData
                                    .NextLevelPrefab.GetComponent<PassiveItem>().PassiveItemData.Description;
                                upgradeOption.upgradeNameDisplay.text = chosenPassiveItemUpgrade.PassiveItemData
                                    .NextLevelPrefab.GetComponent<PassiveItem>().PassiveItemData.Name;
                            }

                            break;
                        }
                        else
                        {
                            newPassiveItem = true;
                        }
                    }

                    if (newPassiveItem == true)
                    {
                        upgradeOption.upgradeButton.onClick.AddListener(() =>
                            player.SpawnPassiveItem(chosenPassiveItemUpgrade.InitalPassiveItem));
                        upgradeOption.upgradeDescriptionDisplay.text =
                            chosenPassiveItemUpgrade.PassiveItemData.Description;
                        upgradeOption.upgradeNameDisplay.text = chosenPassiveItemUpgrade.PassiveItemData.Name;
                    }

                    upgradeOption.upgradeIcon.sprite = chosenPassiveItemUpgrade.PassiveItemData.Icon;
                }
            }

        }
    }
    void RemoveUpgradeOptions() 
    {
        foreach (var upgradeOptions in upgradeUiOptions)
        {
            upgradeOptions.upgradeButton.onClick.RemoveAllListeners();
            DisableUpgradeUI(upgradeOptions);
        }
    }
    public void RemoveAndApplyUpgrades()
    {
        RemoveUpgradeOptions();
        ApplyUpgradeOptions();
    }

    void DisableUpgradeUI(UpgradeUI ui)
    {
        ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(false);
    }

    void EnableUpgradeUI(UpgradeUI ui)
    {
        ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(true);
    }
}
