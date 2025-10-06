using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            Check();
        }
    }

    private void Check()
    {
        TurnOn();
    }

    [NaughtyAttributes.Button]
    private void TurnOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.black);
    }
    [NaughtyAttributes.Button]
    private void TurnOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }
}
