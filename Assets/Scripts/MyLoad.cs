using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyLoad 
{
    public static GameObject LoadGamepbject(string path)
    {
        path = Application.dataPath + path;
        GameObject gameObject = Resources.Load(path) as GameObject;
        return gameObject;
    }
}
