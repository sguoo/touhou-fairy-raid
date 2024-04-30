using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Obsolete("This has been replaced by CharacterData")]
[CreateAssetMenu(fileName = "characterScriptableObject", menuName = "ScriptableObjects/Character")]
public class CharacterScriptObject : ScriptableObject
{
   [SerializeField] 
   private GameObject startingWeapon;
   public GameObject StartingWeapon {get => startingWeapon; private set => startingWeapon = value;}

   [SerializeField] 
   private float maxHealth;
   public float MaxHealth { get => maxHealth; private set => maxHealth = value;}
   
   [SerializeField]
   private float recovery;
   public float Recovery { get => recovery; private set => recovery = value; }
   [SerializeField]
   private float moveSpeed;
   public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value;}
   [SerializeField]
   float might;
   public float Might { get => might; private set => might = value;}
   [SerializeField]
   float projectile;
   public float Projectile { get => projectile; private set => projectile = value;}
   [SerializeField]
   float magnet;
   public float Magnet { get => magnet; private set => magnet = value;}
}
