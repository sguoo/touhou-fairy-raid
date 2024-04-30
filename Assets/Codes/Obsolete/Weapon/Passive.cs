using Unity;
using Unity.VisualScripting;
using UnityEngine;

public class Passive : Item
{
    public PassiveData data;
    [SerializeField] private CharacterData.Stats currentBoosts;
    
    [System.Serializable]
    public struct Modifier
    {
        public string name, description;
        public CharacterData.Stats boosts;
    }

    public virtual void Initalise(PassiveData data)
    {
     ((Item)this).Initialise(data);
     this.data = data;
     currentBoosts = data.baseStats.boosts;
    }

    public virtual CharacterData.Stats GetBoosts()
    {
        return currentBoosts;
    }

    public override bool DoLevelUp()
    {
        base.DoLevelUp();

        if (!CanLevelUp())
        {
            Debug.LogWarning(string.Format("Cannot level up {0} to Level {1}, max level of {2} aleady reached", name, currentLevel, data.maxLevel));
            return false;
        }

        currentBoosts += data.GetLevelData(++currentLevel).boosts;
        return true;
    }  
}