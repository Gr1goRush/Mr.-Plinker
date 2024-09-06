using UnityEngine;

[System.Serializable]
public class AgressivePlayerAnimation : PlayerAnimation
{
    [SerializeField] private Sprite[] _openMouseSprites;
    [SerializeField] private float _footSearchRadius;
    [SerializeField] private LayerMask _footSearchMask;

    int _index = 0;
    public override void DoHappy()
    {
    }
    public override void Enable()
    {
        base.Enable();
        _animator.enabled = false;
    }
    public override void Update()
    {
        _index--;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, _footSearchMask, layerMask: _footSearchMask);
        Collider2D collider = null;
        float dist = float.MaxValue;
        for (int i = 0; i < colliders.Length; i++)
        {
            float currentDist = (colliders[i].transform.position - _transform.position).magnitude;
            if (currentDist < dist)
            {
                collider = colliders[i];
                dist = currentDist;
            }
        }
        if (collider != null )
        {
            _animator.enabled = false;
            float distance = (collider.transform.position - _transform.position).magnitude;
            if (distance < _footSearchRadius)
            {
                float step = _footSearchRadius / _openMouseSprites.Length;
                _index = _openMouseSprites.Length - 1 - Mathf.FloorToInt(distance / step);
            }
        }
        _index = Mathf.Clamp(_index, 0, _openMouseSprites.Length - 1);
        Sprite sprite = _openMouseSprites[_index];
        if (!_animator.enabled) _faceModel.sprite = sprite;
    }
}