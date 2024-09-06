
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Toggle _soundsToggle;
    [SerializeField] private Toggle _vibroToggle;
    private void Awake()
    {
        _soundsToggle.isOn = Saver.GetBool("Sounds", true);
        _vibroToggle.isOn = Saver.GetBool("Vibro", true);
    }

    public void SetSounds(bool active)
    {
        Saver.SaveBool(active, "Sounds");
    }
    public void SetVibro(bool active)
    {
        Saver.SaveBool(active, "Vibro");
    }
}
