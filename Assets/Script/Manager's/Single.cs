using UnityEngine;

public class Single<T> : MonoBehaviour
    where T : MonoBehaviour
{
    public static T Instance { get; private set; } = null;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = GetComponent<T>();
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}

