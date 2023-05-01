using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    public float projectileDamage, explosiveRange, explosionDamage;
    public bool explosiveShot, playerShot, enemyShot;
    public LayerMask layersToHit;

    public GameObject explosionSphere;

    private bool hasHit;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter(Collision other)
    {
        //For when the enemy shoots a projectile
        if (enemyShot)
        {
            if (explosiveShot)
            {
                GameObject explosionClone = Instantiate(explosionSphere, transform.position, transform.rotation);
                explosionClone.transform.localScale = new Vector3(explosiveRange, explosiveRange, explosiveRange);

                if (Physics.CheckSphere(transform.position, explosiveRange, layersToHit, QueryTriggerInteraction.Ignore))
                {
                    player.gameObject.GetComponent<PlayerMove>().TakeDamage(explosionDamage);
                }
            }

            if (other.gameObject.tag == "Player")
            {
                player.gameObject.GetComponent<PlayerMove>().TakeDamage(projectileDamage);
            }
        }

        //For when the player shoots a projectile
        if (playerShot)
        {
            if (explosiveShot)
            {
                GameObject explosionClone = Instantiate(explosionSphere, transform.position, transform.rotation);
                explosionClone.transform.localScale = new Vector3(explosiveRange, explosiveRange, explosiveRange);

                if (Physics.CheckSphere(transform.position, explosiveRange, layersToHit, QueryTriggerInteraction.Ignore))
                {
                    Collider[] enemiesHit = Physics.OverlapSphere(transform.position, explosiveRange, layersToHit, QueryTriggerInteraction.Ignore);

                    for (int i = 0; i < enemiesHit.Length; i++)
                    {
                        if (enemiesHit[i].GetComponent<EnemyAgent>() != null)
                        {
                            enemiesHit[i].gameObject.GetComponent<EnemyAgent>().TakeDamage(explosionDamage);
                        }
                    }
                }
            }

            else if (!explosiveShot && other.gameObject.GetComponentInChildren<EnemyAgent>() != null)
            {
                other.gameObject.GetComponentInChildren<EnemyAgent>().TakeDamage(projectileDamage);
            }
        }

        Destroy(gameObject);
    }
}