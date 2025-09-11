using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHabilityShoot : PlayerHabilityBase
{
    public GunBase gunBase;
    protected override void Init()
    {
        base.Init();
        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.performed += ctx => CancelShoot();
    }

    private void StartShoot()
    {
        gunBase.StartShoot();
        Debug.Log("Start shoot!");
    }
    private void CancelShoot()
    {
        gunBase.StopShoot();
        Debug.Log("Cancel shoot!");
    }
}
