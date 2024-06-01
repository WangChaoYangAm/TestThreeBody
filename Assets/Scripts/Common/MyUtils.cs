using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyUtils
{
    public static TOut TransReflection<TIn, TOut>(TIn tIn)
    {
        TOut tOut = Activator.CreateInstance<TOut>();
        var tInType = tIn.GetType();
        Debug.Log(tOut.GetType().Name + " " + tOut.GetType().GetProperties().Length);
        foreach (var itemOut in tOut.GetType().GetProperties())
        {
            Debug.Log(itemOut.Name);
            var itemIn = tInType.GetProperty(itemOut.Name); ;
            if (itemIn != null)
            {
                itemOut.SetValue(tOut, itemIn.GetValue(tIn));
            }
        }
        return tOut;
    }
}
