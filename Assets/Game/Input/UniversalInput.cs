using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UniversalInput : PlayerInput
{
    [SerializeField] private TouchHandler _upperHandler;
    [SerializeField] private TouchHandler _bottomHandler;

    public override bool GetInputAction(InputKey key)
    {
        switch (key)
        {
            case InputKey.MoveUp:
                return _upperHandler.PointerDown;
            case InputKey.MoveDown:
                return _bottomHandler.PointerDown;
            default:
                return false;
        }
    }
}
