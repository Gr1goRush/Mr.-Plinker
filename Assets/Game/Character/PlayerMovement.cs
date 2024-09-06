
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _movmentRange;
    private PlayerInput _input;
    private bool _canChangeDirection;

    private void Start()
    {
        _input = ServiceLocator.Locator.Input;
    }
    private void FixedUpdate()
    {
        Vector2 input = Vector3.zero;
        if (_input.GetInputAction(InputKey.MoveUp))
        {
            input = Vector2.up;
        }
        else if (_input.GetInputAction(InputKey.MoveDown))
        {
            input = Vector2.down;
        }
        Vector2 nextPosition = _rb.position + (input * _speed * Time.fixedDeltaTime);
        nextPosition.y = Mathf.Clamp(nextPosition.y, _movmentRange.x, _movmentRange.y);
        _rb.position = (nextPosition);
    }
}
