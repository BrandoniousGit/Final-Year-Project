using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    public float projectileDamage, explosiveRange, explosionDamage;
    public bool explosiveShot;
    public LayerMask playerLayer;

    public GameObject explosionSphere;

    private bool hasHit;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (explosiveShot)
        {
            GameObject explosionClone = Instantiate(explosionSphere, transform.position, transform.rotation);
            explosionClone.transform.localScale = new Vector3(explosiveRange, explosiveRange, explosiveRange);

            if (Physics.CheckSphere(transform.position, explosiveRange, playerLayer, QueryTriggerInteraction.Ignore))
            {
                player.gameObject.GetComponent<PlayerMove>().TakeDamage(explosionDamage);
            }
        }

        if (other.gameObject.tag == "Player")
        {
            player.gameObject.GetComponent<PlayerMove>().TakeDamage(projectileDamage);
        }

        Destroy(gameObject);
    }
}