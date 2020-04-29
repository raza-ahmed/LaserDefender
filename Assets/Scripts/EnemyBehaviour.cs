﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float health = 150f;
    public GameObject projectile;
    public float projectileSpeed;
    public float shotsPerSecond = 0.5f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
           
        }
    }

    void Update()
    {
        float probability = Time.deltaTime * shotsPerSecond;
        if(Random.value< probability){
            FireProjectile();
        }
        
    }

        void FireProjectile()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject enemyBeam = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        enemyBeam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
    }

}
