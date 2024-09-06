using UnityEngine;

public class AgressiveCollisionOverrider : ICollisionOverrider
{
    private Player _player;
    private Timer _timer;

    public AgressiveCollisionOverrider(Player player, float bonusTime)
    {
        this._player = player;
        player.SetAgressive();
        _timer = Timer.Start(bonusTime).OnComplete(SwitchToStandartOverrider);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.TryGetComponent<IDamageble>(out IDamageble damageble))
            {
                damageble.GetDamage(1);
                if (collision.gameObject.TryGetComponent<ICostable>(out ICostable costable))
                {
                    _player.ArtefactsReceiver.Add(costable.GetCost(), "Gold");
                }
            }
        }
        else if (collision.gameObject.tag == "Obstacle")
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
                bonus.SetBonus(_player);
            }
        }
        else if (collision.gameObject.tag == "Cherry")
        {
            if (collision.gameObject.TryGetComponent<IBonus>(out IBonus bonus))
            {
                bonus.SetBonus(_player);
                _timer.Stop();
            }
        }
        else if (collision.gameObject.tag == "Coin")
        {
            if (collision.gameObject.TryGetComponent<ICostable>(out ICostable costable))
            {
                _player.ArtefactsReceiver.Add(costable.GetCost(), "Coins");
            }
        }
    }
    private void SwitchToStandartOverrider()
    {
        if(_player.CollisionOverrider == this)
        {
            ICollisionOverrider collisionOverrider = new StandartCollisionOverrider(_player);
            _player.SetCollisionOverrider(collisionOverrider);
        }

    }
}
