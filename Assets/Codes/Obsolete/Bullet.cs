using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    Rigidbody2D rigid;
    float timer;
    
    
    
    
    private void Awake()
    {
        timer = 0f;
        rigid = GetComponent<Rigidbody2D>();
    }
    
    
    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;
        if (per > -1)
        {
            rigid.velocity = dir * 15f;
        }
    }

    private void Update()
    {
        Dead();
    }

    void Dead()
    {
        if (per >= 0)
        {
            Transform target = GameManager.instance.Player.transform;
            Vector3 targetPos = target.position;
            float dir = Vector3.Distance(targetPos, transform.position);
            if (dir > 20f)
                this.gameObject.SetActive(false);

        }
        else if (per == -2)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer > 0.2f)
            {
                timer = 0;
                this.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collution)
    {
        if (!collution.CompareTag("Enemy") || per <= -1)
            return;
        
        per--;
        
        if (per == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
