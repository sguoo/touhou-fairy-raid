using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private GameObject target;
    private Weapon funtion;

    private void Awake()
    {
        funtion = GetComponent<Weapon>();
        
    }

    void Update()
    {
        

        Transform bullet = GameManager.instance.pool.Get(funtion.prefabId).transform;
        Vector3 dir = default;
        bullet.position = transform.position;
        switch (funtion.direction)
        {

            case 0:
                bullet.rotation = Quaternion.Euler(0f, 0f, 90f);
                dir = new Vector3(5f, 0, 90);
                break;
            case 1:
                bullet.rotation = Quaternion.Euler(0f, 0f, -90f);
                dir = new Vector3(-5f, 0, -90);
                break;
            case 2:
                bullet.rotation = Quaternion.Euler(0f, 0f, 0f);
                dir = new Vector3(0, -1f, 0);
                break;
            case 3:
                bullet.rotation = Quaternion.Euler(0f, 0f, 180f);
                dir = new Vector3(0, -1f, 180);
                break; 
        }
        bullet.GetComponent<Bullet>().Init(funtion.damage, funtion.count, dir);
    }
}
