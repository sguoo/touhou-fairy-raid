using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Pickup : MonoBehaviour
{
    

    public float lifespan = 0.5f;
    protected PlayerStats target;
    protected float speed;
    public int exp;
    public GameObject DropExp;

    [Header("Bounses")] 
    public int experience;
    public int health;
    private void Awake()
    {
        DropExp = GameObject.Find("ExpManager");
        transform.parent = DropExp.transform;
    }


    protected virtual void Update()
    {
        if (target)
        {
            Vector2 distance = target.transform.position - transform.position;
            if (distance.sqrMagnitude > speed * speed * Time.deltaTime)
                transform.position += (Vector3)distance.normalized * speed * Time.deltaTime;
            else
                Destroy(gameObject);
        }
    }

    public virtual bool Collect(PlayerStats target, float speed, float lifespan = 0f)
    {
        GameManager.instance.GetExp(exp);
        if (!this.target)
        {
            this.target = target;
            this.speed = speed;
            if (lifespan > 0) this.lifespan = lifespan;
            Destroy(gameObject, Mathf.Max(0.01f, this.lifespan));
            return true;
        }

        return false;
    }

    protected virtual void OnDestroy()
    {
        if (!target) return;
        if(health != 0) target.RestoreHealth(health);
    }
}
