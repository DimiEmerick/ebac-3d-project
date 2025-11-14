using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayLevel : MonoBehaviour
{
    public TextMeshProUGUI uiTextName;

    private void OnEnable()
    {
        //  Inscreve o método OnLoad no evento FileLoaded
        SaveManager.Instance.FileLoaded += OnLoad;
    }

    public void OnLoad(SaveSetup setup)
    {
        //  Atualiza o texto do botão com o próximo nível
        uiTextName.text = "Jogar (Level " + (setup.lastLevel) + ")";
    }

    private void OnDestroy()
    {
        //  Remove a inscrição do evento quando o objeto (botão) for destruído
        if (SaveManager.Instance != null)
            SaveManager.Instance.FileLoaded -= OnLoad;
    }
}
