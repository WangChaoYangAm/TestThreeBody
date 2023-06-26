using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWindowType
{
    Normal,
    Fixed,
    Pop,
    Top
}
public class MyBaseWindow : MonoBehaviour
{
    public EWindowType _windowType;

    public virtual void ShowWindow(bool isShow)
    {
        this.gameObject.SetActive(isShow);
    }
}
