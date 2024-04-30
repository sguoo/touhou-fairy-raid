using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;

    Rigidbody2D rigid;
    public Scaner scanner;
    SpriteRenderer spriter;
    public float speed;
    Animator anim;
    public CharacterData characterData;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scaner>();
        characterData = CharacterSelector.GetData();
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = characterData.stats.moveSpeed;
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical"); 
    }

    void FixedUpdate()
    {
        //anim.SetInteger("Id", characterData.);
        Vector2 nextVec = inputVec.normalized * (speed * Time.fixedDeltaTime);
        rigid.MovePosition(rigid.position + nextVec);
    }

    void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);
        
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        };
    }
}
    