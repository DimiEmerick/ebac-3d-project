using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    public Animator chestAnimator;
    public string triggerOpen = "Open";

    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = .2f;
    public Ease tweenEase = Ease.OutBack;

    private float _startScale;

    private void Start()
    {
        _startScale = notification.transform.localScale.x;
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        chestAnimator.SetTrigger(triggerOpen);
    }

    public void OnTriggerEnter(Collider other)
    {
        EbacPlayer p = other.transform.GetComponent<EbacPlayer>();
        if(p != null)
        {
            ShowNotification();
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        EbacPlayer p = other.transform.GetComponent<EbacPlayer>();
        if(p != null)
        {
            HideNotification();
        }
    }

    [NaughtyAttributes.Button]
    private void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(_startScale, tweenDuration);
    }

    [NaughtyAttributes.Button]
    private void HideNotification()
    {
        notification.SetActive(false);
    }
}
