using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFind
{
    public static List<Transform> FindAllChild(Transform root)
    {
        List < Transform > res=new List <Transform>();
        foreach (Transform item in root)
        {
            res.Add(item);
        }
        return res;
    }
}
