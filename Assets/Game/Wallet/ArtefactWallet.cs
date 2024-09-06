using UnityEngine;

public class ArtefactWallet :  IArtefactWallet
{
    private IWallet _coinsWallet;
    private IWallet _goldWallet;

    public ArtefactWallet(IWallet coinsWallet, IWallet goldWallet)
    {
        _coinsWallet = coinsWallet;
        _goldWallet = goldWallet;
    }
    public void Add(int coins, string name)
    {
        switch (name)
        {
            case "Gold":
                _goldWallet.Add(coins);
                GlobalWallet.AddCoins(coins, name);
                break;
            case "Coins":
                _coinsWallet.Add(coins);
                GlobalWallet.AddCoins(coins, name);
                break;
            default:
                break;
        }
    }

    public bool TryRemove(int coins, string name)
    {
        bool isRemoved = TryRemoveNonSave(coins, name);
        if (isRemoved)
        {
            GlobalWallet.TryRemoveCoins(coins, name);
        }
        return isRemoved;
    }
    private bool TryRemoveNonSave(int coins, string name)
    {
        switch (name)
        {
            case "Gold":
                return _goldWallet.TryRemove(coins);
            case "Coins":
                return _coinsWallet.TryRemove(coins);
            default:
                return false;
        }
    }
}
