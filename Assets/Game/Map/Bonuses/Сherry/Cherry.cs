using UnityEngine;

public class Cherry : MonoBehaviour, IBonus
{
    [SerializeField] private float _bonusTime;
    public void SetBonus(Player player)
    {
        ICollisionOverrider collisionOverrider = new AgressiveCollisionOverrider(player, _bonusTime);
        player.SetCollisionOverrider(collisionOverrider);
        Die();
    }
    private void Die()
    {
        gameObject.SetActive(false);
    }
}

internal interface IBonus
{
    public void SetBonus(Player player);
}