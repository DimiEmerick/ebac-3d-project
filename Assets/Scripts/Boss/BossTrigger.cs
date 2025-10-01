using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform bossLocation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            Instantiate(bossPrefab, bossLocation);
            gameObject.SetActive(false);
        }
    }
}
