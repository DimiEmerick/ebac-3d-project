using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public float maxShots = 5f;
    public float timeToRecharge = 1f;

    private float _currentShots;
    private bool _recharging = false;

    protected override IEnumerator ShootCoroutine()
    {
        if (_recharging) yield break;
        while(true)
        {
            if(_currentShots < maxShots)
            {
                Shoot();
                _currentShots++;
                CheckRecharge();
                yield return new WaitForSeconds(timeBetweenShot);
            }
        }
    }

    private void CheckRecharge()
    {
        if (_currentShots >= maxShots)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while(time < timeToRecharge)
        {
            time += Time.deltaTime;
            Debug.Log("Recharging: " + time);
            yield return new WaitForEndOfFrame();
        }
        _currentShots = 0;
        _recharging = false;
    }
}
