using UnityEngine;

public class Single<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            Instance = FindObjectOfType<T>();
            if (Instance == null)
            {
                GameObject obj = new GameObject();
                instance = obj.AddComponent<T>();
            }
            return instance;
        }
        set
        {
            return;
        }
    }
}





