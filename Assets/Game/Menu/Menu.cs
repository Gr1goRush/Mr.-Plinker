using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _info;
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _settings;
    [SerializeField] private Loading _loading;
    [SerializeField] private TextMeshProUGUI _bestScore;


    private void Awake()
    {
        _bestScore.text = Saver.GetInt("BestScore", 0).ToString();
        CloseInfo();
        CloseShop();
        CloseSettings();
        _loading.gameObject.SetActive(false);
    }
    public void StartGame()
    {
        if (_info.activeInHierarchy)
        {
            _loading.LoadGame();
        }
        else
        {
            OpenInfo();
        }
    }
    public void OpenInfo()
    {
        _info.SetActive(true);
    }
    public void OpenShop()
    {
        _shop.SetActive(true);
    }
    public void OpenSettings()
    {
        _settings.SetActive(true);
    }
    public void CloseInfo()
    {
        _info.SetActive(false);
    }
    public void CloseShop()
    {
        _shop.SetActive(false);
    }
    public void CloseSettings()
    {
        _settings.SetActive(false);
    }
}
