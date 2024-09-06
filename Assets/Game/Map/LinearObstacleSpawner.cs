
using UnityEngine;

public class LinearObstacleSpawner : ISpawner
{
    [SerializeField] private int _maxTileCount;
    [SerializeField] private GameObject _tile;
    [SerializeField] private Vector2 _spawnRange;

    private Pool<GameObject> _tilePool;
    private Transform _camera;
    private GameObject _last;
    private Vector3 _nextTilePos;

    private void Awake()
    {
        _camera = Camera.main.transform;
        GameObject[] tiles = new GameObject[_maxTileCount];
        _last = gameObject;
        GameObject last = gameObject;
        _nextTilePos = transform.position;
        GenerateNextPosition();
        for (int i = 0; i < _maxTileCount; i++)
        {
            Vector3 position = _nextTilePos;
            tiles[i] = Instantiate(_tile, position, transform.rotation, transform);
            last = tiles[i];
            GenerateNextPosition();
        }
        _last = last;
        _tilePool = new Pool<GameObject>(tiles);
    }
    private void GenerateNextPosition()
    {
        float xPos = Random.Range(_spawnRange.x, _spawnRange.y);
        _nextTilePos.x += xPos;
    }
    public GameObject Spawn(Vector3 position)
    {
        GameObject obj = _tilePool.Get();
        obj.transform.position = position;
        obj.SetActive(true);
        _last = obj;
        return obj;
    }

    public override void TrySpawn(float xPos)
    {
        if(xPos + 1 >= _nextTilePos.x)
        {
            Vector3 pos = _nextTilePos;
            Spawn(pos);
            GenerateNextPosition();
        }
    }
}
