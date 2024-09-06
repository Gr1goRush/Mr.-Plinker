using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private ISpawner[] _spawners;
    [SerializeField] private float _xOffset;
    private Transform _target;

    public void Init(Transform target)
    {
        _target = target;
    }
    private void Update()
    {
        float xPos = _target.position.x + _xOffset;
        for (int i = 0; i < _spawners.Length; i++)
        {
            _spawners[i].TrySpawn(xPos);
        }
    }
}
