using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetPlayerCam : MonoBehaviour
{
    private PlayerMove playerScript;
    private GameObject _levelManager;
    public Image sidestepBar, healthBar;
    public GameObject allHolder, deathMessage, weaponHolder;

    private float timer;

    void Update()
    {
        //Finds the player and it's script to get access to variables
        if (playerScript == null)
        {
            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                return;
            }
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        }

        sidestepBar.fillAmount = playerScript.ReturnStepCounter();
        healthBar.fillAmount = playerScript.ReturnHealth() / playerScript.ReturnMaxHealth();

        if (!playerScript.ReturnIsAlive())
        {
            allHolder.gameObject.SetActive(false);
            deathMessage.gameObject.SetActive(true);

            timer += 1 * Time.deltaTime;

            if (timer <= 0.9f)
            {
                Time.timeScale = 1 - timer;
            }

            else if (timer > 1)
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _levelManager.GetComponent<LevelManager>().SetLevelIsReady(false);
                SceneManager.LoadScene(0);
            }
        }

        //Finds the player's camera and to get access to it
        if (gameObject.GetComponent<Canvas>().worldCamera == null)
        {
            if (GameObject.FindGameObjectWithTag("MainCamera") == null)
            {
                return;
            }
            gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
    }

    private void Awake()
    {
        _levelManager = GameObject.FindGameObjectWithTag("LevelManager");
    }
}
