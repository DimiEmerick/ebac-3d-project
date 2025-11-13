using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent OnTriggerEvent;
    public string tagToCompare;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            SaveManager.Instance.SaveLastLevel(0, 2);
            OnTriggerEvent?.Invoke();
        }
    }
}
