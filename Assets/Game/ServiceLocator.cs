using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Locator { get; private set; }
    [SerializeField] private PlayerInput _input;

    public PlayerInput Input => _input;

    private void Awake()
    {
        if(Locator == null) Locator = this;
    }
}
