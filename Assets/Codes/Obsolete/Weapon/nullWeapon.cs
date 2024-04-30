using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class nullWeapon : MonoBehaviour
{
    
    
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;
    public int direction;
    private float timer;
    public Player player;
    private Item itemer;
    

    private LineRenderer lineRenderer;

    private void Awake()
    {
        player = GameManager.instance.Player;
    }

    private void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }


    void Update()
    {
        switch (id)
        {
            case 0 :
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                Batch();
                break;
            case 2:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    LaserBeam();
                }
                break;
            case 3 :
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Batch();
                }
                break;
            default:    
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }
        if (Input.GetKey("right")) // 오른쪽 키가 눌렸을때.
        {
            direction = 0;
        }
        else if (Input.GetKey("left")) //  왼쪽 키가 눌렸을때.
        {
            direction = 1;

        }
        else if (Input.GetKey("up")) // 위 키가 눌렸을때.
        {
            direction = 2;
        }
        else if (Input.GetKey("down")) // 아랫 키가 눌렸을때.
        {
            direction = 3;

        }
        if (Input.GetKey("right") && Input.GetKey("up"))
        {
            direction = 4;

        }
        if (Input.GetKey("left") && Input.GetKey("up"))
        {
            direction = 5;

        }
        if (Input.GetKey("right") && Input.GetKey("down"))
        {
            direction = 6;

        }
        if (Input.GetKey("left") && Input.GetKey("down"))
        {
            direction = 7;
  
        }
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(2, 0);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;
        
        if (id == 0)
            Batch();
    }

    
    public void Batch()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }
            
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;
            
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 = 무한 per
            
            
            
        }
    }

    void LaserBeam()
    {
        GetComponent<Rigidbody2D>();
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up,new Vector3());
        Vector3 dire = default;
        switch (direction)
        {
            
            case 0:
                bullet.rotation = Quaternion.Euler(0f, 0f, 90f);
                dire = new Vector3(4.5f,0,0);
                break;
            case 1:
                bullet.rotation = Quaternion.Euler(0f, 0f, 270f);
                dire = new Vector3(-4.5f,0,0);
                break;
            case 2:
                bullet.rotation = Quaternion.Euler(0f, 0f, 0f);
                dire = new Vector3(0,4.5f,0);
                break;
            case 3:
                bullet.rotation = Quaternion.Euler(0f, 0f, 180f);
                dire = new Vector3(0,-4.5f,180);
                break;
            


        }
        bullet.GetComponent<Bullet>().Init(damage, count, dire);
        bullet.position = player.transform.position + dire;
    }


    void Fire()
    {
        GetComponent<Rigidbody2D>();
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        Vector3 dir = default;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up,new Vector3());
        switch (direction)
        {
            
            case 0:
                bullet.rotation = Quaternion.Euler(0f, 0f, 90f);
                dir = new Vector3(1,0,90);
                break;
            case 1:
                bullet.rotation = Quaternion.Euler(0f, 0f, -90f);
                dir = new Vector3(-1,0,-90);
                break;
            case 2:
                bullet.rotation = Quaternion.Euler(0f, 0f, 0f);
                dir = new Vector3(0,1,0);
                break;
            case 3:
               bullet.rotation = Quaternion.Euler(0f, 0f, 180f);
                dir = new Vector3(0,-1,180);
                break;
            case 4:
                bullet.rotation = Quaternion.Euler(0f, 0f, -45f);
                dir = new Vector3(1,1,-45);
                break;
            case 5:
                bullet.rotation = Quaternion.Euler(0f, 0f, 45f);
                dir = new Vector3(-1,1,45);
                break;
            case 6:
                bullet.rotation = Quaternion.Euler(0f, 0f, -135f);
                dir = new Vector3(1,-1,90);
                break;
            case 7:
                bullet.rotation = Quaternion.Euler(0f, 0f, 135f);
                dir = new Vector3(-1,-1,90);
                break;

        }
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }   
}

