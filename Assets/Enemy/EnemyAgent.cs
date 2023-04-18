using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAgent : MonoBehaviour
{
    public EnemyData enemyData;
    public GameObject _projectile;
    public TrailRenderer _bulletTrail;
    public Image _healthBarImage;
    public TextMeshProUGUI _healthBarValue;
    public GameObject enemyCanvas;

    public float m_currentHP;
    public bool m_alive;
    // Start is called before the first frame update
    void Awake()
    {
        m_alive = true;
        OnSpawnEvents();
        m_currentHP = enemyData.m_maxHP;
        _healthBarImage.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        enemyCanvas.transform.LookAt(Camera.main.transform);
        //enemyCanvas.transform.localEulerAngles += new Vector3(0, 180, 0);
        if (m_currentHP <= 0)
        {
            OnDeadEvents();
            gameObject.GetComponent<Rigidbody>().freezeRotation = false;
            enemyCanvas.SetActive(false);
            m_alive = false;
            StartCoroutine("EnemyDie");
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

    IEnumerator EnemyDie()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
