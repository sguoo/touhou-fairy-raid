using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ItemData
{
    
    [HideInInspector] public string behavior;
    public Weapon.Stats baseStats;
    public Weapon.Stats[] linearGrowth;
    public Weapon.Stats[] randomGrowth;

    public Weapon.Stats GetLevelData(int level)
    {
        if (level - 2 < linearGrowth.Length)
            return linearGrowth[level - 2];
        if (randomGrowth.Length > 0)
            return randomGrowth[Random.Range(0, randomGrowth.Length)];
        Debug.LogWarning(string.Format("Weapon doesn't have its level up stats configured for Level {0}!", level));
        return new Weapon.Stats();
    }

}

