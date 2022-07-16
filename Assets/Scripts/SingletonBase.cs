using UnityEngine;

public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;
    public static T Instance { get => _instance; }

    protected bool InstanceToBeDestroyed = false;

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = GetComponent<T>();
        }
        else
        {
            transform.SetParent(_instance.transform.parent);
            InstanceToBeDestroyed = true;
            Destroy(gameObject);
        }
    }
}
