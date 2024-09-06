
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{ 
    [SerializeField] private TextMeshProUGUI _scoreOutput;
    [SerializeField] private WalletPresenter _goldPresenter;
    [SerializeField] private WalletPresenter _coinsPresenter;
    private Wallet _scoreWallet;

    public void Init(Wallet scoreWallet, Wallet goldWallet, Wallet coinsWallet)
    {
        _scoreWallet = scoreWallet;
        _goldPresenter.Init(goldWallet);
        _coinsPresenter.Init(coinsWallet);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void Open()
    {
        gameObject.SetActive(true);
        ShowScore();
    }
    private void ShowScore()
    {
        int score = _scoreWallet.Coins;

        string scoreText = $"SCORE: {score}";

        _scoreOutput.text = scoreText;
    }
    public void BackToHome()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
