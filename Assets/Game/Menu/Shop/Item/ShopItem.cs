using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class ShopItem : MonoBehaviour
    {

        [SerializeField] private Transform _priceContext;
        [SerializeField] private GameObject _selectLable;
        [SerializeField] private GameObject _artefactPrefab;
        [SerializeField] private Image _icon;

        private string _name;
        private ItemCost[] _itemCosts;
        private ArtifactsWallet _wallet;
        public ItemCost[] ArtifactsType => _itemCosts;
        public string Type { get; private set; }
        public bool IsPurchased { get; private set; }
        public event Action<string> OnSelected;


        public void Init(ItemData itemData, ArtifactsWallet wallet, string type)
        {
            ItemSceneCost[] sceneCosts = itemData.ItemCosts;
            _itemCosts = new ItemCost[sceneCosts.Length];
            _name = itemData.Name;
                _icon.sprite = itemData.Icon;
            for (int i = 0; i < sceneCosts.Length; i++)
            {
                ItemSceneCost itemSceneCost = sceneCosts[i];
                ItemCost itemCost = sceneCosts[i].ItemCost;
                GameObject artefact = Instantiate(_artefactPrefab, _priceContext);
                artefact.GetComponentInChildren<Image>().sprite = itemSceneCost.Icon;
                artefact.GetComponentInChildren<TextMeshProUGUI>().text = itemCost.Cost.ToString();
                _itemCosts[i] = itemCost;
            }
            _wallet = wallet;
            Type = type;
            IsPurchased = Saver.GetBool(Type + _name, false);
            if (_name == "0")
            {
                IsPurchased = true;
                Saver.SaveBool(true, Type + _name);
            }

            if (IsPurchased)
            {
                SetAsPurchased();
            }
            else
            {
                SetAsNotPurchased();
            }
        }
        public void SetAsPurchased()
        {
            _priceContext.gameObject.SetActive(false);
            _selectLable.SetActive(true);
        }
        public void SetAsNotPurchased()
        {
            _priceContext.gameObject.SetActive(true);
            _selectLable.SetActive(false);
        }
        public bool Buy()
        {
            bool isPurchased = _wallet.TryRemoveArtifacts(_itemCosts);
            if (isPurchased)
            {
                IsPurchased = true;
                Saver.SaveBool(true,Type + _name);
                SetAsPurchased();
            }
            return isPurchased;
        }
        public void TrySelect()
        {
            if (IsPurchased)
            {
                Saver.SaveString(_name, Type);
                OnSelected?.Invoke(_name);
            }
            else
            {
                Buy();
            }
        }
    }
}
[System.Serializable]
public struct ItemData
{
    public string Name;
    public Sprite Icon;
    public ItemSceneCost[] ItemCosts;

}
[System.Serializable]
public struct ItemCost
{

    public int Cost;
    public string Type;

    public ItemCost( int cost, string type)
    {
        Cost = cost;
        Type = type;
    }
}