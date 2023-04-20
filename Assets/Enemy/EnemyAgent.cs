using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyAgent : MonoBehaviour
{
    public EnemyData enemyData;
    public Image _healthBarImage;
    public TextMeshProUGUI _healthBarValue;
    public GameObject enemyCanvas;

    private GameObject player;
    public NavMeshAgent agent;

    public Vector3 heightOffset;
    public float m_currentHP;
    public bool m_alive;

    //Attacking
    public float attackRange;
    public bool playerInAttackRange;

    public float sightRange;
    public bool playerInSightRange;

    public float timeBetweenAttacks;
    public bool canAttack;
    public float shotForce;

    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Awake()
    {
        m_alive = true;
        m_currentHP = enemyData.m_maxHP;
        _healthBarImage.fillAmount = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        OnSpawnEvents();
        Invoke("GracePeriod", 1.0f);
    }

    void GracePeriod()
    {
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_alive)
        {
            return;
        }

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer, QueryTriggerInteraction.Ignore);
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer, QueryTriggerInteraction.Ignore);

        if (!playerInAttackRange && !playerInSightRange)
        {
            agent.SetDestination(transform.position);
        }

        if (!playerInAttackRange && playerInSightRange)
        {
            GetInRangePlayer();
        }

        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }

        UpdateHealthBar();
        enemyCanvas.transform.LookAt(Camera.main.transform.position);
        if (m_currentHP <= 0)
        {
            OnDeadEvents();
            gameObject.GetComponent<Rigidbody>().freezeRotation = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            enemyCanvas.SetActive(false);
            m_alive = false;
            StartCoroutine("EnemyDie");
        }
    }

    public void GetInRangePlayer()
    {
        agent.SetDestination(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
    }

    public void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        if (!canAttack)
        {
            return;
        }

        switch (enemyData.m_enemyId)
        {
            case 0:
                //Grunk attack
                GameObject grunkProj = Instantiate(enemyData.m_projectile, transform.position, transform.rotation);
                grunkProj.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * shotForce);
                Destroy(grunkProj, 3);
                StartCoroutine("WaitToAttack", timeBetweenAttacks);
                break;
            case 1:
                //Flying thing
                StartCoroutine("WaitToAttack", timeBetweenAttacks);
                break;
        }
    }

    public void OnSpawnEvents()
    {

    }

    public void OnDeadEvents()
    {
        
    }

    public bool IsAlive()
    {
        return m_alive;
    }

    void UpdateHealthBar()
    {
        _healthBarImage.fillAmount = m_currentHP / enemyData.m_maxHP;
        _healthBarValue.text = string.Format("{0}\n{1} / {2}", enemyData.name, Mathf.Round(m_currentHP), enemyData.m_maxHP);
    }

    public void TakeDamage(float damageTaken)
    {
        m_currentHP -= damageTaken;
        UpdateHealthBar();
    }

    IEnumerator WaitToAttack(float timer)
    {
        canAttack = false;
        yield return new WaitForSeconds(timer);
        canAttack = true;
    }

    IEnumerator EnemyDie()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
