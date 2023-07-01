using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySingle<T> : MonoBehaviour where T : MySingle<T>
{
    private static volatile T _instance;
    private static object _instanceLock = new object();
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        var _instanceObj = new GameObject(typeof(T).ToString());
                        _instance = _instanceObj.AddComponent<T>();
                    }
                }
            }
            return _instance;
        }
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
