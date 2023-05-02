using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileHit : MonoBehaviour
{
    public float projectileDamage, explosiveRange, explosionDamage;
    public bool explosiveShot, playerShot, enemyShot;
    public LayerMask layersToHit;

    public GameObject explosionSphere;

    private void Awake()
    {
        Destroy(gameObject, 10.0f);
    }

    private void OnCollisionEnter(Collision other)
    {
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