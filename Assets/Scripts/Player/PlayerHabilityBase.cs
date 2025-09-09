using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHabilityBase : MonoBehaviour
{
    protected Player player;
    private void OnValidate()
    {
        if (player == null) player = GetComponent<Player>();
    }

    private void Start()
    {
        Init();
        OnValidate();
        RegisterListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    protected virtual void Init() { }

    protected virtual void RegisterListeners() { }

    protected virtual void RemoveListeners() { }
}
