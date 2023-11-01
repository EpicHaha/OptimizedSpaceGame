using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Damagable
{
    public Transform Player;
    public MeteorSpawnerMono Spawner;


    private void Start()
    {
        MoveTowardsPlayer();
    }



    void MoveTowardsPlayer()
    {
        GetComponent<Rigidbody2D>().AddForce( Player.position- transform.position ) ;

        Invoke("MoveTowardsPlayer", 1f);
    }


    private void OnDestroy()
    {
        Spawner.CurrentMeteorCount--;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerMono>() != null)
        {
            collision.gameObject.GetComponent<PlayerMono>().TakeDamage(20);
            Destroy(gameObject);
        }
    }
}
