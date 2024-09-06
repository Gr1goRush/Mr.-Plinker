public interface IDamageble
{
    public void GetDamage(int damage);
}
public interface IDamager
{
    public void CauseDamage(IDamageble damageble);
}
internal interface IHealer
{
    public void Heal(int health);
}