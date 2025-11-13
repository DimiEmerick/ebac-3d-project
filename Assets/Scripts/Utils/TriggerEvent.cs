using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent OnTriggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEvent?.Invoke();
    }
}
