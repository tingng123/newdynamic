using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommonScript : MonoBehaviour
{
    public InventoryScript inventoryScript;
    private bool inventory_opened;
    public bool startmenu_opened;
    public bool pausing;
    public GameObject MainMenu;
    public GameObject InGameObj;
    public GameObject PauseMenu;

    private void Update()
    {
        if (startmenu_opened == false)
        {
            //pause menu
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pausing == true)
                {
                    resumegame();
                }
                else
                {
                    pausegame();
                }
            }

            //inventory menu
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventory_opened == true)
                {
                    inventory_opened = inventoryScript.closeinventory();
                }
                else
                {
                    inventory_opened = inventoryScript.openinventory();
                }
            }
        }
    }

    public void StartGame()
    {
        MainMenu.SetActive(false);
        InGameObj.SetActive(true);
        startmenu_opened = false;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void pausegame()
    {
        Time.timeScale = 0.0f;
        PauseMenu.SetActive(true);
        pausing = true;
    }

    public void resumegame()
    {
        Time.timeScale = 1.0f;
        PauseMenu.SetActive(false);
        pausing = false;
    }

    public void BodyBreakDown(GameObject[] bodypart, Vector3 position)
    {
        for (int i = 0; i < bodypart.Length; i++)
        {
            GameObject temp = Instantiate(bodypart[i], position, Quaternion.identity);
            Destroy(temp, 5f);
        }
    }
}
