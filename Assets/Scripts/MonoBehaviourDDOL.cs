using UnityEngine;

public abstract class MonoBehaviourDDOL<T> : MonoBehaviour
{
    public static T Instance;

    protected void SingletonCheck(T obj)
    {
        if (Instance == null)
        {
            Instance = obj;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(gameObject);
    }
}
