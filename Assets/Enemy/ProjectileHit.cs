using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    public float projectileDamage, explosiveRange, explosionDamage;
    public bool explosiveShot;
    public LayerMask playerLayer;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (explosiveShot)
        {
            if (Physics.CheckSphere(transform.position, explosiveRange, playerLayer, QueryTriggerInteraction.Ignore))
            {
                player.gameObject.GetComponent<PlayerMove>().TakeDamage(explosionDamage);
            }
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            player.gameObject.GetComponent<PlayerMove>().TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }
}