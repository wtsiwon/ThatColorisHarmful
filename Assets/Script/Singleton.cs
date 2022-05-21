using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour  where T:  MonoBehaviour
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        Instance = GetComponent<T>();
        var obj = FindObjectsOfType<T>();
        if (obj.Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }
    private void OnDestroy()
    {
        Instance = null;
    }
}
