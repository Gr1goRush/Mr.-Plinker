using TMPro;
using UnityEngine;
public class WalletPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsOutput;
    [SerializeField] private string _start;
    [SerializeField] private string _end;

    private Wallet _wallet;
    public void Init(Wallet wallet)
    {
        _wallet = wallet; 
        _coinsOutput.text = _wallet.Coins.ToString();
        _wallet.OnCoinsChanget += UpdateOutput;
    }
    private void UpdateOutput(int coins)
    {
        _coinsOutput.text = _start + coins.ToString() + _end;
    }
}