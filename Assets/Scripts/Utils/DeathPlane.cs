using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.transform.GetComponent<IDamageable>();
        if (damageable != null)
            damageable.Damage(10000000);
    }
}
