using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    private bool hasKey;
    private bool levelIsReady;
    public Environment currentEnvironment;
    public Environment backupEnvironment;
    public EnvironmentList eList;

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

    public void SetPlayerHaveKey(bool doThey)
    {
        hasKey = doThey;
    }

    public void SetEnvironment(Environment environment)
    {
        currentEnvironment = environment;
    }

    public void SetEnvironmentByID(int environment)
    {
        SetEnvironment(eList.environmentList[environment]);
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
        SetEnvironmentByID(1);
        DontDestroyOnLoad(this);
    }
}
