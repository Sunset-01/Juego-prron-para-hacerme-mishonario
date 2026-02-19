using UnityEngine;

public abstract class BaseSystem : MonoBehaviour
{
    protected virtual void Awake()
    {
        SystemHub.Register(this);
    }

    protected virtual void Start()
    {
        Initialize();
    }

    protected virtual void Initialize() { }
}
