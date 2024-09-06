using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private WalletPresenter _coinsPresenter;
    [SerializeField] private WalletPresenter _goldPresenter;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private HealthView _healthView;
    [SerializeField] private CameraFollowing _cameraFollowing;
    [SerializeField] private DistanceTracker _tracker;
    [SerializeField] private WalletPresenter _trackerWalletPresenter;
    [SerializeField] private MapGenerator _mapGenerator;
    [SerializeField] private SpriteSkeensData _characterData;

    private void Awake()
    {
        Wallet coinsWallet = new Wallet();
        Wallet goldWallet = new Wallet();
        _coinsPresenter.Init(coinsWallet);
        _goldPresenter.Init(goldWallet);
        IArtefactWallet artefactWallet = new ArtefactWallet(coinsWallet, goldWallet);
        Player player = Instantiate(_playerPrefab, transform.position, Quaternion.identity);
        player.OnDied += ShowScore;
        player.OnDied += _tracker.SaveDistance;
        SkeenSetup skeen = new SkeenSetup(_characterData, "PacMan");
        player.Init(skeen, artefactWallet);
        ICollisionOverrider collisionOverrider = new StandartCollisionOverrider(player);
        player.SetCollisionOverrider(collisionOverrider);
        Wallet trackerWallet = new Wallet();
        _tracker.Init(player.transform, trackerWallet);
        _trackerWalletPresenter.Init(trackerWallet);
        _mapGenerator.Init(player.transform);
        _healthView.Init(player.Health);
        _cameraFollowing.Init(player.transform);
        _losePanel.Init(trackerWallet, goldWallet, coinsWallet);
        _losePanel.Close();
    }
    private void ShowScore()
    {
        _losePanel.Open();
    }
}
