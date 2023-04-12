using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private bool hasKey;
    private bool levelIsReady;

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

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
