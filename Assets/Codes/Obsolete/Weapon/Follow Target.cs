using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private nullWeapon _nullWeapon;
    private int direction;
    private Vector3 dire;
    private void Awake()
    {
        GetComponent<Rigidbody2D>();
        _nullWeapon = GetComponent<nullWeapon>();
        GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey("right")) // 오른쪽 키가 눌렸을때.
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            transform.position = GameManager.instance.Player.transform.position + new Vector3(4.5f,0f,0f);
        }
        else if (Input.GetKey("left")) //  왼쪽 키가 눌렸을때.
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            transform.position = GameManager.instance.Player.transform.position + new Vector3(-4.5f,0f,0f);

        }
        else if (Input.GetKey("up")) // 위 키가 눌렸을때.
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            transform.position = GameManager.instance.Player.transform.position + new Vector3(0f,4.5f,0f);
        }
        else if (Input.GetKey("down")) // 아랫 키가 눌렸을때.
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -180f);
            transform.position = GameManager.instance.Player.transform.position + new Vector3(0f,-4.5f,0f);
            
        }
        


    }
}
