using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectTIle : WeaponEffect
{
    public enum DamageSource { projectile, owner };
    public DamageSource damageSource = DamageSource.projectile;
    public bool hasAutoAim = false;
    public Vector3 rotationSpeed = new Vector3(0, 0, 0);

    protected Rigidbody2D rb;
    protected int piercing;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Item.Stats stats = weapon.GetStats();
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.angularVelocity = rotationSpeed.z;
            rb.velocity = transform.right * stats.area;
        }

        float area = stats.area == 0 ? 1 : stats.area;
        transform.localScale = new Vector3(area * Math.Sign(transform.localScale.x),
            area * Math.Sign(transform.localScale.y), 1);
        piercing = stats.piercing;
        if (stats.lifespan > 0) Destroy(gameObject, stats.lifespan);
        if (hasAutoAim) AcquireAutoAimFancing();
    }

    public virtual void AcquireAutoAimFancing()
    {
        float aimAngle;

        EnemyStats targets = FindObjectOfType<EnemyStats>();
    }
}
