using UnityEngine;

public class SceneCoin : MonoBehaviour, ICostable
{
    [SerializeField] private int _cost = 1;

    public int GetCost()
    {
        gameObject.SetActive(false);
        return _cost;
    }
}
