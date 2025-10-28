using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHabilityShoot : PlayerHabilityBase
{
    public GunBase gunBase1;
    public GunBase gunBase2;
    public Transform gunPosition;
    public FlashColor flashColor;

    private GunBase _currentGun;

    protected override void Init()
    {
        base.Init();
        CreateGun(gunBase1);
        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.performed += ctx => CancelShoot();
        inputs.Gameplay.ChangeGun1.performed += ctx => DestroyGun();
        inputs.Gameplay.ChangeGun1.performed += ctx => CreateGun(gunBase1);
        inputs.Gameplay.ChangeGun2.performed += ctx => DestroyGun();
        inputs.Gameplay.ChangeGun2.performed += ctx => CreateGun(gunBase2);
    }

    private void CreateGun(GunBase g)
    {
        _currentGun = Instantiate(g, gunPosition);
        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void DestroyGun()
    {
        Destroy(_currentGun);
    }

    private void StartShoot()
    {
        _currentGun.StartShoot();
        flashColor?.Flash();
        Debug.Log("Start shoot!");
    }
    private void CancelShoot()
    {
        _currentGun.StopShoot();
        Debug.Log("Cancel shoot!");
    }
}
