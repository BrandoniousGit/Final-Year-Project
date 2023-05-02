using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    public float projectileDamage, explosiveRange, explosionDamage, projectileSize;
    public bool explosiveShot, playerShot, enemyShot;
    public LayerMask layersToHit, layersToDamage;

    public GameObject explosionSphere;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, 10.0f);
    }

    private void Update()
    {
        if (explosiveShot)
        {
            if (Physics.CheckSphere(transform.position, projectileSize, layersToHit, QueryTriggerInteraction.Ignore))
            {
                GameObject explosionClone = Instantiate(explosionSphere, transform.position, transform.rotation);
                explosionClone.transform.localScale = new Vector3(explosiveRange, explosiveRange, explosiveRange);

                if (Physics.CheckSphere(transform.position, explosiveRange, layersToDamage, QueryTriggerInteraction.Ignore))
                {
                    player.gameObject.GetComponent<PlayerMove>().TakeDamage(explosionDamage);
                }
                Destroy(gameObject);
            }
        }

        else if (Physics.CheckSphere(transform.position, projectileSize, layersToDamage, QueryTriggerInteraction.Ignore))
        {
            player.gameObject.GetComponent<PlayerMove>().TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }
}