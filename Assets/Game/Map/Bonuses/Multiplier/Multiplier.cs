using UnityEngine;

public class Multiplier : IArtefactWalletOverrider
{
    Player _player;
    private Timer _lifeTimer;
    private int _multiplaier;

    public IArtefactWallet DecoratedWallet { get; private set; }

    public IArtefactWalletOverrider DecoratorWallet {get; private set;}

    public Multiplier(IArtefactWallet decoratedWallet, Player player, int multiplaier, float lifeTime)
    {
        _player = player;
        _multiplaier = multiplaier;
        _lifeTimer = Timer.Start(lifeTime).OnComplete(SwitchToDecorated);
        DecoratedWallet = decoratedWallet;
    }

    public void Add(int coins, string name)
    {
        DecoratedWallet.Add(coins * _multiplaier, name);
    }

    public bool TryRemove(int coins, string name)
    {
        return DecoratedWallet.TryRemove(coins, name);
    }
    public void SwitchToDecorated()
    {
        if(DecoratorWallet == null)
        {
            IArtefactWalletOverrider lastOverrider = DecoratedWallet as IArtefactWalletOverrider;
            if (lastOverrider != null)
            {
                lastOverrider.SetDecoratorWallet(null);
            }

        }
        else
        {
            IArtefactWalletOverrider lastOverrider = DecoratedWallet as IArtefactWalletOverrider;
            if (lastOverrider != null)
            {
                lastOverrider.SetDecoratorWallet(DecoratorWallet);
            }
            DecoratorWallet.SetDecoratedWallet(DecoratedWallet);
        }
    }

    public void SetDecoratedWallet(IArtefactWallet artefactWallet)
    {
        DecoratedWallet = artefactWallet;
    }

    public void SetDecoratorWallet(IArtefactWalletOverrider artefactWallet)
    {
        DecoratorWallet = artefactWallet;
    }
}
