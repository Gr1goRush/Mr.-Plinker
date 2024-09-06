using UnityEngine;

[CreateAssetMenu(fileName = "SpriteSkeensData", menuName = "Skeens/SpriteSkeensData")]
public class SpriteSkeensData : ScriptableObject
{
    [SerializeField] private SpriteSkeen[] skeens;

    public Sprite GetSkeen(string Name)
    {
        for (int i = 0; i < skeens.Length; i++)
        {
            if (skeens[i].Name == Name)
            {
                return skeens[i].Sprite;
            }
        }
        return null;

    }
}
[System.Serializable]
public struct SpriteSkeen
{
    public string Name;
    public Sprite Sprite;
}