using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public EnemyData enemyData;
    public GameObject _projectile;
    public TrailRenderer _bulletTrail;
    public Image _healthBarImage;
    public TextMeshProUGUI _healthBarValue;
    public GameObject enemyCanvas;

    public float currentHP;
    // Start is called before the first frame update
    void Awake()
    {
        currentHP = enemyData.m_maxHP;
        _healthBarImage.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        if (currentHP <= 0)
        {
            gameObject.GetComponent<Rigidbody>().freezeRotation = false;
            enemyCanvas.SetActive(false);
            StartCoroutine("EnemyDie");
        }
    }

    void UpdateHealthBar()
    {
        enemyCanvas.transform.LookAt(Camera.main.transform);
        enemyCanvas.transform.localEulerAngles += new Vector3(0, 180, 0);
        _healthBarImage.fillAmount = currentHP / enemyData.m_maxHP;
        _healthBarValue.text = string.Format("{0}\n{1} / {2}", enemyData.name, currentHP, enemyData.m_maxHP);
    }

public void TakeDamage(float damageTaken)
    {
        currentHP -= damageTaken;
        UpdateHealthBar();
    }

    IEnumerator EnemyDie()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
