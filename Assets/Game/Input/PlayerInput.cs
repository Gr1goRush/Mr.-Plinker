using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PlayerInput : MonoBehaviour
{
    private Dictionary<EventKey, UnityEvent> _events = new Dictionary<EventKey, UnityEvent>();

    public PlayerInput Get()
    {
        return FindObjectOfType<PlayerInput>();
    }
    public abstract bool GetInputAction(InputKey key);

}
public enum EventKey
{
    Shoot
}
public enum InputKey
{
    MoveDown,
    MoveUp
}
public enum AxisKey
{

}