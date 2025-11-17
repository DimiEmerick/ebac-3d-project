using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Ebac.Core.Singleton;

public class AsyncLoader : Singleton<AsyncLoader>
{
    public Slider progressBar;
    public TextMeshProUGUI percentText;
    public GameObject pressAnyKeyPanel;

    private AsyncOperation _operation;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene _, LoadSceneMode __)
    {
        progressBar = FindObjectOfType<MainMenuSlider>().slider;
        percentText = FindObjectOfType<MainMenuPercentText>().text;
        pressAnyKeyPanel = FindObjectOfType<MainMenuPanelAnyButton>().gameObject;
        progressBar.gameObject.SetActive(false);
        percentText.gameObject.SetActive(false);
        pressAnyKeyPanel.SetActive(false);
    }

    public void LoadGame()
    {
        StartCoroutine(LoadSceneRoutine());
    }

    private IEnumerator LoadSceneRoutine()
    {
        //  Se houver um save, carrega o próximo level
        if (SaveManager.Instance.Setup != null)
            _operation = SceneManager.LoadSceneAsync(SaveManager.Instance.lastLevel);
        //  Senão, carrega o primeiro level
        else
            _operation = SceneManager.LoadSceneAsync(1);
        _operation.allowSceneActivation = false;
        while (!_operation.isDone)
        {
            //  Cria uma variável que guarda o valor da progressão do carregamento e converte 0.9 em 1
            float prog = Mathf.Clamp01(_operation.progress / 0.9f);
            if (progressBar != null) progressBar.value = prog;
            if (percentText != null) percentText.text = Mathf.RoundToInt(prog * 100f) + "%";
            if (_operation.progress >= .9f)
            {
                // Se houver prompt, aguarda o usuário, senão ativa automaticamente
                if (pressAnyKeyPanel != null)
                {
                    pressAnyKeyPanel.SetActive(true);
                    while (!Input.anyKeyDown)
                        yield return null;
                    yield return new WaitForSeconds(.2f);
                    _operation.allowSceneActivation = true;
                }
                else _operation.allowSceneActivation = true;

            }
        }
        yield return null;
    }
}
