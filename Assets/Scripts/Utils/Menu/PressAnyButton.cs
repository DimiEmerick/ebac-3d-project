using UnityEngine;
using TMPro;
using DG.Tweening;

public class PressAnyButton : MonoBehaviour
{
    public GameObject mainMenu;
    public TextMeshProUGUI text;
    public float animationSize;
    public float animationDuration;

    private Tween _pulseTween;

    private void Start()
    {
        _pulseTween = text.transform.DOScale(animationSize, animationDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutBack);
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            ShowMenu();
        }
    }

    private void ShowMenu()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
