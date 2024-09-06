using Shop;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField] private Image[] _bgImages;
    [SerializeField] private ItemSkeensShop _shop;
    [SerializeField] private SpriteSkeensData _data;
    [SerializeField] private string _name;

    private void Awake()
    {
        if(_shop)_shop.OnItemSelected += ChangeBG;
    }
    private void Start()
    {
        ChangeBG(_name, Saver.GetString(_name, "0"));
    }
    public void ChangeBG(string itemType, string itemName)
    {
        if(itemType == _name)
        {
            Sprite sprite = _data.GetSkeen(itemName);
            for (int i = 0; i < _bgImages.Length; i++)
            {
                _bgImages[i].sprite = sprite;
            }
        }



    }
}
