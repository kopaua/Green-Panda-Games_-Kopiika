using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singelton<T> : MonoBehaviour where T : Component
{
    public static T Instance;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        InitManager();
    }

    protected abstract void InitManager();
  
}
