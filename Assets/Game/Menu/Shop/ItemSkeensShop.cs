using System;
using UnityEngine;

namespace Shop
{
    public class ItemSkeensShop : MonoBehaviour
    {
        [SerializeField] private string _itemName;
        [SerializeField] private Transform _context;
        [SerializeField] private ShopItem _itemPrefab;
        [SerializeField] private ItemData[] _itemsData;

        public event Action<string, string> OnItemSelected;

        private ArtifactsWallet _wallet;
        private ShopItem[] _skeens;
        public string ItemName => _itemName;

        public void Init(ArtifactsWallet wallet)
        {
            _wallet = wallet;
            _skeens = new ShopItem[_itemsData.Length];
            for (int i = 0; i < _itemsData.Length; i++)
            {
                ShopItem item = Instantiate(_itemPrefab, _context);
                item.Init(_itemsData[i],wallet, _itemName);
                _skeens[i] = item;
                _skeens[i].OnSelected += (name) =>
                {
                    OnItemSelected?.Invoke(_itemName, name);
                };
            }
        }
        public void Open()
        {
            gameObject.SetActive(true);
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }
        [ContextMenu("ClearKeys")]
        public void ClearPrefs()
        {
            for (int i = 0; i < _itemsData.Length; i++)
            {
                PlayerPrefs.DeleteKey(_itemName + _itemsData[i].Name);
            }

        }
    }
}
[System.Serializable]
public struct ItemSceneCost
{
    public ItemCost ItemCost;
    public Sprite Icon;
}