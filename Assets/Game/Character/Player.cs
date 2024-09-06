
using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageble, IHealer
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private DirectionalMoving _directionalMoving;
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimatorOverrideController _animatorAgressive;
    [SerializeField] private StandartPlayerAnimation _calmPlayerAnimation;
    [SerializeField] private AgressivePlayerAnimation _agressivePlayerAnimation;

    private PlayerAnimation _playerAnimation;
    private Timer _timer;

    public PlayerAnimation Animation => _playerAnimation;

    public ICollisionOverrider CollisionOverrider { get; private set; }

    public IArtefactWallet ArtefactsReceiver { get; private set; }

    public event Action OnDied;
    public Health Health { get; private set; }

    private AnimatorOverrideController _animatorSkeen;
    private bool _canVibrate;

    public void Init(SkeenSetup setup, IArtefactWallet artefactWallet)
    {
        _calmPlayerAnimation.Init(transform);
        _agressivePlayerAnimation.Init(transform);
        _canVibrate = Saver.GetBool("Vibro", true);
        _calmPlayerAnimation.SetSkeen(setup);
        SetCalm();
        ArtefactsReceiver = artefactWallet;
        Health = new Health(_maxHealth);
        Health.OnHealthChanged += CheckHealth;
    }
    private void Update()
    {
        _playerAnimation?.Update();
    }
    public void CheckHealth(int health)
    {
        if(health <= 0)
        {
            Die();
        }
    }
    public void GetDamage(int damage)
    {
        if(_canVibrate) Handheld.Vibrate();
        Health.GetDamage(damage);
    }
    private void Die()
    {
        gameObject.SetActive(false);
        OnDied?.Invoke();
    }

    public void Heal(int health)
    {
        Health.Heal(health);
    }
    public void SetCollisionOverrider(ICollisionOverrider collisionOverrider)
    {
        CollisionOverrider = collisionOverrider;
    }
    public void SetAgressive()
    { 
        _playerAnimation = _agressivePlayerAnimation;
        _playerAnimation.Enable();
    }
    public void SetCalm()
    {
        _playerAnimation = _calmPlayerAnimation;
        _playerAnimation.Enable();
    }
    public void SetWalletOverride(IArtefactWallet wallet)
    {
        ArtefactsReceiver = wallet;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionOverrider.OnTriggerEnter2D(collision);
    }
}

public interface IArtefactWallet
{
    public void Add(int coins, string name);
    public bool TryRemove(int coins, string name);
}
public interface IArtefactWalletOverrider : IArtefactWallet
{
    public IArtefactWallet DecoratedWallet { get; }
    public IArtefactWalletOverrider DecoratorWallet { get; }
    public void SetDecoratedWallet(IArtefactWallet artefactWallet);
    public void SetDecoratorWallet(IArtefactWalletOverrider artefactWallet);
}

public interface ICollisionOverrider
{
    public void OnTriggerEnter2D(Collider2D collision);
}