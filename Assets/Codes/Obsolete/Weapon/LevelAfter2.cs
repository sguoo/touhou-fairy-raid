using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAfter2 : MonoBehaviour
{
    void Start()
    {
        transform.parent = GameManager.instance.Player.transform;
        transform.position = GameManager.instance.Player.transform.position;
    }
}
