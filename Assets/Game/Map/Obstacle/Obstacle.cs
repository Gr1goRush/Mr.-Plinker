using UnityEngine;

public class Obstacle : MonoBehaviour, IDamager
{
    public void CauseDamage(IDamageble damageble)
    {
        damageble.GetDamage(4);
    }
}
