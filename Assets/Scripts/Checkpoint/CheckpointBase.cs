using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public TextMeshProUGUI checkpointText;
    public int key = 01;
    public int level = 01;

    private bool _checkpointActive = false;

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
        StartCoroutine(TextCheckpoint());
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
        CheckpointManager.Instance.SaveCheckpoint(key, level);
        _checkpointActive = true;
    }

    IEnumerator TextCheckpoint()
    {
        checkpointText.text = "Checkpoint Activated";
        yield return new WaitForSeconds(2.5f);
        checkpointText.text = "";
    }
}
