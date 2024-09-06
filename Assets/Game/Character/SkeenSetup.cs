using UnityEngine;

public class SkeenSetup 
{
    public Sprite CharacterSkeen { get; private set; }

    public SkeenSetup(SpriteSkeensData characterData, string name)
    {
        CharacterSkeen = characterData.GetSkeen(Saver.GetString(name, "0"));
    }

}
