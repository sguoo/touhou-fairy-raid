using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    public ItemData.itemType type;
    public float rate;
    public ItemData PassiveItemData;

    private void Start()
    {
        Init(PassiveItemData);
    }

    public void Init(ItemData data)
    {
        name = "Gear" + data.itemId;
        transform.parent = GameManager.instance.Player.transform;
        transform.localPosition = new Vector3(0, 0, 0);
    
        type = data.ItemType;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.itemType.Shoe:
                SpeedUp();
                break;
        }
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void SpeedUp()
    {
        
        GameManager.instance.Player.speed += rate;
    }
}
