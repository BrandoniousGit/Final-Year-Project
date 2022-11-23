using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    public void LoadLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
