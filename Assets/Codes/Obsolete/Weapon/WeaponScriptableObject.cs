using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Obsolete("This will be replaced by the WeaponData class")]
[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObject/Weapon")]
public class WeaponScriptable : ScriptableObject
{
    [SerializeField]
    private GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }
    
    [SerializeField]
    private float damage;
    public float Damage { get => damage; private set => damage = value; }
    
    [SerializeField]
    private float speed;
    public float Speed { get => speed; private set => speed = value; }
    
    [SerializeField]
    private float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }
    
    [SerializeField]
    private int pierce;
    public int Pierce { get => pierce; private set => pierce = value; }

    [SerializeField]
    private int level;
    public int Level { get => level; private set => level = value; }
    
    [SerializeField]
    private GameObject nextLevelPrefab;
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }
    
    [SerializeField]
    private TMP_Text name;
    public TMP_Text Name { get => name; private set => name = value; }
    
    [SerializeField]
    private TMP_Text description;
    public TMP_Text Description { get => description; private set => description = value; }
    
    [SerializeField]
    private Image icon;
    public Image Icon { get => icon; private set => icon = value; }
    
    [SerializeField]
    private TMP_Text evolvedUpgradeToRemove;
    public TMP_Text EvolvedUpgradeToRemove { get => evolvedUpgradeToRemove; private set => evolvedUpgradeToRemove = value; }
    

















}
