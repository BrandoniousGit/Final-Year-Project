using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private bool hasKey;
    private bool levelIsReady;
    private int currentStage;
    public Environment currentEnvironment;
    public Environment backupEnvironment;

    public int environmentToUse;
    public List<Environment> environmentList;

    public List<GameObject> enemies;

    private float timer;

    public List<GameObject> GetEnemyList()
    {
        return enemies;
    }

    public bool IsLevelReady()
    {
        return levelIsReady;
    }

    public void SetLevelIsReady(bool shouldit)
    {
        levelIsReady = shouldit;
    }

    public bool DoesPlayerHaveKey()
    {
        return hasKey;
    }

    public void SetPlayerHasKey(bool doThey)
    {
        hasKey = doThey;
    }

    public void SetEnvironment(Environment environment)
    {
        currentEnvironment = environment;
    }

    public void SetEnvironmentByID(int environment)
    {
        SetEnvironment(environmentList[environment]);
    }

    public Environment GetCurrentEnvironment()
    {
        if (currentEnvironment == null) 
        {
            return backupEnvironment;
        }
        return currentEnvironment;
    }

    private void Awake()
    {
        SetEnvironmentByID(environmentToUse);
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (levelIsReady)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                timer = 0;
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                timer += 1 * Time.deltaTime;

                if (timer >= 3)
                {
                    timer = 0;
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    levelIsReady = false;
                    SceneManager.LoadScene(0);
                }
            }
        }
    }

    public void NextStage()
    {
        SetEnvironmentByID(Random.Range(0, 3));
        SceneManager.LoadScene(1);
    }
}
