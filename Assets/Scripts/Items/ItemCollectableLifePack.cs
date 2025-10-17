using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ItemCollectableLifePack : ItemCollectableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.tutorialText.gameObject.SetActive(true);
        ItemManager.Instance.tutorialText.text = "Press L to use a Life Pack and heal.";
    }
}
