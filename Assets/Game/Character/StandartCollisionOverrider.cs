
using UnityEngine;

public class StandartCollisionOverrider : ICollisionOverrider
{
    private Player _player;
    public StandartCollisionOverrider(Player player)
    {
        this._player = player;
        player.SetCalm();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Obstacle")
        {
            if (collision.gameObject.TryGetComponent<IDamager>(out IDamager damager))
            {
                damager.CauseDamage(_player);
            }
        }
        else if (collision.gameObject.tag == "Bonus")
        {
            if (collision.gameObject.TryGetComponent<IBonus>(out IBonus bonus))
            {
                _player.Animation.DoHappy();
                bonus.SetBonus(_player);
            }
        }
        else if (collision.gameObject.tag == "Cherry")
        {
            if (collision.gameObject.TryGetComponent<IBonus>(out IBonus bonus))
            {
                bonus.SetBonus(_player);
            }
        }
        else if (collision.gameObject.tag == "Coin")
        {
            if (collision.gameObject.TryGetComponent<ICostable>(out ICostable costable))
            {
                _player.Animation.DoHappy();
                _player.ArtefactsReceiver.Add(costable.GetCost(), "Coins");
            }
        }
    }
}
