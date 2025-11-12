using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ActionLifePack : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.L;
    public SFXType sfxType;
    public SOInt soInt;

    private void Start()
    {
        soInt = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;
    }

    private void RecoverLife()
    {
        if(soInt.value > 0)
        {
            ItemManager.Instance.RemoveByType(ItemType.LIFE_PACK);
            EbacPlayer.Instance.healthBase.ResetLife();
            SFXPool.Instance.Play(sfxType);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(keyCode))
        {
            RecoverLife();
            ItemManager.Instance.tutorialText.text = "";
            ItemManager.Instance.tutorialText.gameObject.SetActive(false);
        }
    }
}
