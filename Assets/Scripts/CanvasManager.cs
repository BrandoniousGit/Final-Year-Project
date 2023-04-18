using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject mainCanvas, levelSelectCanvas, settingsCanvas, weaponCanvas;

    public List<GameObject> CanvasList;

    public int canvasIndex, prevCanvasIndex;

    public void Start()
    {
        CanvasList.Add(mainCanvas);
        CanvasList.Add(settingsCanvas);
        CanvasList.Add(weaponCanvas);

        canvasIndex = 0;
        prevCanvasIndex = 0;
    }

    private void UpdateIndex(int index)
    {
        prevCanvasIndex = canvasIndex;
        canvasIndex = index;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (canvasIndex == 0)
            {
                return;
            }
            CanvasList[canvasIndex].SetActive(false);
            CanvasList[prevCanvasIndex].SetActive(true);

            UpdateIndex(prevCanvasIndex);
        }
    }

    public void StartButton()
    {
        CanvasList[canvasIndex].SetActive(false);

        UpdateIndex(2);

        CanvasList[canvasIndex].SetActive(true);
    }

    public void SettingsButton()
    {
        CanvasList[canvasIndex].SetActive(false);

        UpdateIndex(1);

        CanvasList[canvasIndex].SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
