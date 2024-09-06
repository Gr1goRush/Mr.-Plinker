using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ArtifactsWallet _artifactsWallet;

        private ItemSkeensShop[] _itemSkeens;

        private ItemSkeensShop _currentShop;

        private void Awake()
        {
            _artifactsWallet.Init();
            _itemSkeens = GetComponentsInChildren<ItemSkeensShop>();
            for (int i = 0; i < _itemSkeens.Length; i++)
            {
                _itemSkeens[i].Init(_artifactsWallet);
                _itemSkeens[i].Close();
            }
            OpenItemSkeensShop(_itemSkeens[0]);
        }
        public void OpenItemSkeensShop(ItemSkeensShop skeensShop)
        {
            _currentShop = skeensShop;
            skeensShop.Open();
            for(int i = 0; i < _itemSkeens.Length; i++)
            {
                if(skeensShop.ItemName != _itemSkeens[i].ItemName)
                {
                    _itemSkeens[i].Close();
                }
            }
        }
    }
}

