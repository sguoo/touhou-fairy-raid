using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    
    public Sprite icon;
    private Text textlevel;

    private void Awake()
    {
        switch (data.ItemType)
        {
            case ItemData.itemType.Melee:
                break;
            case ItemData.itemType.Range:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage = data.baseDamage * data. damages[level];
                    nextCount = data.counts[level];
                    weapon.LevelUp(nextDamage, nextCount);
                }
                break;
            case ItemData.itemType.Glove:
                break;
            case ItemData.itemType.Shoe:
                break;
            case ItemData.itemType.Heal:
                break;
            case ItemData.itemType.Laser:
                //weapon.Init(data);
                break;
        }

//        icon = GetComponentsInChildren<Sprite>()[0];
        icon = data.itemIcon;
        Text[] texts = GetComponentsInChildren<Text>();
       // textlevel = texts[0];
    }

    private void LateUpdate()
    {
   //     textlevel.text = ("Lv." +( level + 1));
    }

 
}
