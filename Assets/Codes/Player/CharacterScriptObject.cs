using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "characterScriptableObject", menuName = "ScriptableObjects/Character")]
public class CharacterScriptObject : ScriptableObject
{
    [SerializeField] 
    public GameObject startingWeapon;
    public GameObject StartingWeapon
    {
        get => startingWeapon;
        private set => startingWeapon = value;
    }

    [SerializeField] 
    private float maxHealth;
    public float MaxHealth
    {
        get => maxHealth;
        private set => maxHealth = value;
    }

    [SerializeField]
    private float recovery;

    public float Recovery
    {
        get => recovery;
        set => recovery = value;
    }

    [SerializeField] 
    private float moveSpeed;
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }
    
    [SerializeField] 
    private float might;
    public float Might
    {
        get => might;
        set => might = value;
    }
        
    [SerializeField] 
    private float projectileSpeed;
    public float ProjectileSpeed
    {
        get => projectileSpeed;
        set => projectileSpeed = value;
    }
            
    [SerializeField] 
    private float magnet;
    public float Magnet
    {
        get => magnet;
        set => magnet = value;
    }

    [SerializeField] 
    public int Id;


}
