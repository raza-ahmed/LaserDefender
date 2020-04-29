﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 15.0f;
    public float padding = 0.5f;
    public GameObject projectile;
    public float projectileSpeed;
    public float firingRate = 0.2f;
    public float health = 250f;


    float xmin;
    float xmax;

    // Start is called before the first frame update
    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            InvokeRepeating("FireProjectile", 0.000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space)){
            CancelInvoke("FireProjectile");
        }

            if (Input.GetKey(KeyCode.LeftArrow)){
            transform.position += Vector3.left * speed * Time.deltaTime;
           
        }
        else  if (Input.GetKey(KeyCode.RightArrow)){
            transform.position += Vector3.right * speed * Time.deltaTime;
           
        }
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void FireProjectile()
    {
        Vector3 startPositionBeam = transform.position + new Vector3(0, 1, 0);
        GameObject beam = Instantiate(projectile, startPositionBeam, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
    }

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
}
