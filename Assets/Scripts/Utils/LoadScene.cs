using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void Load(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void Load(string s)
    {
        SceneManager.LoadScene(s);
    }

    public void LoadGame()
    {
        if (SaveManager.Instance.Setup != null)
            Load(SaveManager.Instance.Setup.lastLevel);
        else
            Load(1);
    }
}
