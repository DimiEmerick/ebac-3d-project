using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;

namespace Itens
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }

    public class ItemManager : Singleton<ItemManager>
    {
        public List<ItemSetup> itemSetups;
        public SOInt coins;
        public TextMeshProUGUI uiTextCoins;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
            Reset();
        }

        private void Reset()
        {
            coins.value = 0;
            UpdateUI();
        }

        public void AddCoins(int amountC = 1)
        {
            coins.value += amountC;
            UpdateUI();
        }

        private void UpdateUI()
        {
            //uiTextCoins.text = coins.ToString();
            //UIInGameManager.UpdateTextCoins(coins.ToString());
        } 
    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
    }
}
