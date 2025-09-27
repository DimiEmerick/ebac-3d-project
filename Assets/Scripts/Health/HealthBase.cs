using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public float startLife = 10f;
    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    [SerializeField] private float _currentLife;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }

    protected void ResetLife()
    {
        _currentLife = startLife;
    }

    protected virtual void Kill()
    {
        Destroy(gameObject, 1.45f);
    }

    protected virtual void Damage(float f)
    {
        _currentLife -= f;
        if (_currentLife <= 0)
        {
            Kill();
        }
    }
}
