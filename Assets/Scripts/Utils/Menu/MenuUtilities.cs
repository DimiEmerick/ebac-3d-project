using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuUtilities : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Scrollbar volumeBar;
    public TextMeshProUGUI helpText;
    public string parameterName;

    private bool _isMuted;
    private float _currentVolume;

    private void Awake()
    {
        ResumeGame();
    }

    private void Start()
    {
        if (volumeBar != null)
        {
            if (audioMixer.GetFloat(parameterName, out _currentVolume))
                volumeBar.value = Mathf.InverseLerp(-60f, 10f, _currentVolume);
            volumeBar.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float value)
    {
        float dB = Mathf.Lerp(-60f, 10f, value);
        audioMixer.SetFloat(parameterName, dB);
    }

    public void MuteAll()
    {
        audioMixer.GetFloat("MasterVolume", out _currentVolume);
        audioMixer.SetFloat("MasterVolume", -80f);
        _isMuted = true;
    }

    public void UnMuteAll()
    {
        audioMixer.SetFloat("MasterVolume", _currentVolume);
        _isMuted = false;
    }

    public void ToggleMute()
    {
        if (_isMuted) 
        {
            UnMuteAll();
            if (helpText != null) helpText.text = "Mute All";
        }
        else
        {
            MuteAll();
            if (helpText != null) helpText.text = "Unmute All";
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void DeleteSave()
    {
        SaveManager.Instance.DeleteSave();
    }

    public void ReturnTitleScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
