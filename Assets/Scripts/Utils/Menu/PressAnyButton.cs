using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyButton : MonoBehaviour
{
    public GameObject mainMenu;

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            ShowMenu();
        }
    }

    private void ShowMenu()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
