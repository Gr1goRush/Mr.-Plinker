using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMultiplier : MonoBehaviour, IBonus
{
    [SerializeField] private int _multiplier;
    [SerializeField] private float _duration;
    public void SetBonus(Player player)
    {
        IArtefactWalletOverrider overrider = new Multiplier(player.ArtefactsReceiver, player, _multiplier, _duration);
        IArtefactWalletOverrider lastOverrider = player.ArtefactsReceiver as IArtefactWalletOverrider;
        if(lastOverrider != null)
        {
            lastOverrider.SetDecoratorWallet(overrider);
        }
        player.SetWalletOverride(overrider);
        gameObject.SetActive(false);
    }
}
