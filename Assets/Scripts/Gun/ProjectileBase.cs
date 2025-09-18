using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float damageAmount = 1f;
    public float timeToDestroy = 2f;
    public float speed = 50f;
    public Vector3 direction;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * speed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.transform.GetComponent<IDamageable>();
        if (damageable != null) damageable.Damage(damageAmount);
        if (!collision.gameObject.CompareTag("Projectile")) Destroy(gameObject);
    }
}
