using UnityEngine;

public class Enemy : MonoBehaviour, IDamageble, IDamager, ICostable
{
    [SerializeField] private int _damage;
    [SerializeField] private int _cost = 1;

    public void CauseDamage(IDamageble damageble)
    {
        damageble.GetDamage(_damage);
    }

    public int GetCost()
    {
        return _cost;
    }

    public void GetDamage(int damage)
    {
        gameObject.SetActive(false);
    }
}

