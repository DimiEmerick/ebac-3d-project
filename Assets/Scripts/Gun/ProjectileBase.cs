using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float damageAmount = 1f;
    public float timeToDestroy = 2f;
    public float speed = 50f;
    public List<string> tagToHit;
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
        foreach(var t in tagToHit)
        {
            if(collision.transform.tag == t)
            {

                var damageable = collision.transform.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    Vector3 direction = collision.transform.position - transform.position;
                    direction = -direction.normalized;
                    direction.y = 0;
                    damageable.Damage(damageAmount, direction);
                }
                break;
            }
        }
        if (!collision.gameObject.CompareTag("Projectile")) Destroy(gameObject);
    }
}
