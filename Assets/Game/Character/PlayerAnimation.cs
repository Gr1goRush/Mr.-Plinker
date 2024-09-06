using UnityEngine;

[System.Serializable]
public class StandartPlayerAnimation : PlayerAnimation
{
    [SerializeField] private Sprite[] _openMouseSprites;
    [SerializeField] private float _footSearchRadius;
    [SerializeField] private LayerMask _footSearchMask;


    private bool _isHappy;
    int _index = 0;
    public override void DoHappy()
    {
        _animator.enabled = true;
        _isHappy = true;
        _animator.SetTrigger("DoHappy");
        Timer.Start(3f).OnComplete(() => _isHappy = false);
    }
    public void SetSkeen(SkeenSetup skeenSetup)
    {
        _bodyModel.sprite = skeenSetup.CharacterSkeen;
        _bodySprite = skeenSetup.CharacterSkeen;
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
            if(currentDist < dist)
            {
                collider = colliders[i];
                dist = currentDist;
            }
        }
        if(collider != null && !_isHappy)
        {
            _animator.enabled = false;
            float distance = (collider.transform.position - _transform.position).magnitude;
            if (distance < _footSearchRadius)
            {
                float step = _footSearchRadius/_openMouseSprites.Length;
                _index = _openMouseSprites.Length - 1 - Mathf.FloorToInt(distance/step );
            }
        }
        else
        {

        }
        _index = Mathf.Clamp(_index, 0, _openMouseSprites.Length - 1);
        Sprite sprite = _openMouseSprites[_index];
        if(!_animator.enabled) _faceModel.sprite = sprite;
    }
}
public abstract class PlayerAnimation
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected SpriteRenderer _bodyModel;
    [SerializeField] protected SpriteRenderer _faceModel;
    [SerializeField] protected Sprite _bodySprite;

    protected Transform _transform;
    
    private bool _initialized;
    public void Init(Transform transform)
    {
        if (!_initialized)
        {
            _initialized = true;
            _transform = transform;
        }
    }
    public virtual void Enable()
    {
        _bodyModel.sprite = _bodySprite;
    }
    public abstract void Update();
    public abstract void DoHappy();
}
