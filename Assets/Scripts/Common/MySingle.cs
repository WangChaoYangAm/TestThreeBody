using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySingle<T> : MonoBehaviour where T : MySingle<T>
{
    private static T _instance;
    public static T Instance
    {
        get { return _instance; }
    }
    protected virtual void Awake()
    {
        if (_instance != null) Destroy(gameObject);
        else
            _instance = (T)this;
    }
    public static bool IsNull
    {
        get { return _instance == null; }
    }
}
