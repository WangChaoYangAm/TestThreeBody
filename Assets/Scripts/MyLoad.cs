using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class MyLoad 
{
    public static GameObject LoadGamepbject(string path)
    {
        Debug.Log(path);
        GameObject go = GameObject.Instantiate(Resources.Load(path) as GameObject);
        return go;
    }
}
