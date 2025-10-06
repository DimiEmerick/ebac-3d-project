using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;

    private bool _checkpointActive = false;
    private string _checkpointKey = "CheckpointKey";

    private void OnTriggerEnter(Collider other)
    {
        if(!_checkpointActive && other.transform.tag == "Player")
        {
            Check();
            
        }
    }

    private void Check()
    {
        TurnOn();
        SaveCheckpoint();

    }

    [NaughtyAttributes.Button]
    private void TurnOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.gray);
    }
    [NaughtyAttributes.Button]
    private void TurnOn()
    {
        meshRenderer.material. DOColor(new Color32(255, 255, 255, 127), "_EmissionColor", 1f);
    }

    private void SaveCheckpoint()
    {
        if(PlayerPrefs.GetInt(_checkpointKey, 0) > key)
            PlayerPrefs.SetInt(_checkpointKey, key);
        _checkpointActive = true;
    }
}
