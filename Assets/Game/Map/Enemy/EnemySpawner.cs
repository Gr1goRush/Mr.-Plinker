using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : ISpawner
{

    [SerializeField] private int _maxTileCount;
    [SerializeField] private WeightedObject[] _objects;
    [SerializeField] private Vector2 _xSpawnRange;
    [SerializeField] private Vector2 _ySpawnRange;
    [SerializeField] private float _obstacleCheckRadius;
    [SerializeField] private LayerMask _obstacleCheckMask;

    private List<Pool<GameObject>> _objectPools;
    private Transform _camera;
    private GameObject _last;
    private Vector3 _nextTilePos;

    private void Awake()
    {
        _camera = Camera.main.transform;
        _objectPools = new List<Pool<GameObject>>();
        _last = gameObject;
        GameObject last = gameObject;
        _nextTilePos = transform.position;
        GenerateNextPosition(0);
        for (int i = 0; i < _objects.Length; i++)
        {
            WeightedObject weightedObj = _objects[i];
            List<GameObject> tiles = new List<GameObject>();
            for (int j = 0; j < _maxTileCount; j++)
            {
                Vector3 position = _nextTilePos;
                GameObject tile = Instantiate(weightedObj.Object, position, transform.rotation, transform);
                tiles.Add(tile);
                last = tile;
                tile.SetActive(false);
                //GenerateNextPosition(0);
            }
            Pool<GameObject> pool = new Pool<GameObject>(tiles.ToArray());
            for (int w = 0; w < weightedObj.Weight; w++)
            {
                _objectPools.Add(pool);
            }
        }
        _last = last;

    }
    private void GenerateNextPosition(int i)
    {
        float xPos = Random.Range(_xSpawnRange.x, _xSpawnRange.y);
        float yPos = Random.Range(_ySpawnRange.x, _ySpawnRange.y);
        Vector2 pos = _nextTilePos + new Vector3(xPos, yPos, 0);
        if(Physics2D.OverlapCircle(pos, _obstacleCheckRadius, _obstacleCheckMask) && i < 5)
        {
            GenerateNextPosition(i++);
            return;
        }
        _nextTilePos.x += xPos;
        _nextTilePos.y = yPos;
    }
    public GameObject Spawn(Vector3 position)
    {
        Pool<GameObject> pool = _objectPools[Random.Range(0, _objectPools.Count)];
        GameObject obj = pool.Get();
        obj.transform.position = position;
        obj.SetActive(true);
        _last = obj;
        return obj;
    }

    public override void TrySpawn(float xPos)
    {
        if (xPos >= _nextTilePos.x)
        {
            Vector3 pos = _nextTilePos;
            Spawn(pos);
            GenerateNextPosition(0);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _obstacleCheckRadius);
    }
}
[System.Serializable]
public struct WeightedObject
{
    public int Weight;
    public GameObject Object;
}