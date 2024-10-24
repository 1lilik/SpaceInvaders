using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Missile : Projectile
{
    public GameObject particles;
    private void Awake()
    {
        direction = Vector3.down;


    }

    void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject); //s� fort den krockar med n�got s� ska den f�rsvinna.
    }

}