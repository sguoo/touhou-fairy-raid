using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "item", menuName = "ScriptableObjects/itemData")]
public class ItemData : ScriptableObject
{
    public enum itemType {Melee, Range, Glove, Shoe, Heal, Laser }

    [Header("# Main Info")] 
    public itemType ItemType;
    public int itemId;

    [TextArea]
    public Sprite itemIcon;
    
    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")] 
    public GameObject projectile;

    [SerializeField] 
    int level;
    public int Level { get => level;
        set => level = value; }

    [SerializeField]
    private GameObject nextLevelPrefab;
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value;}
            
    [SerializeField] 
    Sprite icon;
    public Sprite Icon { get => icon; private set => icon = value; }

    [SerializeField]
    private new string name;
    public string Name { get => name; private set => name = value;}

    [SerializeField] 
    private string description;
    public string Description { get => description; private set => Description = value;}
}

