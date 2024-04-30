using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private PlayerStats player;
    private CircleCollider2D detector;
    public float pullSpeed;
    
    private void Start()
    {
        player = GetComponentInParent<PlayerStats>();
    }

    private void Update()
    {
        SetRadius(player.currentMagnet);
    }

    public void SetRadius(float r)
    {
        if (!detector) detector = GetComponent<CircleCollider2D>();
        detector.radius = r;
    }
    

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Pickup p))
        {
            p.Collect(player, pullSpeed);
        }
    }
}
