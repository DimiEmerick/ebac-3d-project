using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public List<UIFillUpdater> uIGunUpdaters;
    public EbacPlayer player;
    public float maxShots = 5f;
    public float timeToRecharge = 1f;
    public bool recharging = false;

    private float _currentShots;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<EbacPlayer>();
        GetAllUIs();
    }

    protected override IEnumerator ShootCoroutine()
    {
        if (recharging) yield break;
        while(true)
        {
            if(_currentShots < maxShots)
            {
                Shoot();
                _currentShots++;
                CheckRecharge();
                UpdateUI();
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
        recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while(time < timeToRecharge)
        {
            time += Time.deltaTime;
            Debug.Log("Recharging: " + time);
            uIGunUpdaters.ForEach(i => i.UpdateValue(time/timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
        _currentShots = 0;
        recharging = false;
    }

    private void UpdateUI()
    {
        uIGunUpdaters.ForEach(i => i.UpdateValue(maxShots, _currentShots));
    }

    private void GetAllUIs()
    {
        uIGunUpdaters = player.uiGunUpdaters;
    }
}
